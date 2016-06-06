using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Marimba
{
    class clsStorage
    {
        public static club currentClub;
        public static bool loggedin = false;
        /*
         * this variable is used for holding a list of selected members
         * currently used for emails and adding users to a term
         */
        public static IList<int> selectedMembersList = new List<int>();
        public static bool unsavedChanges = false;

        public static string moneyTypeToString(int iType)
        {
            switch (iType)
            {
                case 0:
                    return "Asset";
                case 1:
                    return "Depreciation";
                case 2:
                    return "Expense";
                case 3:
                    return "Revenue";
                default:
                    return "";
            }
        }

        public const string receiptSubject = "Membership Fee Receipt";
        public static string[] receiptMessage = new string[3]{"Your membership fee payment for the term "," has been recorded. Here are the details:\r\n\r\n",
         "\r\n\r\nIf you believe there are any errors, please inform the club as soon as possible."};

        /// <summary>
        /// The encryption relies on new lines to separate data. Use this to mark actual new lines differently
        /// </summary>
        /// <param name="input">A string</param>
        /// <returns>The cleaned string without new lines</returns>
        public static string cleanNewLine(string input)
        {
            return input.Replace("\r\n","\\R\\N");
        }

        public static string reverseCleanNewLine(string input)
        {
            return input.Replace("\\R\\N", "\r\n");
        }

        /// <summary>
        /// XORs bitwise two byte arrays
        /// </summary>
        /// <param name="b1">First byte array</param>
        /// <param name="b2">Second byte array</param>
        /// <returns>b1 ^ b2</returns>
        public static byte[] byteXOR(byte[] b1, byte[] b2)
        {
            byte[] output = new byte[b1.Length];
            for (int i = 0; i < b1.Length; i++)
                output[i] = (byte)(b1[i] ^ b2[i]);
            return output;
        }
    }
}
