namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Marimba.Utility;

    /// <summary>
    /// The main Club object which holds all the information we want to keep track of
    /// </summary>
    class Club
    {
        /// <summary>
        /// the latest file version, and the version used to save
        /// </summary>
        public static readonly double FileVersion = 2.2;

        /// <summary>
        /// used to write the main file
        /// </summary>
        public BinaryReader br;

        /// <summary>
        /// used to write the main file
        /// </summary>
        public static BinaryWriter bw;

        /// <summary>
        /// used to write the main file
        /// </summary>
        public FileStream fs;

        /// <summary>
        /// aesInfo is for encryption, AES encryption; stores encryption information
        /// </summary>
        private Aes aesInfo;

        /// <summary>
        /// this essentially contains the encrypted part of the club... it should be cleared once no longer needed (since it is huge)
        /// </summary>
        private byte[] cipherText;

        /// <summary>
        /// strName is the name of the club
        /// </summary>
        public string strName;

        /// <summary>
        /// fileVersion contains the version of the file currently loaded
        /// </summary>
        public double fileVersion;

        /// <summary>
        /// Number of user fields that are stored
        /// </summary>
        private static readonly int UserFieldsToStore = 4;

        /// <summary>
        /// strUsers [,0] stores Name
        /// strUsers [,1] stores password (note: encrypted, but not that well)
        /// I do not recommend publicly releasing any .mrb files and use a unique password for Marimba
        /// strUsers [,2] stores type of user
        /// strUsers [,3] stores the key xor'd with the single hash of the user's password
        /// </summary>
        public List<string[]> strUsers;

        /// <summary>
        /// Number of members currently in the club
        /// </summary>
        public short iMember;

        /// <summary>
        /// array of members on the mailing list
        /// currently, prepare for five thousand total members
        /// </summary>
        public Member[] members = new Member[5000];

        /// <summary>
        /// location of the .mrb file to save this club to
        /// </summary>
        protected string strLocation;

        /// <summary>
        /// Name of current logged in user
        /// </summary>
        public string strCurrentUser;
        
        /// <summary>
        /// Priviledge level of current logged in user
        /// </summary>
        public string strCurrentUserPrivilege;

        /// <summary>
        /// stores the information about the terms
        /// </summary>
        public List<Term> listTerms;

        /// <summary>
        /// TODO: remove this
        /// </summary>
        public Election currentElection;

        /// <summary>
        /// list of budget items stored in Marimba
        /// </summary>
        public List<BudgetItem> budget;

        /// <summary>
        /// list of history items stored in Marimba
        /// </summary>
        public List<HistoryItem> historyList;

        /// <summary>
        /// Email address for the club
        /// </summary>
        public string emailAddress;

        /// <summary>
        /// IMAP server address for the club email
        /// </summary>
        public string imapServerAddress;

        /// <summary>
        /// SMTP server address for the club email
        /// </summary>
        public string smptServerAddress;
        
        /// <summary>
        /// Whether SSL is required for the club's IMAP server
        /// </summary>
        public bool bImap;

        /// <summary>
        /// Whether SSL is required for the club's SMTP server
        /// </summary>
        public bool imapRequiresSSL;

        /// <summary>
        /// The SMTP server's port to use
        /// </summary>
        public int smtpRequiresSSL;

        /// <summary>
        /// The email object that handles the email functions in Marimba
        /// </summary>
        public Email clubEmail;

        /// <summary>
        /// The length of the salt to use for passwords
        /// </summary>
        private static readonly int SaltLength = 16;

        /// <summary>
        /// when initializing the list of budget items, how much buffer to add at the end of the list for new budget items
        /// </summary>
        private static readonly int BudgetBuffer = 50;

        /// <summary>
        /// when initializing the list of history items, how much buffer to add at the end of the list for new budget items
        /// </summary>
        private static readonly int HistoryBuffer = 50;

        /// <summary>
        /// The possible user privileges in Marimba
        /// </summary>
        private static readonly string[] ValidPrivileges = { "Exec", "Admin" };

        public Club(string strLocation, Aes aesKey = null)
        {
            this.strLocation = strLocation;
            this.strName = String.Empty;
            this.aesInfo = aesKey;
        }

        /// <summary>
        /// Used for duplicating the club, specifically for Excel
        /// </summary>
        /// <param name="strLocation">The file location to save the new club to</param>
        /// <returns>A clone of the current club</returns>
        public Club CloneClub(string strLocation)
        {
            return new Club(this.strLocation, this.aesInfo);
        }

        /// <summary>
        /// Loads the unencrypted part of the .mrb file into the Club
        /// </summary>
        public void LoadClub()
        {
            // read the given file, update all of the appropriate information
            this.fs = new FileStream(this.strLocation, FileMode.Open);

            this.br = new BinaryReader(fs);
            fileVersion = this.br.ReadDouble(); // read the version number, needed for reading legacy file formats
            this.strName = this.br.ReadString();
            int iUser = this.br.ReadInt32();

            // this next part is for importing old files
            int numUsers;
            if (fileVersion < 2)
                numUsers = 20;
            else
                numUsers = iUser;

            strUsers = new List<string[]>(iUser);
            for (int i = 0; i < numUsers; i++)
            {
                string[] nextUser = new string[UserFieldsToStore];
                for (int j = 0; j < UserFieldsToStore; j++)
                {
                    nextUser[j] = this.br.ReadString();
                }

                strUsers.Add(nextUser);
            }
            
            // string strKey = br.ReadString();
            string strIV = this.br.ReadString();

            // REMOVE LATER: Unsafe, only suitable for testing
            this.aesInfo = Aes.Create();

            // aesInfo.Key = Convert.FromBase64String(strKey);
            this.aesInfo.IV = Convert.FromBase64String(strIV);

            cipherText = this.br.ReadBytes(Convert.ToInt32(this.br.BaseStream.Length - this.br.BaseStream.Position));
        }

        /// <summary>
        /// Loads the encrypted part of the .mrb file into the Club
        /// </summary>
        public void LoadEncryptedSection()
        {
            /**********************************
             * HOW ENCRYPTION IN MARIMBA WORKS*
             * One part of the .mrb file is encrypted, the other is not
             * The part that is not encrypted is login information
             * A user will try to login with their password
             * The double-hash of the password is used to check the hash is correct
             * If correct, login will retrieve the key the rest of the file is encrypted with
             * That key is stored by being xor'd with the single hash of the password
             * As long as the password is unknown and SHA-256 is preimage resistant, this should be secure
             * The rest of the file is encrypted in 256-bit AES
             * The key is retreived, and the rest of the file is decrypted
             * The file is loaded into Marimba as normally would be
             * If a user changes their password, the key xor'd with the single hash is redone
             * If an admin updates the key, everyone gets their key/hash thing updated
             * This can easily be done if the key is known since xor is linear
             * */

            // DECRYPT ENCRYPTED SECTION
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = this.aesInfo.Key;
                aesAlg.IV = this.aesInfo.IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            // read the membership list
                            iMember = Convert.ToInt16(reader.ReadLine());
                            for (int i = 0; i < iMember; i++)
                                members[i] = new Member(reader);
                            short numTerms = Convert.ToInt16(reader.ReadLine());
                            listTerms = new List<Term>(numTerms);
                            for (int i = 0; i < numTerms; i++)
                            {
                                Term nextTerm = new Term(reader);
                                listTerms.Add(nextTerm);
                            }
                            
                            // read the budget stuff
                            int iBudget = Convert.ToInt32(reader.ReadLine());
                            this.budget = new List<BudgetItem>(iBudget + BudgetBuffer);
                            List<int> assetToDepIndices = new List<int>(iBudget);
                            for (int i = 0; i < iBudget; i++)
                            {
                                BudgetItem newItem = new BudgetItem();
                                newItem.value = Convert.ToDouble(reader.ReadLine());
                                newItem.name = reader.ReadLine();
                                newItem.dateOccur = new DateTime(Convert.ToInt64(reader.ReadLine()));
                                newItem.dateAccount = new DateTime(Convert.ToInt64(reader.ReadLine()));
                                newItem.cat = reader.ReadLine();
                                newItem.type = (TransactionType)Convert.ToInt32(reader.ReadLine());
                                newItem.term = Convert.ToInt32(reader.ReadLine());
                                newItem.comment = ClsStorage.ReverseCleanNewLine(reader.ReadLine());
                                budget.Add(newItem);

                                assetToDepIndices.Add(Convert.ToInt32(reader.ReadLine()));
                            }

                            int k = 0;
                            foreach (BudgetItem item in this.budget)
                            {
                                int assetToDepIndex = assetToDepIndices[k];
                                item.depOfAsset = (assetToDepIndex == -1) ? null : this.budget[assetToDepIndex];
                                k++;
                            }

                            int iHistory = Convert.ToInt32(reader.ReadLine());
                            historyList = new List<HistoryItem>(iHistory + HistoryBuffer);
                            for (int i = 0; i < iHistory; i++)
                            {
                                HistoryItem nextItem = new HistoryItem(reader);
                                historyList.Add(nextItem);
                            }

                            // read email
                            if (fileVersion >= 2)
                            {
                                emailAddress = reader.ReadLine();
                                imapServerAddress = reader.ReadLine();
                                this.bImap = Convert.ToBoolean(reader.ReadLine());
                                smptServerAddress = reader.ReadLine();
                                smtpRequiresSSL = Convert.ToInt32(reader.ReadLine());
                                imapRequiresSSL = Convert.ToBoolean(reader.ReadLine());
                            }
                        }
                    }
                }

                // remove cipherText from storage
                cipherText = null;
            }

            this.clubEmail = new Email(emailAddress, Properties.Settings.Default.emailPassword, imapServerAddress, this.bImap, smptServerAddress, smtpRequiresSSL, imapRequiresSSL);
        }

        /// <summary>
        /// Saves the current club
        /// </summary>
        public void SaveClub()
        {
            if (this.br != null)
                this.br.Close();
            fs = new FileStream(this.strLocation, FileMode.Create);
            bw = new BinaryWriter(fs);

            // this line is the file version number
            // this will be useful later on if .mrb files are siginificantly modified
            bw.Write(FileVersion);
            bw.Write(strName);
            bw.Write(strUsers.Count);

            // write the users (i.e. exec account information)
            foreach (string[] user in strUsers)
            {
                for (int i = 0; i < UserFieldsToStore; i++)
                {
                    bw.Write(user[i]);
                }
            }
            
            // ENCRYPTED SECTION
            byte[] bEncryptedSection;
            
            // generate a new IV
            this.aesInfo.GenerateIV();
            bw.Write(Convert.ToBase64String(this.aesInfo.IV));

            // In future, set key to whatever here
            // aesAlg.Key;
            // create encryptor
            using (Aes AesEncrypt = Aes.Create())
            {
                AesEncrypt.Key = this.aesInfo.Key;
                AesEncrypt.IV = this.aesInfo.IV;
                ICryptoTransform encryptor = AesEncrypt.CreateEncryptor(AesEncrypt.Key, AesEncrypt.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            // write the members (i.e. mailing/membership list)
                            sw.WriteLine(iMember);
                            for (int i = 0; i < iMember; i++)
                            {
                                sw.WriteLine(members[i].firstName);
                                sw.WriteLine(members[i].lastName);
                                sw.WriteLine((int)members[i].type);
                                sw.WriteLine(members[i].uiStudentNumber);
                                sw.WriteLine((int)members[i].memberFaculty);

                                // if the member plays an "other" instrument, write it here
                                // write blank if the member does not play an other instrument
                                sw.WriteLine(members[i].otherInstrument);

                                // write the main instrument
                                sw.WriteLine((int)members[i].curInstrument);

                                // write if the member plays multiple instruments
                                // write any other instruments that the member plays (or does not play)
                                sw.WriteLine(members[i].bMultipleInstruments);
                                int numberOfInstruments = Enum.GetValues(typeof(Member.Instrument)).Length;
                                if (members[i].bMultipleInstruments)
                                    for (int j = 0; j < numberOfInstruments; j++)
                                        sw.WriteLine(members[i].playsInstrument[j]);

                                sw.WriteLine(members[i].email);
                                sw.WriteLine(ClsStorage.CleanNewLine(members[i].comments));
                                sw.WriteLine(members[i].sID);
                                sw.WriteLine(members[i].signupTime.Ticks);
                                sw.WriteLine((int)members[i].size);
                            }

                            // write the terms
                            sw.WriteLine(listTerms.Count);

                            // loop through the terms
                            foreach (Term currentTerm in listTerms)
                            {
                                currentTerm.saveTerm(sw);
                            }

                            // save the budget
                            sw.WriteLine(this.budget.Count);
                            foreach (BudgetItem item in this.budget)
                            {
                                sw.WriteLine(item.value);
                                sw.WriteLine(item.name);
                                sw.WriteLine(item.dateOccur.Ticks);
                                sw.WriteLine(item.dateAccount.Ticks);
                                sw.WriteLine(item.cat);
                                sw.WriteLine((int)item.type);
                                sw.WriteLine(item.term);
                                sw.WriteLine(ClsStorage.CleanNewLine(item.comment));
                                sw.WriteLine(this.budget.IndexOf(item.depOfAsset));
                            }

                            // save history
                            sw.WriteLine(historyList.Count);
                            foreach (HistoryItem item in historyList)
                            {
                                item.saveHistory(sw);
                            }
                            
                            // save email details
                            sw.WriteLine(emailAddress);
                            sw.WriteLine(imapServerAddress);
                            sw.WriteLine(this.bImap);
                            sw.WriteLine(smptServerAddress);
                            sw.WriteLine(smtpRequiresSSL);
                            sw.WriteLine(imapRequiresSSL);
                        }

                        bEncryptedSection = ms.ToArray();
                        bw.Write(bEncryptedSection);
                    }
                }
            }

            bw.Close();
            fs.Close();
            
            // reopen the binary reader to prevent anyone else from editing the file
            this.br = new BinaryReader(new FileStream(this.strLocation, FileMode.Open));
        }

        /// <summary>
        /// Saves the club to the given file location
        /// </summary>
        /// <param name="strLocation">Location of .mrb file to save to</param>
        public void SaveClub(string strLocation)
        {
            // change the location, and then save
            this.strLocation = strLocation;
            SaveClub();
        }

        /// <summary>
        /// add this user with the given name, password and privilege level
        /// </summary>
        /// <param name="strName">Name of user</param>
        /// <param name="strPassword">Password selected by user</param>
        /// <param name="strPrivileges">Privilege level for user</param>
        /// <returns>Whether user creation was successful</returns>
        public bool AddUser(string strName, string strPassword, string strPrivileges)
        {
            // see if a user with this name already exists
            if (FindUser(strName) != null)
                return false;

            // priviledge level isn't allowed
            if (Array.IndexOf(ValidPrivileges, strPrivileges) < 0)
            {
                return false;
            }

            // do a basic encryption on the password
            // the intention is just so that no one can read plaintext passwords
            // I am well aware this algorithm isn't particularly strong, but it is sufficient for our needs
            SHA256 shaHash = SHA256.Create();

            // salt
            byte[] salt = new byte[SaltLength];
            int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
            byte[] saltPlusPassword = new byte[SaltLength + passwordLength];

            // generate salt
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }

            // combine the salt and password
            Array.Copy(salt, saltPlusPassword, SaltLength);
            Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, SaltLength, passwordLength);

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

            string[] newUser = new string[UserFieldsToStore];
            newUser[0] = strName;
            newUser[1] = ConvertToString(salt) + "$" + ConvertToString(data);
            newUser[2] = strPrivileges;
            newUser[3] = Convert.ToBase64String(ClsStorage.XOR(this.aesInfo.Key, shaHash.ComputeHash(saltPlusPassword)));
            strUsers.Add(newUser);
            return true;
        }

        /// <summary>
        /// remove the user with the given name from Marimba
        /// </summary>
        /// <param name="strName">The name of the user to remove</param>
        /// <returns>Whether removal was successful</returns>
        public bool DeleteUser(string strName)
        {
            string[] user = FindUser(strName);
            if (user == null)
            {
                return false;
            }

            return strUsers.Remove(user);
        }

        /// <summary>
        /// get the index of the specified user
        /// </summary>
        /// <param name="strName">name of user</param>
        /// <returns>Array representing user if exists, null if doesn't exist</returns>
        public string[] FindUser(string strName)
        {
            int i = 0;

            // first, find the user
            foreach (string[] user in strUsers)
            {
                if (user[0].Equals(strName))
                {
                    return user;
                }

                i++;
            }

            // did not find user
            return null;
        }

        /// <summary>
        /// returns true if the login was successful, false otherwise
        /// </summary>
        /// <param name="strName">name of user to login as</param>
        /// <param name="strPassword">password to user to login as</param>
        /// <returns>Whether the login was successful</returns>
        public bool LoginUser(string strName, string strPassword)
        {
            string[] user = FindUser(strName);
            if (user == null)
                return false;
            else
            {
                // create a hash of the password and compare
                SHA256 shaHash = SHA256.Create();

                // Convert the input string to a byte array and compute the hash. 
                
                // retrieve the salt
                byte[] salt = StringToByteArray(user[1].Split('$')[0]);

                // retrieve the hash
                string hash = user[1].Split('$')[1];

                // calculate hash of salt + password
                int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
                byte[] saltPlusPassword = new byte[SaltLength + passwordLength];
                Array.Copy(salt, saltPlusPassword, SaltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, SaltLength, passwordLength);

                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                if (StringComparer.OrdinalIgnoreCase.Compare(hash, ConvertToString(data)) == 0)
                {
                    this.strCurrentUser = strName;
                    this.strCurrentUserPrivilege = user[2];
                    try
                    {
                        this.aesInfo.Key = ClsStorage.XOR(Convert.FromBase64String(user[3]), shaHash.ComputeHash(saltPlusPassword));
                    }
                    catch
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Error.Play();
                        System.Windows.Forms.MessageBox.Show("Username and password are correct, but key is corrupted. Unable to open file.");
                        return false;
                    }

                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Edit the currently logged in user
        /// </summary>
        /// <param name="strName">Name of the current user</param>
        /// <param name="strPassword">Old password of the current user</param>
        /// <param name="strNewPassword">New password of the current user</param>
        /// <returns>Whether the current user was successfully edited</returns>
        public bool EditUser(string strName, string strPassword, string strNewPassword)
        {
            // check user exists and current password is correct
            if (LoginUser(strName, strPassword))
            {
                string[] user = FindUser(strName);

                // replace the old password with the new password
                SHA256 shaHash = SHA256.Create();

                // salt
                byte[] salt = new byte[SaltLength];
                int passwordLength = Encoding.UTF8.GetBytes(strNewPassword).Length;
                byte[] saltPlusPassword = new byte[SaltLength + passwordLength];

                // generate salt
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetBytes(salt);
                }

                // combine the salt and password
                Array.Copy(salt, saltPlusPassword, SaltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strNewPassword), 0, saltPlusPassword, SaltLength, passwordLength);

                // Convert the input string to a byte array and compute the hash.
                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                user[1] = ConvertToString(salt) + "$" + ConvertToString(data);

                // add the key used to encrypted the files here
                user[3] = Convert.ToBase64String(ClsStorage.XOR(this.aesInfo.Key, shaHash.ComputeHash(saltPlusPassword)));

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Changes the priviledge level of this user
        /// </summary>
        /// <param name="strName">Name of the user to edit</param>
        /// <param name="strNewPrivilege">New priviledge level for user</param>
        /// <returns>Whether editing privilege was successful</returns>
        public bool EditUserPrivilege(string strName, string strNewPrivilege)
        {
            string[] user = FindUser(strName);
            if (user == null || Array.IndexOf(ValidPrivileges, strNewPrivilege) < 0)
            {
                return false;
            }

            user[2] = strNewPrivilege;
            return true;
        }

        /// <summary>
        /// Updates the master key used to encrypt the passwords
        /// </summary>
        public void UpdateKey()
        {
            // first, generate a new key
            Aes newKey = Aes.Create();
            
            // next, update the key access everyone has
            // NOTE: We reset everyone's password; for now to the default of being the club name
            // An admin should go in and change everyone's password to something else
            // The key would be updated to prevent a person who previously had access from having access again
            SHA256 shaHash = SHA256.Create();
            byte[] data;
            byte[] salt = new byte[SaltLength];
            int passwordLength = Encoding.UTF8.GetBytes(strName).Length;
            byte[] saltPlusPassword = new byte[SaltLength + passwordLength];
            
            foreach (string[] user in strUsers)
            {
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // generate salt
                    rngCsp.GetBytes(salt);
                }

                // combine the salt and password
                Array.Copy(salt, saltPlusPassword, SaltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strName), 0, saltPlusPassword, SaltLength, passwordLength);

                data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                // build hash
                user[1] = ConvertToString(salt) + "$" + ConvertToString(data);

                user[3] = Convert.ToBase64String(ClsStorage.XOR(shaHash.ComputeHash(saltPlusPassword), newKey.Key));
            }

            // finally, update key
            this.aesInfo.Key = newKey.Key;
        }

        /// <summary>
        /// Converts a byte array into a hexadecimal string
        /// </summary>
        /// <param name="byteArray">Array of bytes</param>
        /// <returns>String in hexadecimal</returns>
        public static string ConvertToString(byte[] byteArray)
        {
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < byteArray.Length; j++)
                builder.Append(byteArray[j].ToString("x2"));
            return builder.ToString();
        }

        /// <summary>
        /// Reverses bytesToHex
        /// </summary>
        /// <param name="hex">The array to convert</param>
        /// <returns>An array of bytes equivalent to the input</returns>
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Adds a member to the club member list
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Type of member</param>
        /// <param name="uiID">ID number to give to this member</param>
        /// <param name="iFaculty">Faculty of member</param>
        /// <param name="strInstrument">Instrument which this member plays</param>
        /// <param name="strOtherInstrument">Other Instrument</param>
        /// <param name="strEmail">Email of user</param>
        /// <param name="strOther">Other info</param>
        /// <param name="shirtSize">Shirt size</param>
        /// <returns>Whether the member was successfully added</returns>
        public bool AddMember(string strFName, string strLName, Member.MemberType type, uint uiID, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int shirtSize)
        {
            // before adding, check if it is a duplicate member
            // a matching student number or email address will be the judge of this
            // if it is a duplicate, then update the member's profile
            for (int i = 0; i < iMember; i++)
                if (this.members[i].email == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].EditMember(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, shirtSize);
                    return false;
                }

            this.members[iMember] = new Member(strFName, strLName, type, uiID, iFaculty, strInstrument, strOtherInstrument, strEmail, strOther, shirtSize);
            iMember++;
            return true;
        }

        /// <summary>
        /// Adds a member to the club member list
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Type of member</param>
        /// <param name="uiID">ID number to give to this member</param>
        /// <param name="iFaculty">Faculty of member</param>
        /// <param name="strInstrument">Instrument which this member plays</param>
        /// <param name="strOtherInstrument">Other Instrument</param>
        /// <param name="strEmail">Email of user</param>
        /// <param name="strOther">Other info</param>
        /// <param name="shirtSize">Shirt size</param>
        /// <param name="bInstruments">Boolean array of whether a member plays each instrument</param>
        /// <returns>Whether the member was successfully added</returns>
        public bool AddMember(string strFName, string strLName, Member.MemberType type, uint uiID, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int shirtSize, bool[] bInstruments)
        {
            // before adding, check if it is a duplicate member
            // a matching student number or email address will be the judge of this
            // if it is a duplicate, then update the member's profile
            for (int i = 0; i < iMember; i++)
                if (this.members[i].email == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].EditMember(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, shirtSize);
                    return false;
                }

            this.members[iMember] = new Member(strFName, strLName, type, uiID, iFaculty, strInstrument, strOtherInstrument, strEmail, strOther, shirtSize, bInstruments);
            iMember++;
            return true;
        }

        /// <summary>
        /// Adds a member to the club member list
        /// this version is kept for legacy purposes to open old data.
        /// Potentially updates an old member instead of adding a new member
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Type of member</param>
        /// <param name="uiID">ID number to give to this member</param>
        /// <param name="iFaculty">Faculty of member</param>
        /// <param name="strInstrument">Instrument which this member plays</param>
        /// <param name="strEmail">Email of user</param>
        /// <param name="strOther">Other info</param>
        /// <param name="signup">Time that this member signed up</param>
        /// <returns>Whether a strictly new member is added; if an old member is updated, this return false</returns>
        public bool AddMember(string strFName, string strLName, Member.MemberType type, uint uiID, int iFaculty, string strInstrument, string strEmail, string strOther, DateTime signup)
        {
            // before adding, check if it is a duplicate member
            // a matching student number or email address will be the judge of this
            for (int i = 0; i < iMember; i++)
                if (this.members[i].email == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].EditMember(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, -1);
                    return false;
                }

            this.members[iMember] = new Member(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, signup, -1);
            iMember++;
            return true;
        }

        /// <summary>
        /// Removes members who have not attended a single rehearsal in 4 years from the club mailing list
        /// </summary>
        public void PurgeOldMembers()
        {
            bool attendedOneRehearsal;

            // for each member, check if they have attended any rehearsals
            // we aren't getting rid of members who have in fact attended a rehearsal and were once a member
            // this is to prevent the mailing list from getting too cluttered
            for (int i = 0; i < iMember; i++)
            {
                attendedOneRehearsal = false;

                // check each term to confirm they are not in any of them
                for (int j = 0; j < listTerms.Count && !attendedOneRehearsal; j++)
                {
                    attendedOneRehearsal = attendedOneRehearsal || listTerms[j].memberSearch((short)i) != -1;
                }

                // if they haven't attended any rehearsals, next check if they have been on the list for four years (1461 days)
                // just keep using the same attendedOneRehearsal variable
                attendedOneRehearsal = attendedOneRehearsal || TimeSpan.Compare(DateTime.Now - members[i].signupTime, TimeSpan.FromDays(1461)) < 0;

                // if they didn't meet at least one of these requirements... they gotta go!
                if (!attendedOneRehearsal)
                {
                    // bye bye!

                    // note: this algorithm isn't super efficient, but since it is going to be performed rarely, I saw no need in making it efficient
                    PurgeMember(i);

                    // reduce i, because we might have more members to remove!
                    i--;
                }
            }
        }

        public bool AddTerm(string strName, short index, short numRehearsals, DateTime start, DateTime end, DateTime[] rehearsalDates, double membershipFees, double[] dOtherFees = null, string[] strOtherFees = null)
        {
            if (listTerms == null) // no term has been added yet
                listTerms = new List<Term>(1);

            listTerms.Add(new Term(strName, index, numRehearsals, start, end, rehearsalDates, membershipFees, dOtherFees, strOtherFees));
            return true;

            // like adding member, always returns true
            // functionality to add false in case adding a term becomes more difficult
        }

        public string[] GetTermNames()
        {
            string[] output = new string[listTerms.Count];
            int i = 0;
            foreach (Term currentTerm in listTerms)
            {
                output[i] = currentTerm.strName;
                i++;
            }

            return output;
        }

        public string GetFormattedName(int index)
        {
            if (this.members[index].curInstrument != Member.Instrument.Other)
                return String.Format("{0}, {1}", GetFirstAndLastName(index), Member.instrumentToString(this.members[index].curInstrument));
            else
            {
                if (this.members[index].otherInstrument == null || this.members[index].otherInstrument == String.Empty)
                {
                    return String.Format("{0}", GetFirstAndLastName(index));
                }
                else
                {
                    return String.Format("{0}, {1}", GetFirstAndLastName(index), this.members[index].otherInstrument);
                }
            }
        }

        public string GetFirstAndLastName(int index)
        {
            return String.Format("{0} {1}", this.members[index].firstName, this.members[index].lastName);
        }

        /// <summary>
        /// Search for member's index by email. Returns -1 if not found.
        /// </summary>
        /// <param name="strEmail">Email Address of Member</param>
        /// <returns>An integer for the member with the given email, -1 if no member found</returns>
        public int FindMember(string strEmail)
        {
            for (int i = 0; i < iMember; i++)
                if (members[i].email == strEmail)
                    return i;
            return -1;
        }

        /// <summary>Adds an item to the club's budget </summary>
        /// <param name="val">Value of item</param>
        /// <param name="strName">Description for this budget item</param>
        /// <param name="dtDateOccur">Date of event</param>
        /// <param name="dtDateAccount">Date as per account</param>
        /// <param name="strCategory">Category of item</param>
        /// <param name="type">Whether this item is a revenue/expense/etc.</param>
        /// <param name="termIndex">Index of relevant term</param>
        /// <param name="strComment">Any comments from the user</param>
        /// <param name="asset">Index of the asset</param>
        public void AddBudget(double val, string strName, DateTime dtDateOccur, DateTime dtDateAccount, string strCategory, TransactionType type, int termIndex, string strComment, BudgetItem asset = null)
        {
            BudgetItem newItem = new BudgetItem();
            newItem.value = val;
            newItem.name = strName;
            newItem.dateOccur = dtDateOccur;
            newItem.dateAccount = dtDateAccount;
            newItem.cat = strCategory;
            newItem.type = type;
            newItem.term = termIndex;
            newItem.comment = strComment;

            // if depreciation
            if (type == TransactionType.Depreciation)
                newItem.depOfAsset = asset;

            this.budget.Add(newItem);
        }

        public void DeleteBudget(int index)
        {
            this.budget.RemoveAt(index);
        }

        /// <summary>
        /// Returns a list of the indexes of all assets
        /// </summary>
        /// <param name="withDepAssets">Whether to include assets that are depreciated</param>
        /// <returns>An array of budgetItem's</returns>
        public BudgetItem[] GetAssetList(bool withDepAssets)
        {
            List<BudgetItem> output = new List<BudgetItem>();
            foreach (BudgetItem item in this.budget)
            {
                // if asset and not fully depreciated
                if (item.type == TransactionType.Asset && (withDepAssets || !DetermineIsFullyDepreciated(item)))
                    output.Add(item);
            }

            return output.ToArray();
        }

        /// <summary>
        /// Determines if asset has any value after depreciation
        /// </summary>
        /// <param name="asset">the asset to check</param>
        /// <param name="beforeDate">Only include depreciation before this date</param>
        /// <returns>Returns true is the depreciation on the asset is at least the value of the asset</returns>
        public bool DetermineIsFullyDepreciated(BudgetItem asset, DateTime? beforeDate = null)
        {
            if (beforeDate == null)
            {
                beforeDate = DateTime.MaxValue;
            }

            // don't even to guess if the asset being depreciated hasn't been marked
            if (!ClsStorage.currentClub.budget.Contains(asset))
            {
                return false;
            }

            // sum up all of the depreciation against this asset
            double amountDepreciated = 0;
            foreach (BudgetItem currentItem in this.budget)
            {
                if (currentItem.type == TransactionType.Depreciation && currentItem.depOfAsset == asset && currentItem.dateOccur <= beforeDate)
                {
                    amountDepreciated += currentItem.value;
                }
            }

            return amountDepreciated >= asset.value;
        }

        public double CalculateValueAfterDepreciation(BudgetItem asset)
        {
            double dDep = 0;

            // sum up all of the depreciation against this asset
            foreach (BudgetItem itemIterator in this.budget)
                if (itemIterator.type == TransactionType.Depreciation && itemIterator.depOfAsset == asset)
                    dDep += itemIterator.value;
            return asset.value - dDep;
        }

        public void AddHistory(string additionalInfo, ChangeType type)
        {
            HistoryItem newItem = new HistoryItem(strCurrentUser, type, additionalInfo, DateTime.Now);
            historyList.Add(newItem);
            
            // mark unsaved changes
            ClsStorage.unsavedChanges = true;

            // if autosave is turned on, then save at this point
            if (Properties.Settings.Default.autoSave)
                Program.home.btnSave_Click(new object(), new EventArgs());
        }

        private void PurgeMember(int index)
        {
            // every member with a greater index needs to be adjusted
            // notably, the references in terms
            int termIndex;

            // first, fix the terms
            // then, move members into their new positions
            for (int i = index + 1; i < iMember; i++)
            {
                for (int j = 0; j < listTerms.Count; j++)
                {
                    // check if the member is in a term
                    termIndex = listTerms[j].memberSearch((short)i);

                    // if so, correct that members position in the term
                    if (termIndex != -1)
                        listTerms[j].members[termIndex]--;
                }

                // adjust the member's sID
                members[i].sID--;

                // adjust the member down
                members[i - 1] = members[i];
            }

            members[iMember] = null;
            iMember--;
        }
    }

    /// <summary>
    /// A budget entry tracked by Marimba
    /// </summary>
    public class BudgetItem
    {
        /// <summary>
        /// stores the magnitude of the revenue/expense
        /// </summary>
        public double value;

        /// <summary>
        /// string description of the item provided by user
        /// </summary>
        public string name;

        /// <summary>
        /// date the transaction took place
        /// </summary>
        public DateTime dateOccur;

        /// <summary>
        /// date of transaction in the actual accounts
        /// </summary>
        public DateTime dateAccount;

        /// <summary>
        /// allows users to organize transactions using string category
        /// </summary>
        public string cat;

        /// <summary>
        /// whether item is asset, depreciation, revenue, or expense
        /// </summary>
        public TransactionType type;

        /// <summary>
        /// index of the term that this transaction took place in
        /// </summary>
        public int term;

        /// <summary>
        /// record any additional information about the item
        /// </summary>
        public string comment;

        /// <summary>
        /// If depreciation, stores a reference to the asset it depreciates
        /// </summary>
        public BudgetItem depOfAsset;

        public IList<object> Export()
        {
            List<object> output = new List<object>
            {
                this.name, this.value, this.dateOccur, this.dateAccount, this.cat, this.type, this.term, this.comment
            };

            if (type == TransactionType.Depreciation)
            {
                output.Add(ClsStorage.currentClub.budget.IndexOf(this.depOfAsset));
            }

            return output;
        }
    }
}
