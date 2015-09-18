using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Marimba
{
    class term
    {
        //strName is the name of the term (e.g. Fall 2010)
        public string strName;
        //sNumber is the term index (Fall 2010 = 1)
        //sRehearsals is the number of rehearsals
        //sMembers counts the number of members here this term (member = attended >= 1 rehearsal)
        //iOtherFees is the number of other fees
        public short sNumber, sRehearsals, sMembers;
        public int iOtherFees;
        //startDate and endDate are used for cash flow purposes
        public DateTime startDate, endDate;
        //rehearsalDates is the set of rehearsal dates
        public DateTime[] rehearsalDates;
        //membershipFees stores the membership fee charged for the term
        public double membershipFees;
        //dOtherFees and strOtherFees store information on other fees
        //such as uniform fees
        public double[] dOtherFees;
        public string[] strOtherFees;
        //members is the members for the term
        //it is effectively a subset of the mailing list
        //NOTE: a "member" of the term is someone who attended at least one practice that term
        //they may not be a true fully paid member
        //assuming no more than 120 unique people show up over the course of the term
        //this is pretty easy to change if, for whatever reason, the club needed to
        public short[] members = new short[120];
        //limboMembers is to track members who come to only a few practices
        //a limbo member should not be charged membership fees
        public bool[] limboMembers = new bool[120];
        //iLimbo counts the number of limbo members
        //obviously, iLimbo <= sMembers
        public int iLimbo;
        //Set up a record of attendance for the members
        public bool[,] attendance;
        //Store who has paid and how much, and the date (feesPaidDate)
        public double[,] feesPaid;
        public DateTime[,] feesPaidDate;

        public term(string strName, short index, short numRehearsals, DateTime start, DateTime end, DateTime[] rehearsalDates, double membershipFees, double[] dOtherFees, string[] strOtherFees)
        {
            this.strName = strName;
            this.sMembers = 0;
            this.sNumber = index;
            for(int i = 0; i < 120; i++)
                members[i] = -1; //set to -1 so we know it does not represent member 0
            this.startDate = start;
            this.endDate = end;
            this.sRehearsals = numRehearsals;
            this.rehearsalDates = new DateTime[numRehearsals];
            for (int i = 0; i < sRehearsals; i++)
                this.rehearsalDates[i] = rehearsalDates[i];
            this.membershipFees = membershipFees;
            this.iOtherFees = Convert.ToInt16(dOtherFees.Length);
            this.dOtherFees = new double[iOtherFees];
            this.strOtherFees = new string[iOtherFees];
            for (int i = 0; i < iOtherFees; i++)
            {
                this.dOtherFees[i] = dOtherFees[i];
                this.strOtherFees[i] = strOtherFees[i];
            }
            //initialize the attendance record
            //it only means something if the rehearsal has happened and there is a member for that record
            attendance = new bool[120, sRehearsals];
            for (int i = 0; i < 120; i++)
                for (int j = 0; j < sRehearsals; j++)
                    this.attendance[i, j] = false;
            this.feesPaid = new double[120, 1 + iOtherFees];
            this.feesPaidDate = new DateTime[120, 1 + iOtherFees];
            checkLimbo();
        }

        public term(string strName, short index, short numRehearsals, DateTime start, DateTime end, DateTime[] rehearsalDates, double membershipFees)
        {
            this.strName = strName;
            this.sMembers = 0;
            this.sNumber = index;
            for (int i = 0; i < 120; i++)
                members[i] = -1; //set to -1 so we know it does not represent member 0
            this.startDate = start;
            this.endDate = end;
            this.sRehearsals = numRehearsals;
            this.rehearsalDates = new DateTime[numRehearsals];
            for (int i = 0; i < sRehearsals; i++)
                this.rehearsalDates[i] = rehearsalDates[i];
            this.membershipFees = membershipFees;
            this.iOtherFees = 0;
            //initialize the attendance record
            //it only means something if the rehearsal has happened and there is a member for that record
            attendance = new bool[120, sRehearsals];
            for (int i = 0; i < 120; i++)
                for (int j = 0; j < sRehearsals; j++)
                    this.attendance[i, j] = false;
            this.feesPaid = new double[120, 1];
            this.feesPaidDate = new DateTime[120, 1];
            checkLimbo();
        }

        public term(BinaryReader br)
        {
            strName = br.ReadString();
            sMembers = br.ReadInt16();
            sNumber = br.ReadInt16();
            for (int i = 0; i < 120; i++)
                members[i] = br.ReadInt16();
            startDate = new DateTime(br.ReadInt64());
            endDate = new DateTime(br.ReadInt64());
            sRehearsals = br.ReadInt16();
            rehearsalDates = new DateTime[sRehearsals];
            for (int i = 0; i < sRehearsals; i++)
                this.rehearsalDates[i] = new DateTime(br.ReadInt64());
            membershipFees = br.ReadDouble();
            iOtherFees = br.ReadInt32();
            dOtherFees = new double[iOtherFees];
            strOtherFees = new string[iOtherFees];
            for (int i = 0; i < iOtherFees; i++)
            {
                dOtherFees[i] = br.ReadDouble();
                strOtherFees[i] = br.ReadString();
            }
            attendance = new bool[120, sRehearsals];
            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < sRehearsals; j++)
                {
                    attendance[i, j] = br.ReadBoolean();
                }
            }
            feesPaid = new double[120, 1 + iOtherFees];
            feesPaidDate = new DateTime[120, 1 + iOtherFees];
            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < 1 + iOtherFees; j++)
                {
                    feesPaid[i, j] = br.ReadDouble();
                    feesPaidDate[i, j] = new DateTime(br.ReadInt64());
                }
            }
            checkLimbo();
        }

        public term(StreamReader sr)
        {
            strName = sr.ReadLine();
            sMembers = Convert.ToInt16(sr.ReadLine());
            sNumber = Convert.ToInt16(sr.ReadLine());
            for (int i = 0; i < 120; i++)
                members[i] = Convert.ToInt16(sr.ReadLine());
            startDate = new DateTime(Convert.ToInt64(sr.ReadLine()));
            endDate = new DateTime(Convert.ToInt64(sr.ReadLine()));
            sRehearsals = Convert.ToInt16(sr.ReadLine());
            rehearsalDates = new DateTime[sRehearsals];
            for (int i = 0; i < sRehearsals; i++)
                this.rehearsalDates[i] = new DateTime(Convert.ToInt64(sr.ReadLine()));
            membershipFees = Convert.ToDouble(sr.ReadLine());
            iOtherFees = Convert.ToInt32(sr.ReadLine());
            dOtherFees = new double[iOtherFees];
            strOtherFees = new string[iOtherFees];
            for (int i = 0; i < iOtherFees; i++)
            {
                dOtherFees[i] = Convert.ToDouble(sr.ReadLine());
                strOtherFees[i] = sr.ReadLine();
            }
            attendance = new bool[120, sRehearsals];
            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < sRehearsals; j++)
                {
                    attendance[i, j] = Convert.ToBoolean(sr.ReadLine());
                }
            }
            feesPaid = new double[120, 1 + iOtherFees];
            feesPaidDate = new DateTime[120, 1 + iOtherFees];
            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < 1 + iOtherFees; j++)
                {
                    feesPaid[i, j] = Convert.ToDouble(sr.ReadLine());
                    feesPaidDate[i, j] = new DateTime(Convert.ToInt64(sr.ReadLine()));
                }
            }
            checkLimbo();
        }

        public term()
        {
            //really, just make one
        }

        /// <summary>
        /// Saves the term data to a .mrb file
        /// </summary>
        /// <param name="bw">BinaryWriter initialized with save location</param>
        public void saveTerm(StreamWriter sw)
        {
            sw.WriteLine(strName);
            sw.WriteLine(sMembers);
            sw.WriteLine(sNumber);
            for (int i = 0; i < 120; i++)
                sw.WriteLine(members[i]);
            //start and end date
            sw.WriteLine(startDate.Ticks);
            sw.WriteLine(endDate.Ticks);
            sw.WriteLine(sRehearsals);
            for (int i = 0; i < sRehearsals; i++)
                sw.WriteLine(this.rehearsalDates[i].Ticks);
            sw.WriteLine(membershipFees);
            sw.WriteLine(iOtherFees);
            for (int i = 0; i < iOtherFees; i++)
            {
                sw.WriteLine(dOtherFees[i]);
                sw.WriteLine(strOtherFees[i]);
            }
            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < sRehearsals; j++)
                {
                    sw.WriteLine(attendance[i, j]);
                }
            }
            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < 1 + iOtherFees; j++)
                {
                    sw.WriteLine(feesPaid[i, j]);
                    sw.WriteLine(feesPaidDate[i, j].Ticks);
                }
            }
        }

        /// <summary>
        /// memberSearch checks if a member is in the term
        /// </summary>
        /// <param name="sID">Club ID of member searching for</param>
        /// <returns>Index in term (0 to 119) if found, -1 if not found</returns>
        public int memberSearch(short sID)
        {
            for (int i = 0; i < sMembers; i++)
                if (members[i] == sID)
                    return i;
            return -1;
        }

        public bool addMember(short sID)
        {
            //first, make sure the member is not already part of the term
            //also fail if we have 120 members already this term
            if (this.memberSearch(sID) != -1 || sMembers == 120)
                return false;
            //at this point, not already a member, so add them to the term
            members[sMembers] = sID;
            sMembers++;
            return true;
        }

        /// <summary>
        /// Removes a member from being associated with the term
        /// </summary>
        /// <param name="termIndex">Index of member relative to this term</param>
        /// <returns>True if removing member was successful</returns>
        public bool removeMember(short termIndex)
        {
            //make sure this member indeed exists
            //and that the member has no attendance
            if (this.members[termIndex] == -1 || iMemberAttendance(termIndex) != 0)
                return false;
            //move the reference indices and attendance to the correct member
            for (int i = termIndex; i < this.sMembers; i++)
            {
                members[i] = members[i + 1];
                for (int j = 0; j < sRehearsals; j++)
                    attendance[i, j] = attendance[i + 1, j];
                for (int j = 0; j<=iOtherFees;j++)
                {
                    feesPaid[i, j] = feesPaid[i + 1, j];
                    feesPaidDate[i, j] = feesPaidDate[i + 1, j];
                }
            }
            sMembers--;
            return true;
        }

        /// <summary>
        /// rehearsalIndex returns the index of a rehearsal date
        /// </summary>
        /// <param name="currentDate">The date of the rehearsal</param>
        /// <returns>The index of the rehearsal. Returns -1 if date is not found.</returns>
        public int rehearsalIndex(DateTime currentDate)
        {
            for (int i = 0; i < sRehearsals; i++)
                if (rehearsalDates[i] == currentDate)
                    return i;
            return -1;
        }

        /// <summary>
        /// memberAttendance returns an array of a member's attendance records for the term
        /// </summary>
        /// <param name="iID">Member Term index</param>
        /// <returns>Array containing attendance</returns>
        public bool[] memberAttendance(int iID)
        {
            bool[] output = new bool[sRehearsals];
            for (int i = 0; i < sRehearsals; i++)
                output[i] = attendance[iID, i];
            return output;
        }

        /// <summary>
        /// Counts the number of rehearsals a member attended in the term
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public int iMemberAttendance(int iID)
        {
            int output = 0;
            for (int i = 0; i < sRehearsals; i++)
                if (attendance[iID, i])
                    output++;
            return output;
        }

        /// <summary>
        /// Counts the number of people who attended a rehearsal
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int iRehearsalAttendance(int index)
        {
            int output = 0;
            for (int i = 0; i < sMembers; i++)
                if (attendance[i, index])
                    output++;
            return output;
        }

        /// <summary>
        /// recentRehearsal returns the index of the last rehearsal
        /// </summary>
        /// <param name="currentDate">Current date to compare rehearsals against</param>
        /// <returns>Returns index of last rehearsal. Returns -1 if no rehearsal in term has happened yet</returns>
        public int recentRehearsal(DateTime currentDate)
        {
            int i;
            for (i = 0; i < sRehearsals; i++)
                if ((currentDate - rehearsalDates[i]).Days < 0)
                    return i-1;
            return sRehearsals-1;
        }

        /// <summary>
        /// Returns the index of the last common member between two terms
        /// </summary>
        /// <param name="low">Low index</param>
        /// <param name="high">High index</param>
        /// <param name="term1">First term</param>
        /// <param name="term2">Second term</param>
        /// <returns>Returns index of last common member</returns>
        public static int lastCommonMember(int low, int high, term term1, term term2)
        {
            if (low >= high) //found the first differing member
            {
                if (term1.members[low] == term2.members[low])
                    return low;
                else
                    return low - 1;
            }
            else
            {
                int mid = (low + high) / 2;
                if (term1.members[low] == term2.members[low] && term1.members[mid] == term2.members[mid])
                    return lastCommonMember(mid + 1, high, term1, term2);
                else
                    return lastCommonMember(low, mid - 1, term1, term2);
            }
        }

        /// <summary>
        /// Checks if a member is a limbo member
        /// </summary>
        /// <param name="iMember">Term index of member</param>
        /// <returns>True if member is in limbo for term</returns>
        public bool checkLimbo(int iMember)
        {
            //we need to establish some rules for who is and is not a limbo member
            //here are the rules I decided
            //1) Even if someone tells us they will not be returning, we do not mark them as such
            //   If someone has attended enough practices, they may vote if they have paid membership dues
            //   Also, we do not let them off the hook if they have attended over half of the practices
            //   They almost certainly used club resources (e.g. attended socials, etc.)
            //2) As hinted in one, they have attended fewer than half of the rehearsals
            //3) They have missed three rehearsals in a row, and did not attend any future ones
            //NOTE: A member may be de-limboed if they attend enough future practices during the term
            int lastRehearsal = recentRehearsal(DateTime.Today);
            if (lastRehearsal >= 2 && (!attendance[iMember, lastRehearsal] && !attendance[iMember, lastRehearsal - 1] && !attendance[iMember, lastRehearsal - 2])
                && iMemberAttendance(iMember) <= sRehearsals / 2)
                //member is in limbo
                return true;
            else
                return false;
        }

        public void checkLimbo()
        {
            //check limbo for all members and update limbo information
            iLimbo = 0;
            for (int i = 0; i < sMembers; i++)
            {
                limboMembers[i] = checkLimbo(i);
                if(limboMembers[i])
                    iLimbo++;
            }
        }
    }
}
