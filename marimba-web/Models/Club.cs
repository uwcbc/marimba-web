using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CsvHelper;

namespace marimba_web.Models
{
    public class Club
    {
        public List<Term> termList { get; set; }

        /// <summary>
        /// Export list of eligible electors to CSV.
        /// </summary>
        /// <param name="pathToCsv">File path to CSV file</param>
        public void ExportEligibleElectorsToCsv(string pathToCsv)
        {
            List<Elector> electorList = GetEligibleElectors();

            using var writer = new StreamWriter(pathToCsv);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap<ElectorMap>();
            csv.WriteRecords(electorList);
        }

        /// <summary>
        /// Construct list of eligible electors, according to the Constitution:
        /// 
        /// To be eligible to vote in the UW Concert Band Club elections, a person must be
        /// a University of Waterloo student, an active member of the club in one of the
        /// past two terms, and must not have any outstanding debts with the club.
        /// </summary>
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

            Term previousTerm = lastTwoTerms[0];
            Term currentTerm = lastTwoTerms[1];

            // All member IDs in last two terms
            var allMemberIds = previousTerm.GetAllMemberIds().Union(currentTerm.GetAllMemberIds());

            // Check eligiblity of every member in last two terms
            foreach (Guid guid in allMemberIds)
            {
                // Member must be present in one of the two terms
                Member member = currentTerm.GetMemberByGuid(guid) ?? previousTerm.GetMemberByGuid(guid);
                bool wasMemberPrevTerm = previousTerm.IsMember(guid);
                bool wasLimboPrevTerm = previousTerm.IsLimboMember(guid);
                bool isMemberCurTerm = currentTerm.IsMember(guid);
                bool isLimboCurTerm = currentTerm.IsLimboMember(guid);
                bool isEligible = false;

                // Member was in both terms
                if (wasMemberPrevTerm && isMemberCurTerm)
                {
                    isEligible = !(wasLimboPrevTerm && isLimboCurTerm) && member.IsUWStudent();
                }
                // Member was in previous term but not this term
                else if (wasMemberPrevTerm)
                {
                    isEligible = !wasLimboPrevTerm && member.IsUWStudent();
                }
                // Member is in this term but not last term
                else if (isMemberCurTerm)
                {
                    isEligible = !isLimboCurTerm && member.IsUWStudent();
                }

                if (isEligible)
                {
                    Elector elector = new Elector(member.GetFullName(), member.instrument, member.email, member.debtsOwed == 0m);
                    eligibleElectors.Add(elector);
                }
            }

            return eligibleElectors;
        }

        /// <summary>
        /// Get the latest terms in chronological order (earliest to most recent).
        /// </summary>
        /// <param name="numTerms">Number of terms to retrieve</param>
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
