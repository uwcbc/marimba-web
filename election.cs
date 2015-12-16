using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Marimba
{
    class election
    {
        //these variables are for the list
        public int iElectors;
        public int iAlmostElectors;
        public List<elector> electorList;
        public List<elector> almostElector;

        public int iTermIndex;

        public election(club mainClub, int iTermIndex)
        {
            //creating an election is a lot of work!
            //we have a few special structures just for this
            //first, create a list of electors
            //start with the 2 terms back, then do 1 term back
            //240 is 120*2, the maximum possible number we could handle for an election
            electorList = new List<elector>();
            //this is so we can let people who forgot to pay membership fees know that if they want to vote
            //they need to settle their debts
            almostElector = new List<elector>();
            iElectors = 0;
            iAlmostElectors = 0;
            this.iTermIndex = iTermIndex;
            updateElectorList(mainClub);
        }

        public election(StreamReader sr)
        {
            iTermIndex = Convert.ToInt32(sr.ReadLine());
            elector nextElector = new elector();
            electorList = new List<elector>();
            almostElector = new List<elector>();

            /* don't keep track of this anymore */

            iAlmostElectors = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < iAlmostElectors; i++)
            {
                nextElector.id = Convert.ToInt32(sr.ReadLine());
                nextElector.strName = sr.ReadLine();
                nextElector.strEmail = sr.ReadLine();
                almostElector.Add(nextElector);
            }
            iElectors = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < iElectors; i++)
            {
                nextElector.id = Convert.ToInt32(sr.ReadLine());
                nextElector.strName = sr.ReadLine();
                nextElector.strEmail = sr.ReadLine();

                /* don't keep track of this anymore */
                string dummy_strCode = sr.ReadLine();

                electorList.Add(nextElector);
                //note to self: add id's here
            }

            /* don't keep track of this anymore */
            int iPositions = Convert.ToInt32(sr.ReadLine());
            string[] strPositions = new string[iPositions];
            for (int i = 0; i < iPositions; i++)
                strPositions[i] = sr.ReadLine();

            int numCandidates = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < numCandidates; i++)
            {
                int dummy_index = Convert.ToInt32(sr.ReadLine());
                int[] dummy_preferences = new int[iPositions];
                for (int j = 0; j < iPositions; j++)
                    dummy_preferences[j] = Convert.ToInt32(sr.ReadLine());
                string dummy_strBlurb = sr.ReadLine();
                bool dummy_bElected = Convert.ToBoolean(sr.ReadLine());
            }
        }

        public void updateElectorList(club mainClub)
        {
            int term1index, term2index;
            bool term1okay, term2okay;
            for (short i = 0; i < mainClub.iMember; i++)
            {
                term2index = -1;
                term2okay = true;
                term1index = mainClub.listTerms[iTermIndex].memberSearch(i);
                term1okay = (term1index == -1) || mainClub.listTerms[iTermIndex].limboMembers[term1index] ||
                        (mainClub.listTerms[iTermIndex].feesPaid[term1index, 0] > 0);
                if (iTermIndex != 0)//There are two terms to go back to
                {
                    term2index = mainClub.listTerms[iTermIndex - 1].memberSearch(i);
                    term2okay = (term2index == -1) || mainClub.listTerms[iTermIndex - 1].limboMembers[term2index] ||
                        (mainClub.listTerms[iTermIndex - 1].feesPaid[term2index, 0] > 0);
                }
                if (((term1index != -1 && !mainClub.listTerms[iTermIndex].limboMembers[term1index]) ||
                    (term2index != -1 && !mainClub.listTerms[iTermIndex - 1].limboMembers[term2index]))
                    && term1okay && term2okay && (clsStorage.currentClub.members[i].type == member.membertype.UWUnderGrad || clsStorage.currentClub.members[i].type == member.membertype.UWGrad))
                {
                    elector temp = new elector();

                    temp.strName = mainClub.formatedName(i);
                    temp.strEmail = mainClub.members[i].strEmail;
                    temp.id = i;

                    electorList.Add(temp);
                    iElectors++;
                }
                else if ((clsStorage.currentClub.members[i].type == member.membertype.UWUnderGrad || clsStorage.currentClub.members[i].type == member.membertype.UWGrad) &&
                    ((term1index != -1 && !mainClub.listTerms[iTermIndex].limboMembers[term1index]) || (term2index != -1 && !mainClub.listTerms[iTermIndex - 1].limboMembers[term2index])))
                {
                    elector temp = new elector();
                    temp.strName = mainClub.formatedName(i);
                    temp.strEmail = mainClub.members[i].strEmail;
                    temp.id = i;

                    almostElector.Add(temp);
                    iAlmostElectors++;
                }
            }
        }

        public class elector
        {
            public int id;
            public string strName;
            public string strEmail;
        }
    }
}
