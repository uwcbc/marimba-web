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
        Random rnd;

        //these variables are for the candidates and voting
        public List<candidate> listOfCandidates = new List<candidate>();
        public int iPositions;
        public string[] strPositions;

        public int iTermIndex;

        public int totalBallots, rejectBallots;

        public struct candidate
        {
            public int index;
            public int[] preferences;
            public string strBlurb;
            public bool bElected;
        }

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
            rnd = new Random();
            this.iTermIndex = iTermIndex;
            updateElectorList(mainClub, true);

            totalBallots = 0;
            rejectBallots = 0;
        }

        public election(StreamReader sr)
        {
            iTermIndex = Convert.ToInt32(sr.ReadLine());
            elector nextElector = new elector();
            electorList = new List<elector>();
            almostElector = new List<elector>();
            totalBallots = 0;
            rejectBallots = 0;
            iAlmostElectors = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < iAlmostElectors; i++)
            {
                nextElector.id = Convert.ToInt32(sr.ReadLine());
                nextElector.strName = sr.ReadLine();
                nextElector.strEmail = sr.ReadLine();
                almostElector.Add(nextElector);
                //note to self: add id's here
            }
            iElectors = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < iElectors; i++)
            {
                nextElector.id = Convert.ToInt32(sr.ReadLine());
                nextElector.strName = sr.ReadLine();
                nextElector.strEmail = sr.ReadLine();
                nextElector.strCode = sr.ReadLine();
                electorList.Add(nextElector);
                //note to self: add id's here
            }
            iPositions = Convert.ToInt32(sr.ReadLine());
            strPositions = new string[iPositions];
            for (int i = 0; i < iPositions; i++)
                strPositions[i] = sr.ReadLine();
            int numCandidates = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < numCandidates; i++)
            {
                candidate nextCandidate = new candidate();
                nextCandidate.index = Convert.ToInt32(sr.ReadLine());
                nextCandidate.preferences = new int[iPositions];
                for (int j = 0; j < iPositions; j++)
                    nextCandidate.preferences[j] = Convert.ToInt32(sr.ReadLine());
                nextCandidate.strBlurb = sr.ReadLine();
                nextCandidate.bElected = Convert.ToBoolean(sr.ReadLine());
                listOfCandidates.Add(nextCandidate);
            }
        }

        public void updateElectorList(club mainClub, bool newCode)
        {
            //Note to self: This will have to be fixed for adding new users without creating new codes
            int term1index, term2index;
            bool term1okay, term2okay;
            for (short i = 0; i < mainClub.iMember; i++)
            {
                term2index = -1;
                term2okay = true;
                term1index = mainClub.terms[iTermIndex].memberSearch(i);
                term1okay = (term1index == -1) || mainClub.terms[iTermIndex].limboMembers[term1index] ||
                        (mainClub.terms[iTermIndex].feesPaid[term1index, 0] > 0);
                if (iTermIndex != 0)//There are two terms to go back to
                {
                    term2index = mainClub.terms[iTermIndex - 1].memberSearch(i);
                    term2okay = (term2index == -1) || mainClub.terms[iTermIndex - 1].limboMembers[term2index] ||
                        (mainClub.terms[iTermIndex - 1].feesPaid[term2index, 0] > 0);
                }
                if (((term1index != -1 && !mainClub.terms[iTermIndex].limboMembers[term1index]) ||
                    (term2index != -1 && !mainClub.terms[iTermIndex - 1].limboMembers[term2index]))
                    && term1okay && term2okay && (clsStorage.currentClub.members[i].type == member.membertype.UWUnderGrad || clsStorage.currentClub.members[i].type == member.membertype.UWGrad))
                {
                    elector temp = new elector();

                    temp.strName = mainClub.formatedName(i);
                    temp.strEmail = mainClub.members[i].strEmail;
                    temp.id = i;

                    //if we are looking for a new code for the user
                    if (newCode)
                        temp.strCode = codeGenerator(rnd);

                    electorList.Add(temp);
                    iElectors++;
                }
                else if ((clsStorage.currentClub.members[i].type == member.membertype.UWUnderGrad || clsStorage.currentClub.members[i].type == member.membertype.UWGrad) &&
                    ((term1index != -1 && !mainClub.terms[iTermIndex].limboMembers[term1index]) || (term2index != -1 && !mainClub.terms[iTermIndex - 1].limboMembers[term2index])))
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
        public static string codeGenerator(Random rnd)
        {
            char[] cCode = new char[16];
            //avoid the = since it messes up Excel
            cCode[0] = (char)rnd.Next(62, 126);
            for (int i = 1; i < 16; i++)
            {
                int iGen = rnd.Next(32, 126);
                cCode[i] = (char)iGen;
            }
            return String.Concat(cCode);
        }

        public struct elector
        {
            public int id;
            public string strName;
            public string strEmail;
            //I don't like having the code stored alongside the elector information
            //This should eventually be changed
            public string strCode;
        }
    }
}
