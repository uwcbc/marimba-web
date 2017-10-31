namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Manages an election for the club
    /// </summary>
    class Election
    {
        public List<Elector> electorList;

        public List<Elector> almostElector;

        public int termIndex;

        public Election(Club mainClub, int termIndex)
        {
            // creating an election is a lot of work!
            // we have a few special structures just for this
            // first, create a list of electors
            // start with the 2 terms back, then do 1 term back
            this.electorList = new List<Elector>();

            // this is so we can let people who forgot to pay membership fees know that if they want to vote
            // they need to settle their debts
            this.almostElector = new List<Elector>();
            this.termIndex = termIndex;
            this.UpdateElectorList(mainClub);
        }

        public void UpdateElectorList(Club mainClub)
        {
            int term1index, term2index;
            bool term1okay, term2okay;
            for (short i = 0; i < mainClub.iMember; i++)
            {
                term2index = -1;
                term2okay = true;
                term1index = mainClub.listTerms[termIndex].memberSearch(i);
                term1okay = (term1index == -1) ||
                    mainClub.listTerms[termIndex].limboMembers[term1index] ||
                    mainClub.listTerms[termIndex].feesPaid[term1index, 0] > 0;

                // There are two terms to go back to
                if (termIndex != 0)
                {
                    term2index = mainClub.listTerms[termIndex - 1].memberSearch(i);
                    term2okay = (term2index == -1) || mainClub.listTerms[termIndex - 1].limboMembers[term2index] ||
                        (mainClub.listTerms[termIndex - 1].feesPaid[term2index, 0] > 0);
                }

                if (((term1index != -1 && !mainClub.listTerms[termIndex].limboMembers[term1index]) ||
                    (term2index != -1 && !mainClub.listTerms[termIndex - 1].limboMembers[term2index]))
                    && term1okay && term2okay && (ClsStorage.currentClub.members[i].type == Member.MemberType.UWUnderGrad || ClsStorage.currentClub.members[i].type == Member.MemberType.UWGrad))
                {
                    Elector temp = new Elector();

                    temp.strName = mainClub.GetFormattedName(i);
                    temp.strEmail = mainClub.members[i].email;
                    temp.id = i;

                    electorList.Add(temp);
                }
                else if ((ClsStorage.currentClub.members[i].type == Member.MemberType.UWUnderGrad || ClsStorage.currentClub.members[i].type == Member.MemberType.UWGrad) &&
                    ((term1index != -1 && !mainClub.listTerms[termIndex].limboMembers[term1index]) || (term2index != -1 && !mainClub.listTerms[termIndex - 1].limboMembers[term2index])))
                {
                    Elector temp = new Elector();
                    temp.strName = mainClub.GetFormattedName(i);
                    temp.strEmail = mainClub.members[i].email;
                    temp.id = i;

                    this.almostElector.Add(temp);
                }
            }
        }

        public class Elector
        {
            public int id;

            public string strName;

            public string strEmail;
        }
    }
}
