namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Contains a bunch of methods and fields to run Marimba
    /// </summary>
    class ClsStorage
    {
        public static Club currentClub;
        public static bool loggedin = false;

        /// <summary>
        /// The currently selected members. Used for inter-Form communication.
        /// Currently used for emails and adding users to a term.
        /// </summary>
        public static IList<int> selectedMembersList = new List<int>();

        /// <summary>
        /// The currently selected terms; used for inter-Form communication.
        /// </summary>
        private static IList<Term> selectedTerms;

        public static bool unsavedChanges = false;

        public const string receiptSubject = "Membership Fee Receipt";
        public static string[] receiptMessage = new string[3]
        {
            "Your membership fee payment for the term ",
            " has been recorded. Here are the details:\r\n\r\n",
            "\r\n\r\nIf you believe there are any errors, please inform the club as soon as possible."
        };

        /// <summary>
        /// The encryption relies on new lines to separate data. Use this to mark actual new lines differently
        /// </summary>
        /// <param name="input">A string</param>
        /// <returns>The cleaned string without newlines</returns>
        public static string CleanNewLine(string input)
        {
            return input.Replace("\r\n", "\\N").Replace("\n", "\\N");
        }

        /// <summary>
        /// To decrypt a file, we need to find the newlines that were saved.
        /// </summary>
        /// <param name="input">String to add back newlines in</param>
        /// <returns>An uncleaned string with newlines</returns>
        public static string ReverseCleanNewLine(string input)
        {
            return input.Replace("\\N", "\r\n");
        }

        /// <summary>
        /// XORs bitwise two byte arrays
        /// </summary>
        /// <param name="b1">First byte array</param>
        /// <param name="b2">Second byte array</param>
        /// <returns>b1 ^ b2</returns>
        public static byte[] XOR(byte[] b1, byte[] b2)
        {
            byte[] output = new byte[b1.Length];
            for (int i = 0; i < b1.Length; i++)
                output[i] = (byte)(b1[i] ^ b2[i]);
            return output;
        }

        public static void setSelectedTerms(IList<Term> terms)
        {
            selectedTerms = terms;
        }

        public static IList<Term> getSelectedTerms()
        {
            return selectedTerms;
        }
    }
}
