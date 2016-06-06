using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Marimba
{
    class history
    {
        public string user, otherInfo;
        public changeType type;
        public DateTime time;
        
        /// <summary>
        /// The type of change made, for the purpose of recording history of changes.
        /// NOTE: You need to always add new types to the end, since we store to file using ints.
        /// </summary>
        public enum changeType{
            signup = 0,
            signin = 1,
            editMember = 2,
            unsubscribe = 3,
            deactivate = 4,
            addUser = 5,
            sentEmail = 6,
            addFees = 7, 
            addBudget = 8,
            editBudget = 9,
            deleteBudget = 10,
            setupElection = 11,
            newTerm = 12,
            importMembers = 13,
            editAttendance = 14,
            removeFromTerm = 15,
            purgeMembers = 16,
            editUser = 17,
            deleteUser = 18,
            addToTerm = 19
        }

        //Our goal here is to minimize the number of strings stored
        //Strings take up a lot of disk space
        //changeType enum helps us save disk space
        //strUser and strAdditional are needed in case the number of users of additional members changes
        //we cannot reference them with their indexes
        public history(string strUser, changeType type, string strAdditional, DateTime dtTime)
        {
            this.user = strUser;
            this.type = type;
            this.otherInfo = strAdditional;
            this.time = dtTime;
        }

        public history(StreamReader sr)
        {
            this.user = sr.ReadLine();
            this.type = (changeType)Convert.ToInt32(sr.ReadLine());
            this.otherInfo = sr.ReadLine();
            this.time = new DateTime(Convert.ToInt64(sr.ReadLine()));
        }

        /// <summary>
        /// Converts history into a string that can be displayed to the user
        /// </summary>
        /// <returns>String representing the history</returns>
        public string toString()
        {
            string output = user;
            switch(type)
            {
                case(changeType.addBudget):
                    output += " added " + otherInfo + " to the budget - ";
                    break;
                case (changeType.addFees):
                    output += " recorded fees paid by " + otherInfo + " members - ";
                    break;
                case(changeType.addToTerm):
                    output += " added " + otherInfo.Split('@')[0] + " to " + otherInfo.Split('@')[1] + " - ";
                    break;
                case(changeType.addUser):
                    output += " created a new user account in Marimba for " + otherInfo + " - ";
                    break;
                case(changeType.editUser):
                    output += " edited the user account in Marimba for " + otherInfo + " - ";
                    break;
                case(changeType.deleteUser):
                    output += " deleted the user account in Marimba for " + otherInfo + " - ";
                    break;
                case(changeType.deactivate):
                    output += " deactivated " + otherInfo + "'s membership record - ";
                    break;
                case(changeType.deleteBudget):
                    output += " deleted " + otherInfo + " from the budget - ";
                    break;
                case(changeType.editAttendance):
                    output += " edited attendance records for " + otherInfo + " - ";
                    break;
                case(changeType.editBudget):
                    output += " edited " + otherInfo + " in the budget - ";
                    break;
                case(changeType.editMember):
                    output += " edited " + otherInfo + " membership information - ";
                    break;
                case(changeType.importMembers):
                    output += " imported new members from Google Doc - ";
                    break;
                case(changeType.newTerm):
                    output += " created a new term, " + otherInfo + " - ";
                    break;
                case(changeType.removeFromTerm):
                    output += " removed " + otherInfo.Split('@')[0] + " from " + otherInfo.Split('@')[1] + " - ";
                    break;
                case(changeType.sentEmail):
                    output += " sent an e-mail to " + otherInfo + " - ";
                    break;
                case(changeType.setupElection):
                    output += " set up an election - ";
                    break;
                case(changeType.signin):
                    output += " signed in members at the rehearsal - ";
                    break;
                case(changeType.signup):
                    output += " signed up " + otherInfo + " new members - ";
                    break;
                case (changeType.unsubscribe):
                    output += " unsubscribed " + otherInfo + " from the membership list - ";
                    break;
                case (changeType.purgeMembers):
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
        public static int lastCommonHistory(int low, int high, history[] list1, history[] list2)
        {
            if (low >= high) //found the first differing member
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

        public static history[] sortHistory(int low, int high, history[] array)
        {
            //i1 is the lowest part of the array
            //i2 is the highest part of the array
            int iFirst, iLast;
            history pivot;
            iFirst = low;
            iLast = high;
            pivot = array[iFirst];
            //since the left and right numbers will move around as we approach the pivot, we must always make sure we do not cross it
            while (low < high)
            {
                //move the right part of the array until it finds a number less than the pivot
                while ((array[high].time.CompareTo(pivot.time) >= 0) && (low < high))
                {
                    high--;
                }
                //if the two numbers are not equal
                if (low != high)
                {
                    //move the element of the array into its new home
                    array[low] = array[high];
                    low++;
                }
                //move the left part of the array until it finds a number greater than the pivot
                while ((array[low].time.CompareTo(pivot.time) <= 0) && (low < high))
                {
                    low++;
                }

                if (low != high)
                {
                    //move the element of the array into its new home
                    array[high] = array[low];
                    high--;
                }
            }
            //fill in these values that would have been overwritten in the above process
            array[low] = pivot;
            //pivot = low;
            //if the minimum part of the array was less than the pivot 
            //that half must be quicksorted
            if (iFirst < low)
                sortHistory(iFirst, iLast - 1, array);
            //if the maximum part of the array was more than the pivot
            //that half must be quicksorted
            if (iLast > low)
                sortHistory(iFirst + 1, iLast, array);
            return array;
        }
    }
}
