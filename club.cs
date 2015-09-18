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
        //fileVersion contains the version of the file
        double fileVersion;
        //iUsers stores the number of users
        //strUsers [,1] stores Name
        //strUsers [,2] stores password (note: encrypted, but not that well)
        //I do not recommend publicly releasing any .mrb files and use a unique password for Marimba
        //strUsers [,3] stores type of user
        //strUsers [,4] stores the key xor'd with the single hash of the user's password
        public Int16 iUser, iMember;
        public string[,] strUsers = new string[40,4];
        //currently, prepare for five thousand total members
        public member[] members = new member[5000];
        protected string strLocation;
        public string strUser, strPrivilege;
        //terms!
        //sTerm is the number of terms
        public short sTerm;
        //terms stores the information about the terms
        public term[] terms;
        //budget stuff
        //we'll start with 20000 atomic entries for now
        //iBudget counts the number of items in the budget
        public int iBudget;
        public budgetItem[] budget = new budgetItem[20000];
        //currentElection is self-explanatory
        public election currentElection;
        public bool electionSaved;
        //store the history 
        public int iHistory;
        public history[] historyList = new history[50000];

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
            iUser = 0;
            for (int i = 0; i < 20; i++)
            {
                strUsers[i, 0] = "";
                strUsers[i, 1] = "";
                strUsers[i, 2] = "";
                strUsers[i, 3] = "";
            }
            sTerm = 0;
            iBudget = 0;
            electionSaved = false;
            iHistory = 0;
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
            iUser = br.ReadInt16();

            //this next part is for importing old files
            int iTempUser;
            if (fileVersion < 2)
                iTempUser = 20;
            else
                iTempUser = iUser;

            for (int i = 0; i < iTempUser; i++)
            {
                strUsers[i, 0] = br.ReadString();
                strUsers[i, 1] = br.ReadString();
                strUsers[i, 2] = br.ReadString();
                strUsers[i, 3] = br.ReadString();
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
                            sTerm = Convert.ToInt16(srDecrypt.ReadLine());
                            terms = new term[sTerm];
                            for (int i = 0; i < sTerm; i++)
                                terms[i] = new term(srDecrypt);
                            //read the budget stuff
                            this.iBudget = Convert.ToInt32(srDecrypt.ReadLine());
                            for (int i = 0; i < iBudget; i++)
                            {
                                budget[i].value = Convert.ToDouble(srDecrypt.ReadLine());
                                budget[i].name = srDecrypt.ReadLine();
                                budget[i].dateOccur = new DateTime(Convert.ToInt64(srDecrypt.ReadLine()));
                                budget[i].dateAccount = new DateTime(Convert.ToInt64(srDecrypt.ReadLine()));
                                budget[i].cat = srDecrypt.ReadLine();
                                budget[i].type = Convert.ToInt32(srDecrypt.ReadLine());
                                budget[i].term = Convert.ToInt32(srDecrypt.ReadLine());
                                budget[i].comment = clsStorage.reverseCleanNewLine(srDecrypt.ReadLine());
                                budget[i].depOfAsset = Convert.ToInt32(srDecrypt.ReadLine());
                            }
                            //read election
                            electionSaved = Convert.ToBoolean(srDecrypt.ReadLine());
                            if (electionSaved)
                                currentElection = new election(srDecrypt);
                            iHistory = Convert.ToInt32(srDecrypt.ReadLine());
                            for (int i = 0; i < iHistory; i++)
                                historyList[i] = new history(srDecrypt);

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

            /*
            //read the membership list
            iMember = br.ReadInt16();
            if (fileVersion < 2)
            {
                for (int i = 0; i < iMember; i++)
                {
                    members[i] = new member(br.ReadString(), br.ReadString(), br.ReadInt32(), br.ReadUInt32(), br.ReadInt32(),
                        br.ReadString(), br.ReadString(), br.ReadString(), br.ReadInt16(), new DateTime(br.ReadInt64()), br.ReadInt32());
                }
            }
            else 
            {
                for (int i = 0; i < iMember; i++)
                    members[i] = new member(br);
            }
            sTerm = br.ReadInt16();
            terms = new term[sTerm];
            for (int i = 0; i < sTerm; i++)
                terms[i] = new term(br);
            //read the budget stuff
            this.iBudget = br.ReadInt32();
            for (int i = 0; i < iBudget; i++)
            {
                budget[i].value = br.ReadDouble();
                budget[i].name = br.ReadString();
                budget[i].dateOccur = new DateTime(br.ReadInt64());
                budget[i].dateAccount = new DateTime(br.ReadInt64());
                budget[i].cat = br.ReadString();
                budget[i].type = br.ReadInt32();
                budget[i].term = br.ReadInt32();
                budget[i].comment = br.ReadString();

                if (fileVersion < 2)
                    budget[i].depOfAsset = -1;
                else
                    budget[i].depOfAsset = br.ReadInt32();
            }
            //read election
            electionSaved = br.ReadBoolean();
            if (electionSaved)
                currentElection = new election(br);
            iHistory = br.ReadInt32();
            for (int i = 0; i < iHistory; i++)
                historyList[i] = new history(br);

            //read email
            if (fileVersion >= 2)
            {
                strEmail = br.ReadString();
                strImap = br.ReadString();
                bImap = br.ReadBoolean();
                strSmtp = br.ReadString();
                iSmtp = br.ReadInt32();
                bSmtp = br.ReadBoolean();
            }*/

            clubEmail = new email(strEmail, Properties.Settings.Default.emailPassword, strImap, bImap, strSmtp, iSmtp, bSmtp);
        }
        public void saveClub()
        {
            if (br != null)
                this.br.Close();
            fs = new FileStream(this.strLocation, FileMode.Create);
            bw = new BinaryWriter(fs);

            //this line is the version number (currently 2)
            //this will be useful later on if .mrb files are siginificantly modified
            bw.Write(Convert.ToDouble(2.1));
            bw.Write(strName);
            bw.Write(iUser);
            //write the users (i.e. exec account information)
            for (int i = 0; i < iUser; i++)
                for(int j = 0; j < 4; j++)
                    bw.Write(strUsers[i, j]);
            
            //ENCRYPTED SECTION
            byte[] bEncryptedSection;
            
            //generate a new IV
            aesInfo.GenerateIV();
            bw.Write(Convert.ToBase64String(aesInfo.IV));

            //bw.Close();
            //fs.Close();

            //In future, set key to whatever here
            //aesAlg.Key;
            //create encryptor

            using (Aes AesEncrypt = Aes.Create())
            {
                AesEncrypt.Key = aesInfo.Key;
                AesEncrypt.IV = aesInfo.IV;
                ICryptoTransform encryptor = AesEncrypt.CreateEncryptor(AesEncrypt.Key, AesEncrypt.IV);
                //fs = new FileStream(this.strLocation, FileMode.Append);
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
                            //wish me luck in writing this
                            sw.WriteLine(sTerm);
                            //loop through the terms
                            for (int i = 0; i < sTerm; i++)
                            {
                                terms[i].saveTerm(sw);
                            }
                            //save the budget
                            sw.WriteLine(iBudget);
                            for (int i = 0; i < iBudget; i++)
                            {
                                sw.WriteLine(budget[i].value);
                                sw.WriteLine(budget[i].name);
                                sw.WriteLine(budget[i].dateOccur.Ticks);
                                sw.WriteLine(budget[i].dateAccount.Ticks);
                                sw.WriteLine(budget[i].cat);
                                sw.WriteLine(budget[i].type);
                                sw.WriteLine(budget[i].term);
                                sw.WriteLine(clsStorage.cleanNewLine(budget[i].comment));
                                sw.WriteLine(budget[i].depOfAsset);
                            }
                            //save the election
                            sw.WriteLine(electionSaved);
                            if (electionSaved)
                                currentElection.saveElection(sw);
                            //save history
                            sw.WriteLine(iHistory);
                            for (int i = 0; i < iHistory; i++)
                                historyList[i].saveHistory(sw);
                            
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

        /// <summary>
        /// Given two clubs, merges attendance, sign-up information, and history
        /// </summary>
        /// <param name="mainFile">Main club</param>
        /// <param name="slaveFile">File to be merged in</param>
        /// <returns>Merged club</returns>
        public static club mergeClub(club mainFile, club slaveFile)
        {
            //NOTE TO SELF: improve checking terms
            //Check that terms indeed correspond, not just that they match
               
            //first, merge sign-ups
            //existing sign-ups remain unchanged
            //we'll use a binary search type thing to find where the members no longer match up
            int commonMember = member.lastCommonMember(0, Math.Min(mainFile.iMember-1,slaveFile.iMember-1), mainFile.members, slaveFile.members);
            //now add members to the current club
            for (int i = 0; i < slaveFile.iMember-commonMember-1; i++)
            {
                mainFile.members[mainFile.iMember] = slaveFile.members[commonMember + 1 + i];
                mainFile.members[mainFile.iMember].sID = Convert.ToInt16(mainFile.iMember);
                mainFile.iMember++;
            }
            //next, we need to do a similar thing, just with terms
            //first, make the list of members registered in each term the same, then do their attendance
            for (int i = 0; i < mainFile.sTerm; i++)
            {
                int termCommonMember = term.lastCommonMember(0, Math.Min(mainFile.terms[i].sMembers-1, slaveFile.terms[i].sMembers-1), mainFile.terms[i], slaveFile.terms[i]);
                //first, update the attendance for common members
                for (int j = 0; j <= termCommonMember; j++)
                    for (int k = 0; k < slaveFile.terms[i].sRehearsals; k++)
                        //if they are signed in on either of the files, they will be signed in on the merged.
                        mainFile.terms[i].attendance[j, k] = mainFile.terms[i].attendance[j, k] || slaveFile.terms[i].attendance[j, k];
                for (int j = 0; j < slaveFile.terms[i].sMembers - termCommonMember - 1; j++)
                {
                    //check if we are adding a new member from one of the files
                    //in that case, they need a special number
                    if (slaveFile.terms[i].members[termCommonMember + 1 + j]<=commonMember)
                        mainFile.terms[i].members[mainFile.terms[i].sMembers] = slaveFile.terms[i].members[termCommonMember + 1 + j];
                    else
                        mainFile.terms[i].members[mainFile.terms[i].sMembers] =(short)(mainFile.iMember + slaveFile.terms[i].members[termCommonMember + 1 + j] - slaveFile.iMember);
                    for (int k = 0; k < slaveFile.terms[i].sRehearsals; k++)
                        mainFile.terms[i].attendance[mainFile.terms[i].sMembers, k] = slaveFile.terms[i].attendance[termCommonMember + 1 + j, k];
                    //for now, not merging feespaid
                    mainFile.terms[i].sMembers++;
                }
            }
            //next, merge history in a similar way
            int commonHistory = history.lastCommonHistory(0, Math.Min(mainFile.iHistory-1,slaveFile.iHistory-1), mainFile.historyList, slaveFile.historyList);
            for (int j = 0; j < slaveFile.iHistory - commonHistory - 1; j++)
            {
                //since we aren't merging anything besides sign-in and sign-up, it wouldn't make sense to also store
                //unsaved history, so only copy history of changes that are being copied
                if (slaveFile.historyList[commonHistory + 1 + j].type == history.changeType.signin ||
                    slaveFile.historyList[commonHistory + 1 + j].type == history.changeType.signup ||
                    slaveFile.historyList[commonHistory + 1 + j].type == history.changeType.importMembers)
                {
                    mainFile.historyList[mainFile.iHistory] = slaveFile.historyList[commonHistory + 1 + j];
                    mainFile.iHistory++;
                }
            }
            //sort the history by time
            mainFile.historyList = history.sortHistory(0, mainFile.iHistory - 1, mainFile.historyList);
            //for now, not doing anything more, like merging budgets
            return mainFile;
        }

        public bool addUser(string strName, string strPassword, string strPrivileges)
        {
            //see if a user with this name already exists
            if (findUser(strName) >= 0)
                return false;
            this.strUsers[iUser, 0] = strName;
            //do a basic encryption on the password
            //the intention is just so that no one can read plaintext passwords
            //I am well aware this algorithm isn't particularly strong, but it is sufficient for our needs
            SHA256 shaHash = SHA256.Create();

            //salt
            byte[] salt = new byte[4];
            int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
            byte[] saltPlusPassword = new byte[4 + passwordLength];

            //generate salt
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(salt);
            //combine the salt and password
            Array.Copy(salt, saltPlusPassword, 4);
            Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, 4, passwordLength);

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(Encoding.UTF8.GetBytes(strPassword)));

            this.strUsers[iUser, 1] = bytesToHex(data);
            this.strUsers[iUser, 2] = strPrivileges;
            this.strUsers[iUser, 3] = Convert.ToBase64String(clsStorage.byteXOR(aesInfo.Key, shaHash.ComputeHash(Encoding.UTF8.GetBytes(strPassword))));
            iUser++;
            return true;
        }

        public int findUser(string strName)
        {
            int i = 0;
            //first, find the user
            for (i = 0; i < 20; i++)
            {
                if (this.strUsers[i, 0] == strName)
                    return i;
            }
            //did not find user
            return -1;
        }

        public bool loginUser(string strName, string strPassword)
        {
            int i = findUser(strName);
            if (i == -1)
                return false;
            else
            {
                //create a hash of the password and compare
                SHA256 shaHash = SHA256.Create();
                //MD5 md5hash = MD5.Create();
                // Convert the input string to a byte array and compute the hash. 
                
                
                //retrieve the salt
                byte[] salt = StringToByteArray(strUsers[i, 1].Split('$')[0]);
                //retrieve the hash
                string hash = strUsers[i, 1].Split('$')[1];

                //calculate hash of salt + password
                int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
                byte[] saltPlusPassword = new byte[16 + passwordLength];
                Array.Copy(salt, saltPlusPassword, 16);
                Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, 16, passwordLength);

                //byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(Encoding.UTF8.GetBytes(strPassword)));
                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));
                //byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(strPassword));

                if (StringComparer.OrdinalIgnoreCase.Compare(hash, bytesToHex(data)) == 0)
                //if (StringComparer.OrdinalIgnoreCase.Compare(strUsers[i, 1], bytesToHex(data)) == 0)
                {
                    this.strUser = strName;
                    this.strPrivilege = this.strUsers[i, 2];
                    try
                    {
                        //this.aesInfo.Key = clsStorage.byteXOR(Convert.FromBase64String(strUsers[i, 3]), shaHash.ComputeHash(Encoding.UTF8.GetBytes(strPassword)));
                        this.aesInfo.Key = clsStorage.byteXOR(Convert.FromBase64String(strUsers[i, 3]), shaHash.ComputeHash(saltPlusPassword));
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
                int userIndex = findUser(strName);
                //replace the old password with the new password
                SHA256 shaHash = SHA256.Create();
                //salt
                byte[] salt = new byte[16];
                int passwordLength = Encoding.UTF8.GetBytes(strNewPassword).Length;
                byte[] saltPlusPassword = new byte[16 + passwordLength];

                //generate salt
                RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
                rngCsp.GetBytes(salt);
                //combine the salt and password
                Array.Copy(salt, saltPlusPassword, 16);
                Array.Copy(Encoding.UTF8.GetBytes(strNewPassword), 0, saltPlusPassword, 16, passwordLength);

                // Convert the input string to a byte array and compute the hash. 
                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                this.strUsers[userIndex, 1] = bytesToHex(salt) + "$" + bytesToHex(data);

                //add the key used to encrypted the files here
                
                this.strUsers[userIndex, 3] = Convert.ToBase64String(clsStorage.byteXOR(aesInfo.Key, shaHash.ComputeHash(saltPlusPassword)));

                return true;
            }
            else
                return false;
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
            byte[] salt = new byte[16];
            int passwordLength = Encoding.UTF8.GetBytes(strName).Length;
            byte[] saltPlusPassword = new byte[16 + passwordLength];

            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            for (int i = 0; i < iUser; i++)
            {
                //generate salt
                rngCsp.GetBytes(salt);
                //combine the salt and password
                Array.Copy(salt, saltPlusPassword, 16);
                Array.Copy(Encoding.UTF8.GetBytes(strName), 0, saltPlusPassword, 16, passwordLength);

                data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                //build hash
                strUsers[i, 1] = bytesToHex(salt) + "$" + bytesToHex(data);

                strUsers[i, 3] = Convert.ToBase64String(clsStorage.byteXOR(shaHash.ComputeHash(saltPlusPassword), newKey.Key));
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
                for (int j = 0; j < sTerm; j++)
                    attendedOneRehearsal = attendedOneRehearsal || terms[j].memberSearch((short)i) != -1;

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
                for (int j = 0; j < sTerm; j++)
                {
                    //check if the member is in a term
                    iTermIndex = terms[j].memberSearch((short)i);
                    //if so, correct that members position in the term
                    if(iTermIndex!=-1)
                        terms[j].members[iTermIndex]--;
                }
                //adjust the member's sID
                members[i].sID--;

                //adjust the member down
                members[i - 1] = members[i];
            }
            members[iMember] = null;
            iMember--;

        }

        public bool addTerm(string strName, short index, short numRehearsals, DateTime start, DateTime end, DateTime[] rehearsalDates, double membershipFees, double[] dOtherFees, string[] strOtherFees)
        {
            if (sTerm == 0) //no term has been added yet
                terms = new term[1];
            //make the array of terms bigger otherwise
            //the inefficiency of redeclaring the array is made up for by the fact that storing a term is a lot of data
            //this is the most efficient means of doing this
            else
                Array.Resize(ref terms, sTerm + 1);
            terms[sTerm] = new term(strName, index, numRehearsals, start, end, rehearsalDates, membershipFees, dOtherFees, strOtherFees);
            sTerm++;
            return true;
            //like adding member, always returns true
            //functionality to add false in case adding a term becomes more difficult
        }

        public bool addTerm(string strName, short index, short numRehearsals, DateTime start, DateTime end, DateTime[] rehearsalDates, double membershipFees)
        {
            //this version is if there are no other fees
            if (sTerm == 0) //no term has been added yet
                terms = new term[1];
            //make the array of terms bigger otherwise
            //the inefficiency of redeclaring the array is made up for by the fact that storing a term is a lot of data
            //this is the most efficient means of doing this
            else
                Array.Resize(ref terms, sTerm + 1);
            terms[sTerm] = new term(strName, index, numRehearsals, start, end, rehearsalDates, membershipFees);
            sTerm++;
            return true;
            //like adding member, always returns true
            //functionality to add false in case adding a term becomes more difficult
        }

        public string[] termNames()
        {
            string[] output = new string[sTerm];
            for (int i = 0; i < sTerm; i++)
                output[i] = terms[i].strName;
            return output;
        }

        public string formatedName(int index)
        {
            if(this.members[index].curInstrument != member.instrument.other)
                return String.Format("{0} {1}, {2}", this.members[index].strFName, this.members[index].strLName, member.instrumentToString(this.members[index].curInstrument));
            else
                return String.Format("{0} {1}, {2}", this.members[index].strFName, this.members[index].strLName, this.members[index].strOtherInstrument);
        }

        public string firstAndLastName(int index)
        {
            return String.Format("{0} {1}", this.members[index].strFName, this.members[index].strLName);
        }

        /// <summary>
        /// Creates a mailing list of email addresses
        /// </summary>
        /// <param name="iTerm">Term index. Enter -1 for all terms.</param>
        /// <returns>A string array contianing email addresses</returns>
        public string[] mailingList(int iTerm)
        {
            string[] output;
            if (iTerm == -1)
            {
                output = new string[this.iMember];
                for (int i = 0; i < this.iMember; i++)
                    output[i] = members[i].strEmail;
            }
            else
            {
                output = new string[terms[iTerm].sMembers];
                for (int i = 0; i < terms[iTerm].sMembers; i++)
                    output[i] = members[terms[iTerm].members[i]].strEmail;
            }
            return output;
        }

        /// <summary>
        /// Creates a list of names of members of a term
        /// </summary>
        /// <param name="iTerm">The term index. Use -1 for all terms.</param>
        /// <returns>A string array containing the names of the members</returns>
        public string[] memberList(int iTerm)
        {
            string[] output;
            if (iTerm == -1)
            {
                output = new string[this.iMember];
                for (int i = 0; i < this.iMember; i++)
                    output[i] = formatedName(i);
            }
            else
            {
                output = new string[terms[iTerm].sMembers];
                for (int i = 0; i < terms[iTerm].sMembers; i++)
                    output[i] = formatedName(terms[iTerm].members[i]);
            }
            return output;
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
        public void addBudget(double val, string strName, DateTime dtDateOccur, DateTime dtDateAccount, string strCategory, int iType, int iTerm, string strComment, int assetIndex = -1)
        {
            budget[iBudget].value = val;
            budget[iBudget].name = strName;
            budget[iBudget].dateOccur = dtDateOccur;
            budget[iBudget].dateAccount = dtDateAccount;
            budget[iBudget].cat = strCategory;
            budget[iBudget].type = iType;
            budget[iBudget].term = iTerm;
            budget[iBudget].comment = strComment;
            //if depreciation
            if (iType == 1)
                budget[iBudget].depOfAsset = assetIndex;
            iBudget++;
        }

        public void deleteBudget(int index)
        {
            iBudget--;
            for(int i = index; i < iBudget; i++)
            {
                budget[i].value = budget[i + 1].value;
                budget[i].name = budget[i + 1].name;
                budget[i].dateOccur = budget[i + 1].dateOccur;
                budget[i].dateAccount = budget[i + 1].dateAccount;
                budget[i].cat = budget[i + 1].cat;
                budget[i].type = budget[i + 1].type;
                budget[i].term = budget[i + 1].term;
                budget[i].comment = budget[i + 1].comment;
            }
        }

        /// <summary>
        /// Returns a list of the indexes of all assets
        /// </summary>
        /// <param name="withDepAssets">Whether to include assets that are depreciated</param>
        /// <returns></returns>
        public int[] assetList(bool withDepAssets)
        {
            List<int> output = new List<int>();
            for(int i = 0; i < iBudget; i++)
                //if asset and not fully depreciated
                if (budget[i].type == 0 && (withDepAssets || !fullyDepreciatedAsset(i)))
                    output.Add(i);
            return output.ToArray();
        }

        /// <summary>
        /// Determines if asset has any value after depreciation
        /// </summary>
        /// <param name="index">Index of the asset</param>
        /// <returns>Returns true is the depreciation on the asset is at least the value of the asset</returns>
        public bool fullyDepreciatedAsset(int index)
        {
            //don't even tr guess if the asset being depreciated hasn't been marked
            if (index == -1)
                return false;
            double dDep = 0;
            //sum up all of the depreciation against this asset
            for (int i = 0; i < iBudget; i++)
                if (budget[i].type == 1 && budget[i].depOfAsset == index)
                    dDep += budget[i].value;
            //return true if the depreciation completely depreciates the asset
            return dDep >= budget[index].value;
        }

        /// <summary>
        /// Determines if asset has any value after depreciation
        /// </summary>
        /// <param name="index">Index of the asset</param>
        /// <param name="beforeDate">Only include depreciation before this date</param>
        /// <returns>Returns true is the depreciation on the asset is at least the value of the asset</returns>
        public bool fullyDepreciatedAsset(int index, DateTime beforeDate)
        {
            //don't even tr guess if the asset being depreciated hasn't been marked
            if (index == -1)
                return false;
            double dDep = 0;
            //sum up all of the depreciation against this asset
            for (int i = 0; i < iBudget; i++)
                if (budget[i].type == 1 && budget[i].depOfAsset == index && budget[i].dateOccur<=beforeDate)
                    dDep += budget[i].value;
            //return true if the depreciation completely depreciates the asset
            return dDep >= budget[index].value;
        }

        /// <summary>
        /// Determines if asset has any value after depreciation
        /// </summary>
        /// <param name="index">Index of the asset</param>
        /// <returns>Returns the value of the asset after depreciation</returns>
        public double valueAfterDepreciation(int index)
        {
            double dDep = 0;
            //sum up all of the depreciation against this asset
            for (int i = 0; i < iBudget; i++)
                if (budget[i].type == 1 && budget[i].depOfAsset == index)
                    dDep += budget[i].value;
            return budget[index].value - dDep;
        }

        public void addHistory(string additionalInfo, history.changeType type)
        {
            historyList[iHistory] = new history(strUser, type, additionalInfo, DateTime.Now);
            iHistory++;
            //mark unsaved changes
            clsStorage.unsavedChanges = true;

            //if autosave is turned on, then save at this point
            if(Properties.Settings.Default.autoSave)
                Program.home.btnSave_Click(new object(), new EventArgs());
        }
    }

    public struct budgetItem
    {
        //stores the size of the revenue/expense
        public double value;
        //description of the item
        public string name;
        //date the purchase/expense took place
        public DateTime dateOccur;
        //date reflected in account
        public DateTime dateAccount;
        //cat is for organizing different budget items into similar categories
        public string cat;
        //whether item is asset, depreciation, revenue, or expense
        public int type;
        //term index item applies to
        public int term;
        //record any additional information about the item
        public string comment;
        /// <summary>
        /// If depreciation, stores the index of the asset it depreciates
        /// </summary>
        public int depOfAsset;
    }
}
