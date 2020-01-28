using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    public static class Club
    {
        public List<Term> termList { get; set; }

        /*
         * Export list of eligible electors to CSV.
         */
        public void ExportEligibleElectorsToCsv(string pathToCsv)
        {
            List<Elector> electorList = GetEligibleElectors();

            using var writer = new StreamWriter(pathToCsv);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap<ElectorMap>();
            csv.WriteRecords(electorList);
        }

        /*
         * Construct list of eligible electors, according to the Constitution:
         *
         * To be eligible to vote in the UW Concert Band Club elections, a person must be
         * a University of Waterloo student, an active member of the club in one of the
         * past two terms, and must not have any outstanding debts with the club.
         */
        private List<Elector> GetEligibleElectors()
        {
            List<Elector> eligibleElectors = new List<Elector>();
            List<Term> lastTwoTerms;

            try
            {
                lastTwoTerms = GetLatestTerms(2);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return new List<Elector>();
            }

            // Previous term members
            Dictionary<Guid, Member> prevTermMemberDict = lastTwoTerms[0].ConstructMembersDict();
            // Previous term inactive member IDs
            HashSet<Guid> prevTermLimboMemberIds = lastTwoTerms[0].ConstructLimboMemberIds();
            // Current term members
            Dictionary<Guid, Member> curTermMemberDict = lastTwoTerms[1].ConstructMembersDict();
            // Current term inactive member IDs
            HashSet<Guid> curTermLimboMemberIds = lastTwoTerms[1].ConstructLimboMemberIds();
            // All member IDs in last two terms
            IEnumerable<Guid> allMemberIds = prevTermMemberDict.Keys.Union(curTermMemberDict.Keys);

            // Check eligiblity of every member in last two terms
            foreach (Guid guid in allMemberIds)
            {
                // Member must be present in one of the two terms
                Member member = curTermMemberDict.GetValueOrDefault(guid, null) ?? prevTermMemberDict[guid];
                bool wasMemberPrevTerm = prevTermMemberDict.ContainsKey(guid);
                bool wasLimboPrevTerm = prevTermLimboMemberIds.Contains(guid);
                bool isMemberCurTerm = curTermMemberDict.ContainsKey(guid);
                bool isLimboCurTerm = curTermLimboMemberIds.Contains(guid);
                bool isEligible = false;

                // Member was in both terms
                if (wasMemberPrevTerm && isMemberCurTerm)
                {
                    isEligible = !(wasLimboPrevTerm && isLimboCurTerm) && member.isUWStudent();
                }
                // Member was in previous term but not this term
                else if (wasMemberPrevTerm)
                {
                    isEligible = !wasLimboPrevTerm && member.isUWStudent();
                }
                // Member is in this term but not last term
                else if (isMemberCurTerm)
                {
                    isEligible = !isLimboCurTerm && member.isUWStudent();
                }

                if (isEligible)
                {
                    Elector elector = new Elector
                    {
                        name = member.GetFullName(),
                        email = member.email,
                        isMembershipPaid = member.debtsOwed == 0m
                    };

                    eligibleElectors.Add(elector);
                }
            }

            return eligibleElectors;
        }

        /*
         * Get last X terms in chronological order (earliest to most recent).
         */
        private List<Term> GetLatestTerms(int numTerms)
        {
            if (termList.Count < numTerms)
            {
                throw new InvalidOperationException("Not enough terms");
            }

            List<Term> sortedTerms = termList.OrderBy(t => t.startDate).ToList();
            return sortedTerms.GetRange(sortedTerms.Count - numTerms, numTerms);
        }
    }
}
