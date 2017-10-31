namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Marimba.Utility;

    class HistoryItem
    {
        /// <summary>
        /// The user who performed the action
        /// </summary>
        public string user;
        
        /// <summary>
        /// Info to store in the history that describes this action
        /// </summary>
        public string otherInfo;

        /// <summary>
        /// The type of change logged
        /// </summary>
        public ChangeType type;

        /// <summary>
        /// Time that the change occurred
        /// </summary>
        public DateTime time;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryItem" /> class.
        /// </summary>
        /// <param name="strUser">User that performed the action</param>
        /// <param name="type">Type of action performed</param>
        /// <param name="strAdditional">Additional information about the action performed</param>
        /// <param name="dtTime">The time that the action occurred</param>
        public HistoryItem(string strUser, ChangeType type, string strAdditional, DateTime dtTime)
        {
            this.user = strUser;
            this.type = type;
            this.otherInfo = strAdditional;
            this.time = dtTime;
        }

        public HistoryItem(StreamReader sr)
        {
            this.user = sr.ReadLine();
            this.type = (ChangeType)Convert.ToInt32(sr.ReadLine());
            this.otherInfo = sr.ReadLine();
            this.time = new DateTime(Convert.ToInt64(sr.ReadLine()));
        }

        /// <summary>
        /// Converts history into a string that can be displayed to the user
        /// </summary>
        /// <returns>String representing the history</returns>
        public override string ToString()
        {
            string output = user;
            switch (type)
            {
                case ChangeType.AddBudget:
                    output += " added " + otherInfo + " to the budget - ";
                    break;
                case ChangeType.AddFees:
                    output += " recorded fees paid by " + otherInfo + " members - ";
                    break;
                case ChangeType.AddToTerm:
                    output += " added " + otherInfo.Split('@')[0] + " to " + otherInfo.Split('@')[1] + " - ";
                    break;
                case ChangeType.AddUser:
                    output += " created a new user account in Marimba for " + otherInfo + " - ";
                    break;
                case ChangeType.EditUser:
                    output += " edited the user account in Marimba for " + otherInfo + " - ";
                    break;
                case ChangeType.DeleteUser:
                    output += " deleted the user account in Marimba for " + otherInfo + " - ";
                    break;
                case ChangeType.Deactivate:
                    output += " deactivated " + otherInfo + "'s membership record - ";
                    break;
                case ChangeType.DeleteBudget:
                    output += " deleted " + otherInfo + " from the budget - ";
                    break;
                case ChangeType.EditAttendance:
                    output += " edited attendance records for " + otherInfo + " - ";
                    break;
                case ChangeType.EditBudget:
                    output += " edited " + otherInfo + " in the budget - ";
                    break;
                case ChangeType.EditMember:
                    output += " edited " + otherInfo + " membership information - ";
                    break;
                case ChangeType.ImportMembers:
                    output += " imported new members from Google Doc - ";
                    break;
                case ChangeType.NewTerm:
                    output += " created a new term, " + otherInfo + " - ";
                    break;
                case ChangeType.RemoveFromTerm:
                    output += " removed " + otherInfo.Split('@')[0] + " from " + otherInfo.Split('@')[1] + " - ";
                    break;
                case ChangeType.SentEmail:
                    output += " sent an e-mail to " + otherInfo + " - ";
                    break;
                case ChangeType.SetupElection:
                    output += " set up an election - ";
                    break;
                case ChangeType.Signin:
                    output += " signed in members at the rehearsal - ";
                    break;
                case ChangeType.Signup:
                    output += " signed up " + otherInfo + " new members - ";
                    break;
                case ChangeType.Unsubscribe:
                    output += " unsubscribed " + otherInfo + " from the membership list - ";
                    break;
                case ChangeType.PurgeMembers:
                    output += " purged inactive members from the club's membership list - ";
                    break;
            }

            return output + time.ToLongDateString();
        }

        public void saveHistory(StreamWriter sw)
        {
            sw.WriteLine(user);
            sw.WriteLine((int)type);
            sw.WriteLine(otherInfo);
            sw.WriteLine(time.Ticks);
        }

        /// <summary>
        /// Compares two list of histories to find the last place in which they differ
        /// </summary>
        /// <param name="low">low index of array</param>
        /// <param name="high">high index of array</param>
        /// <param name="list1">first history array</param>
        /// <param name="list2">second history array</param>
        /// <returns>index where arrays are similar up to</returns>
        public static int lastCommonHistory(int low, int high, HistoryItem[] list1, HistoryItem[] list2)
        {
            // found the first differing member
            if (low >= high)
            {
                if (list1[low].time == list2[low].time)
                    return low;
                else
                    return low - 1;
            }
            else
            {
                int mid = (low + high) / 2;
                if (list1[low].time == list2[low].time && list1[mid].time == list2[mid].time)
                    return lastCommonHistory(mid + 1, high, list1, list2);
                else
                    return lastCommonHistory(low, mid - 1, list1, list2);
            }
        }

        public static HistoryItem[] SortHistory(int low, int high, HistoryItem[] array)
        {
            // i1 is the lowest part of the array
            // i2 is the highest part of the array
            int iFirst, iLast;
            HistoryItem pivot;
            iFirst = low;
            iLast = high;
            pivot = array[iFirst];

            // since the left and right numbers will move around as we approach the pivot, we must always make sure we do not cross it
            while (low < high)
            {
                // move the right part of the array until it finds a number less than the pivot
                while ((array[high].time.CompareTo(pivot.time) >= 0) && (low < high))
                {
                    high--;
                }

                // if the two numbers are not equal
                if (low != high)
                {
                    // move the element of the array into its new home
                    array[low] = array[high];
                    low++;
                }

                // move the left part of the array until it finds a number greater than the pivot
                while ((array[low].time.CompareTo(pivot.time) <= 0) && (low < high))
                {
                    low++;
                }

                if (low != high)
                {
                    // move the element of the array into its new home
                    array[high] = array[low];
                    high--;
                }
            }

            // fill in these values that would have been overwritten in the above process
            array[low] = pivot;

            // pivot = low;
            // if the minimum part of the array was less than the pivot 
            // that half must be quicksorted
            if (iFirst < low)
                SortHistory(iFirst, iLast - 1, array);

            // if the maximum part of the array was more than the pivot
            // that half must be quicksorted
            if (iLast > low)
                SortHistory(iFirst + 1, iLast, array);
            return array;
        }
    }
}
