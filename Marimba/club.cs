using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Marimba
{
    class club
    {
        // the latest file version, and the version used to save
        public static readonly double FILE_VERSION = 2.2;
        //br and bw are for writing the main files
        public BinaryReader br;
        public static BinaryWriter bw;
        public FileStream fs;
        //aesInfo is for encryption, AES encryption; stores encryption information
        Aes aesInfo;
        //this essentially contains the club... it should be cleared once no longer needed (since it is huge)
        byte[] cipherText;
        //strName is the name of the club
        //strLocation is the location of the file
        public string strName;
        //fileVersion contains the version of the file currently loaded
        double fileVersion;
        //iUsers stores the number of users

        private static readonly int USER_FIELDS_TO_STORE = 4;
        //strUsers [,0] stores Name
        //strUsers [,1] stores password (note: encrypted, but not that well)
        //I do not recommend publicly releasing any .mrb files and use a unique password for Marimba
        //strUsers [,2] stores type of user
        //strUsers [,3] stores the key xor'd with the single hash of the user's password
        public List<string[]> strUsers;
        private static readonly string[] priviledges = { "Exec", "Admin" };

        public Int16 iMember;
        private static readonly int saltLength = 16;
        //currently, prepare for five thousand total members
        public member[] members = new member[5000];
        protected string strLocation;
        public string strCurrentUser, strCurrentUserPrivilege;

        // listTerms stores the information about the terms
        public List<term> listTerms;

        public election currentElection;

        //budget stuff
        //iBudget counts the number of items in the budget
        public List<budgetItem> budget;
        private static readonly int BUDGET_BUFFER = 50;

        //store the history 
        public List<history> historyList;
        private static readonly int HISTORY_BUFFER = 50;

        //email stuff
        public string strEmail;
        public string strImap;
        public string strSmtp;
        public bool bImap;
        public bool bSmtp;
        public int iSmtp;
        public email clubEmail;
        public enum money { Asset, Depreciation, Expense, Revenue}
        public club(string strLocation, Aes aesKey = null)
        {
            this.strLocation = strLocation;
            strName = "";
            aesInfo = aesKey;
        }

        /// <summary>
        /// Used for duplicating the club, specifically for Excel
        /// </summary>
        /// <param name="strLocation"></param>
        /// <returns></returns>
        public club clubClone(string strLocation)
        {
            return new club(strLocation, aesInfo);
        }
        public void loadClub()
        {
            //read the given file, update all of the appropriate information
            fs = new FileStream(this.strLocation, FileMode.Open);

            
            br = new BinaryReader(fs);
            fileVersion = br.ReadDouble(); //read the version number, needed for reading legacy file formats
            this.strName = br.ReadString();
            int iUser = br.ReadInt16();

            //this next part is for importing old files
            int iTempUser;
            if (fileVersion < 2)
                iTempUser = 20;
            else
                iTempUser = iUser;

            strUsers = new List<string[]>(iUser);
            for (int i = 0; i < iTempUser; i++)
            {
                string[] nextUser = new string[USER_FIELDS_TO_STORE];
                for (int j = 0; j < USER_FIELDS_TO_STORE; j++)
                {
                    nextUser[j] = br.ReadString();
                }
                strUsers.Add(nextUser);
            }
            
            //string strKey = br.ReadString();
            string strIV = br.ReadString();
            //REMOVE LATER: Unsafe, only suitable for testing
            aesInfo = Aes.Create();
            //aesInfo.Key = Convert.FromBase64String(strKey);
            aesInfo.IV = Convert.FromBase64String(strIV);

            cipherText = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length - br.BaseStream.Position));
        }

        public void loadEncryptedSection()
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



            //DECRYPT ENCRYPTED SECTION
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesInfo.Key;
                aesAlg.IV = aesInfo.IV;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            //read the membership list
                            iMember = Convert.ToInt16(srDecrypt.ReadLine());
                            for (int i = 0; i < iMember; i++)
                                members[i] = new member(srDecrypt);
                            short sTerm = Convert.ToInt16(srDecrypt.ReadLine());
                            listTerms = new List<term>(sTerm);
                            for (int i = 0; i < sTerm; i++)
                            {
                                term nextTerm = new term(srDecrypt);
                                listTerms.Add(nextTerm);
                            }
                            
                            //read the budget stuff
                            int iBudget = Convert.ToInt32(srDecrypt.ReadLine());
                            budget = new List<budgetItem>(iBudget + BUDGET_BUFFER);
                            List<int> assetToDepIndices = new List<int>(iBudget);
                            for (int i = 0; i < iBudget; i++)
                            {
                                budgetItem newItem = new budgetItem();
                                newItem.value = Convert.ToDouble(srDecrypt.ReadLine());
                                newItem.name = srDecrypt.ReadLine();
                                newItem.dateOccur = new DateTime(Convert.ToInt64(srDecrypt.ReadLine()));
                                newItem.dateAccount = new DateTime(Convert.ToInt64(srDecrypt.ReadLine()));
                                newItem.cat = srDecrypt.ReadLine();
                                newItem.type = Convert.ToInt32(srDecrypt.ReadLine());
                                newItem.term = Convert.ToInt32(srDecrypt.ReadLine());
                                newItem.comment = clsStorage.reverseCleanNewLine(srDecrypt.ReadLine());
                                budget.Add(newItem);

                                assetToDepIndices.Add(Convert.ToInt32(srDecrypt.ReadLine()));
                            }
                            int k = 0;
                            foreach (budgetItem item in budget)
                            {
                                int assetToDepIndex = assetToDepIndices[k];
                                item.depOfAsset = (assetToDepIndex == -1) ? null : budget[assetToDepIndex];
                                k++;
                            }

                            //read election
                            if (fileVersion <= 2.1)
                            {
                                bool electionSaved = Convert.ToBoolean(srDecrypt.ReadLine());
                                if (electionSaved)
                                {
                                    election currentElection = new election(srDecrypt);
                                }
                            }

                            int iHistory = Convert.ToInt32(srDecrypt.ReadLine());
                            historyList = new List<history>(iHistory + HISTORY_BUFFER);
                            for (int i = 0; i < iHistory; i++)
                            {
                                history nextItem = new history(srDecrypt);
                                historyList.Add(nextItem);
                            }

                            //read email
                            if (fileVersion >= 2)
                            {
                                strEmail = srDecrypt.ReadLine();
                                strImap = srDecrypt.ReadLine();
                                bImap = Convert.ToBoolean(srDecrypt.ReadLine());
                                strSmtp = srDecrypt.ReadLine();
                                iSmtp = Convert.ToInt32(srDecrypt.ReadLine());
                                bSmtp = Convert.ToBoolean(srDecrypt.ReadLine());
                            }
                        }
                    }
                }

                //remove cipherText from storage
                cipherText = null;
            }

            clubEmail = new email(strEmail, Properties.Settings.Default.emailPassword, strImap, bImap, strSmtp, iSmtp, bSmtp);
        }
        public void saveClub()
        {
            if (br != null)
                this.br.Close();
            fs = new FileStream(this.strLocation, FileMode.Create);
            bw = new BinaryWriter(fs);

            //this line is the file version number
            //this will be useful later on if .mrb files are siginificantly modified
            bw.Write(FILE_VERSION);
            bw.Write(strName);
            bw.Write(strUsers.Count);
            //write the users (i.e. exec account information)
            foreach (string[] user in strUsers)
            {
                for (int i = 0; i < USER_FIELDS_TO_STORE; i++)
                {
                    bw.Write(user[i]);
                }
            }
            
            //ENCRYPTED SECTION
            byte[] bEncryptedSection;
            
            //generate a new IV
            aesInfo.GenerateIV();
            bw.Write(Convert.ToBase64String(aesInfo.IV));

            //In future, set key to whatever here
            //aesAlg.Key;
            //create encryptor

            using (Aes AesEncrypt = Aes.Create())
            {
                AesEncrypt.Key = aesInfo.Key;
                AesEncrypt.IV = aesInfo.IV;
                ICryptoTransform encryptor = AesEncrypt.CreateEncryptor(AesEncrypt.Key, AesEncrypt.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            //write the members (i.e. mailing/membership list)
                            sw.WriteLine(iMember);
                            for (int i = 0; i < iMember; i++)
                            {
                                sw.WriteLine(members[i].strFName);
                                sw.WriteLine(members[i].strLName);
                                sw.WriteLine((int)members[i].type);
                                sw.WriteLine(members[i].uiStudentNumber);
                                sw.WriteLine((int)members[i].memberFaculty);
                                //if the member plays an "other" instrument, write it here
                                //write blank if the member does not play an other instrument
                                sw.WriteLine(members[i].strOtherInstrument);
                                //write the main instrument
                                sw.WriteLine((int)members[i].curInstrument);

                                //write if the member plays multiple instruments
                                //write any other instruments that the member plays (or does not play)
                                sw.WriteLine(members[i].bMultipleInstruments);
                                int numberOfInstruments = Enum.GetValues(typeof(member.instrument)).Length;
                                if(members[i].bMultipleInstruments)
                                    for (int j = 0; j < numberOfInstruments; j++)
                                        sw.WriteLine(members[i].playsInstrument[j]);

                                sw.WriteLine(members[i].strEmail);
                                sw.WriteLine(clsStorage.cleanNewLine(members[i].strOther));
                                sw.WriteLine(members[i].sID);
                                sw.WriteLine(members[i].signupTime.Ticks);
                                sw.WriteLine((int)members[i].size);
                            }
                            //write the terms
                            sw.WriteLine(listTerms.Count);
                            //loop through the terms
                            foreach (term currentTerm in listTerms)
                            {
                                currentTerm.saveTerm(sw);
                            }
                            //save the budget
                            sw.WriteLine(budget.Count);
                            foreach (budgetItem item in budget)
                            {
                                sw.WriteLine(item.value);
                                sw.WriteLine(item.name);
                                sw.WriteLine(item.dateOccur.Ticks);
                                sw.WriteLine(item.dateAccount.Ticks);
                                sw.WriteLine(item.cat);
                                sw.WriteLine(item.type);
                                sw.WriteLine(item.term);
                                sw.WriteLine(clsStorage.cleanNewLine(item.comment));
                                sw.WriteLine(budget.IndexOf(item.depOfAsset));
                            }

                            //save history
                            sw.WriteLine(historyList.Count);
                            foreach (history item in historyList)
                            {
                                item.saveHistory(sw);
                            }
                            
                            //save email details
                            sw.WriteLine(strEmail);
                            sw.WriteLine(strImap);
                            sw.WriteLine(bImap);
                            sw.WriteLine(strSmtp);
                            sw.WriteLine(iSmtp);
                            sw.WriteLine(bSmtp);


                            //finally, write memorystream to array
                            //then convert array into a string
                            //write string to a file

                        }
                        bEncryptedSection = ms.ToArray();
                        bw.Write(bEncryptedSection);
                    }
                }
            }

            bw.Close();
            fs.Close();

            //string strTest = null;

            //strTest = DecryptStringFromBytes_Aes(bEncryptedSection, aesInfo.Key, aesInfo.IV);
            
            
            //reopen the binary reader to prevent anyone else from editing the file
            this.br = new BinaryReader(new FileStream(this.strLocation, FileMode.Open));
        }

        public void saveClub(string strLocation)
        {
            //change the location, and then save
            this.strLocation = strLocation;
            saveClub();
        }

        // add this user with the given name, password and privilege level
        public bool addUser(string strName, string strPassword, string strPrivileges)
        {
            //see if a user with this name already exists
            if (findUser(strName) == null)
                return false;

            if (Array.IndexOf(priviledges, strPrivileges) < 0)
            {
                return false;
            }
            //do a basic encryption on the password
            //the intention is just so that no one can read plaintext passwords
            //I am well aware this algorithm isn't particularly strong, but it is sufficient for our needs
            SHA256 shaHash = SHA256.Create();

            //salt
            byte[] salt = new byte[saltLength];
            int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
            byte[] saltPlusPassword = new byte[saltLength + passwordLength];

            //generate salt
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }

            //combine the salt and password
            Array.Copy(salt, saltPlusPassword, saltLength);
            Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, saltLength, passwordLength);

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

            string[] newUser = new string[USER_FIELDS_TO_STORE];
            newUser[0] = strName;
            newUser[1] = bytesToHex(salt) + "$" + bytesToHex(data);
            newUser[2] = strPrivileges;
            newUser[3] = Convert.ToBase64String(clsStorage.byteXOR(aesInfo.Key, shaHash.ComputeHash(saltPlusPassword)));
            strUsers.Add(newUser);
            return true;
        }

        // remove the user with the given name from Marimba
        public bool deleteUser(string strName)
        {
            string[] user = findUser(strName);
            if (user == null)
            {
                return false;
            }
            strUsers.Remove(user);

            return true;
        }

        // get the index of the specified user
        public string[] findUser(string strName)
        {
            int i = 0;
            //first, find the user
            foreach (string[] user in strUsers)
            {
                if (user[0].Equals(strName))
                {
                    return user;
                }
                i++;
            }
            //did not find user
            return null;
        }

        // returns true if the login was successful, false otherwise
        public bool loginUser(string strName, string strPassword)
        {
            string[] user = findUser(strName);
            if (user == null)
                return false;
            else
            {
                //create a hash of the password and compare
                SHA256 shaHash = SHA256.Create();
                //MD5 md5hash = MD5.Create();
                // Convert the input string to a byte array and compute the hash. 
                
                
                //retrieve the salt
                byte[] salt = StringToByteArray(user[1].Split('$')[0]);
                //retrieve the hash
                string hash = user[1].Split('$')[1];

                //calculate hash of salt + password
                int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
                byte[] saltPlusPassword = new byte[saltLength + passwordLength];
                Array.Copy(salt, saltPlusPassword, saltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, saltLength, passwordLength);

                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                if (StringComparer.OrdinalIgnoreCase.Compare(hash, bytesToHex(data)) == 0)
                {
                    this.strCurrentUser = strName;
                    this.strCurrentUserPrivilege = user[2];
                    try
                    {
                        this.aesInfo.Key = clsStorage.byteXOR(Convert.FromBase64String(user[3]), shaHash.ComputeHash(saltPlusPassword));
                    }
                    catch
                    {
                        if (Properties.Settings.Default.playSounds)
                            sound.error.Play();
                        System.Windows.Forms.MessageBox.Show("Username and password are correct, but key is corrupted. Unable to open file.");
                        return false;
                    }
                    return true;
                }
                else
                    return false;
            }
        }

        public bool editUser(string strName, string strPassword, string strNewPassword)
        {
            //check user exists and current password is correct
            if (loginUser(strName, strPassword))
            {
                string[] user = findUser(strName);
                //replace the old password with the new password
                SHA256 shaHash = SHA256.Create();
                //salt
                byte[] salt = new byte[saltLength];
                int passwordLength = Encoding.UTF8.GetBytes(strNewPassword).Length;
                byte[] saltPlusPassword = new byte[saltLength + passwordLength];

                //generate salt
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetBytes(salt);
                }

                // combine the salt and password
                Array.Copy(salt, saltPlusPassword, saltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strNewPassword), 0, saltPlusPassword, saltLength, passwordLength);

                // Convert the input string to a byte array and compute the hash.
                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                user[1] = bytesToHex(salt) + "$" + bytesToHex(data);

                //add the key used to encrypted the files here
                
                user[3] = Convert.ToBase64String(clsStorage.byteXOR(aesInfo.Key, shaHash.ComputeHash(saltPlusPassword)));

                return true;
            }
            else
                return false;
        }

        public bool editUserPrivilege(string strName, string strNewPrivilege)
        {
            string[] user = findUser(strName);
            if (user == null)
            {
                return false;
            }

            user[2] = strNewPrivilege;
            return true;
        }

        public void updateKey()
        {
            //first, generate a new key
            Aes newKey = Aes.Create();
            
            //next, update the key access everyone has
            //NOTE: We reset everyone's password; for now to the default of being the club name
            //An admin should go in and change everyone's password to something else
            //The key would be updated to prevent a person who previously had access from having access again
            SHA256 shaHash = SHA256.Create();
            byte[] data;
            byte[] salt = new byte[saltLength];
            int passwordLength = Encoding.UTF8.GetBytes(strName).Length;
            byte[] saltPlusPassword = new byte[saltLength + passwordLength];
            
            foreach (string[] user in strUsers)
            {
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    //generate salt
                    rngCsp.GetBytes(salt);
                }

                //combine the salt and password
                Array.Copy(salt, saltPlusPassword, saltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strName), 0, saltPlusPassword, saltLength, passwordLength);

                data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                //build hash
                user[1] = bytesToHex(salt) + "$" + bytesToHex(data);

                user[3] = Convert.ToBase64String(clsStorage.byteXOR(shaHash.ComputeHash(saltPlusPassword), newKey.Key));
            }

            //finally, update key
            aesInfo.Key = newKey.Key;
        }

        /// <summary>
        /// Converts a byte array into a hexadecimal string
        /// </summary>
        /// <param name="byteArray">Array of bytes</param>
        /// <returns>String in hexadecimal</returns>
        public static string bytesToHex(byte[] byteArray)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int j = 0; j < byteArray.Length; j++)
                sBuilder.Append(byteArray[j].ToString("x2"));
            return sBuilder.ToString();
        }

        /// <summary>
        /// Reverses bytesToHex
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public bool addMember(string strFName, string strLName, int iType, uint uiID, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int iShirt)
        {
            //before adding, check if it is a duplicate member
            //a matching student number or email address will be the judge of this
            //if it is a duplicate, then update the member's profile
            for (int i = 0; i < iMember; i++)
                if (this.members[i].strEmail == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].editMember(strFName, strLName, iType, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, iShirt);
                    return false;
                }
            this.members[iMember] = new member(strFName, strLName, iType, uiID, iFaculty, strInstrument, strOtherInstrument, strEmail, strOther, iShirt);
            iMember++;
            return true;
        }

        public bool addMember(string strFName, string strLName, int iType, uint uiID, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int iShirt, bool[] bInstruments)
        {
            //before adding, check if it is a duplicate member
            //a matching student number or email address will be the judge of this
            //if it is a duplicate, then update the member's profile
            for (int i = 0; i < iMember; i++)
                if (this.members[i].strEmail == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].editMember(strFName, strLName, iType, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, iShirt);
                    return false;
                }
            this.members[iMember] = new member(strFName, strLName, iType, uiID, iFaculty, strInstrument, strOtherInstrument, strEmail, strOther, iShirt, bInstruments);
            iMember++;
            return true;
        }

        //this version is kept for legacy purposes to open old data
        public bool addMember(string strFName, string strLName, int iType, uint uiID, int iFaculty, string strInstrument, string strEmail, string strOther, DateTime signup)
        {
            //before adding, check if it is a duplicate member
            //a matching student number or email address will be the judge of this
            for (int i = 0; i < iMember; i++)
                if (this.members[i].strEmail == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].editMember(strFName, strLName, iType, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, -1);
                    return false;
                }
            this.members[iMember] = new member(strFName, strLName, iType, uiID, iFaculty, strInstrument, strEmail, strOther, signup, -1);
            iMember++;
            return true;
        }

        /// <summary>
        /// Removes members who have not attended a single rehearsal in 4 years from the club mailing list
        /// </summary>
        public void purgeOldMembers()
        {
            bool attendedOneRehearsal;
            //for each member, check if they have attended any rehearsals
            //we aren't getting rid of members who have in fact attended a rehearsal and were once a member
            //this is to prevent the mailing list from getting too cluttered
            for(int i = 0; i < iMember; i++)
            {
                attendedOneRehearsal = false;
                //check each term to confirm they are not in any of them
                for (int j = 0; j < listTerms.Count && !attendedOneRehearsal; j++)
                {
                    attendedOneRehearsal = attendedOneRehearsal || listTerms[j].memberSearch((short)i) != -1;
                }

                //if they haven't attended any rehearsals, next check if they have been on the list for four years (1461 days)
                //just keep using the same attendedOneRehearsal variable
                attendedOneRehearsal = attendedOneRehearsal || TimeSpan.Compare(DateTime.Now - members[i].signupTime, TimeSpan.FromDays(1461)) < 0;

                //if they didn't meet at least one of these requirements... they gotta go!
                if (!attendedOneRehearsal)
                {
                    //bye bye!

                    //note: this algorithm isn't super efficient, but since it is going to be performed rarely, I saw no need in making it efficient
                    purgeMember(i);
                    //reduce i, because we might have more members to remove!
                    i--;
                }
            }
        }

        private void purgeMember(int index)
        {
            //every member with a greater index needs to be adjusted
            //notably, the references in terms

            int iTermIndex;
            //first, fix the terms
            //then, move members into their new positions
            for (int i = index + 1; i < iMember; i++)
            {
                for (int j = 0; j < listTerms.Count; j++)
                {
                    //check if the member is in a term
                    iTermIndex = listTerms[j].memberSearch((short)i);
                    //if so, correct that members position in the term
                    if(iTermIndex!=-1)
                        listTerms[j].members[iTermIndex]--;
                }
                //adjust the member's sID
                members[i].sID--;

                //adjust the member down
                members[i - 1] = members[i];
            }
            members[iMember] = null;
            iMember--;

        }

        public bool addTerm(string strName, short index, short numRehearsals, DateTime start, DateTime end, DateTime[] rehearsalDates, double membershipFees, double[] dOtherFees = null, string[] strOtherFees = null)
        {
            if (listTerms == null) //no term has been added yet
                listTerms = new List<term>(1);

            listTerms.Add(new term(strName, index, numRehearsals, start, end, rehearsalDates, membershipFees, dOtherFees, strOtherFees));
            return true;

            //like adding member, always returns true
            //functionality to add false in case adding a term becomes more difficult
        }

        public string[] termNames()
        {
            string[] output = new string[listTerms.Count];
            int i = 0;
            foreach (term currentTerm in listTerms)
            {
                output[i] = currentTerm.strName;
                i++;
            }
            return output;
        }

        public string formatedName(int index)
        {
            if(this.members[index].curInstrument != member.instrument.other)
                return String.Format("{0}, {1}", firstAndLastName(index), member.instrumentToString(this.members[index].curInstrument));
            else
            {
                if (this.members[index].strOtherInstrument == null || this.members[index].strOtherInstrument == "")
                {
                    return String.Format("{0}", firstAndLastName(index));
                }
                else
                {
                    return String.Format("{0}, {1}", firstAndLastName(index), this.members[index].strOtherInstrument);
                }
            }
        }

        public string firstAndLastName(int index)
        {
            return String.Format("{0} {1}", this.members[index].strFName, this.members[index].strLName);
        }

        /// <summary>
        /// Search for member's index by email. Returns -1 if not found.
        /// </summary>
        /// <param name="strEmail">Email Address of Member</param>
        public int emailSearch(string strEmail)
        {
            for(int i = 0; i < iMember; i++)
                if (members[i].strEmail == strEmail)
                    return i;
            return -1;
        }

        /// <summary>
        /// Adds an item to the club's budget
        /// </summary>
        /// <param name="val">Value of item</param>
        /// <param name="strName">Description</param>
        /// <param name="dtDateOccur">Date of event</param>
        /// <param name="dtDateAccount">Date as per account</param>
        /// <param name="strCategory">Category of item</param>
        /// <param name="iType">Number indicating whether revenue/expense/etc.</param>
        /// <param name="iTerm">Index of relevant term</param>
        /// <param name="strComment">Any comments from the user</param>
        /// <param name="assetIndex">Index of the asset</param>
        public void addBudget(double val, string strName, DateTime dtDateOccur, DateTime dtDateAccount, string strCategory, int iType, int iTerm, string strComment, budgetItem asset = null)
        {
            budgetItem newItem = new budgetItem();
            newItem.value = val;
            newItem.name = strName;
            newItem.dateOccur = dtDateOccur;
            newItem.dateAccount = dtDateAccount;
            newItem.cat = strCategory;
            newItem.type = iType;
            newItem.term = iTerm;
            newItem.comment = strComment;
            //if depreciation
            if (iType == 1)
                newItem.depOfAsset = asset;
            budget.Add(newItem);
        }

        public void deleteBudget(int index)
        {
            budget.RemoveAt(index);
        }

        /// <summary>
        /// Returns a list of the indexes of all assets
        /// </summary>
        /// <param name="withDepAssets">Whether to include assets that are depreciated</param>
        /// <returns></returns>
        public budgetItem[] assetList(bool withDepAssets)
        {
            List<budgetItem> output = new List<budgetItem>();
            foreach (budgetItem item in budget)
                //if asset and not fully depreciated
                if (item.type == 0 && (withDepAssets || !fullyDepreciatedAsset(item)))
                    output.Add(item);
            return output.ToArray();
        }

        /// <summary>
        /// Determines if asset has any value after depreciation
        /// </summary>
        /// <param name="asset">the asset to check</param>
        /// <param name="beforeDate">Only include depreciation before this date</param>
        /// <returns>Returns true is the depreciation on the asset is at least the value of the asset</returns>
        public bool fullyDepreciatedAsset(budgetItem asset, DateTime? beforeDate = null)
        {
            if (beforeDate == null)
            {
                beforeDate = DateTime.MaxValue;
            }

            //don't even to guess if the asset being depreciated hasn't been marked
            if (!clsStorage.currentClub.budget.Contains(asset))
            {
                return false;
            }

            // sum up all of the depreciation against this asset
            double amountDepreciated = 0;
            foreach (budgetItem currentItem in budget)
            {
                if (currentItem.type == 1 && currentItem.depOfAsset == asset && currentItem.dateOccur <= beforeDate)
                {
                    amountDepreciated += currentItem.value;
                }
            }

            return amountDepreciated >= asset.value;
        }

        public double valueAfterDepreciation(budgetItem asset)
        {
            double dDep = 0;
            //sum up all of the depreciation against this asset
            foreach (budgetItem itemIterator in budget)
                if (itemIterator.type == 1 && itemIterator.depOfAsset == asset)
                    dDep += itemIterator.value;
            return asset.value - dDep;
        }

        public void addHistory(string additionalInfo, history.changeType type)
        {
            history newItem = new history(strCurrentUser, type, additionalInfo, DateTime.Now);
            historyList.Add(newItem);
            
            //mark unsaved changes
            clsStorage.unsavedChanges = true;

            //if autosave is turned on, then save at this point
            if(Properties.Settings.Default.autoSave)
                Program.home.btnSave_Click(new object(), new EventArgs());
        }
    }

    public class budgetItem
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
        public int type;

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
        public budgetItem depOfAsset;

        public IList<object> Export() {
            List<object> output = new List<object> {
                name, value, dateOccur, dateAccount, cat, type, term, comment
            };

            if (type == 1)
            {
                output.Add(clsStorage.currentClub.budget.IndexOf(depOfAsset));
            }

            return output;
        }
    }
}
