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
        public IList<Term> terms { get; private set; }
        public IList<Member> members { get; private set; }
        private Dictionary<Guid, Member> memberDict { get; set; }

        public Club(IList<Term> terms, IList<Member> members) {
            this.terms = terms;
            this.members = members;
            memberDict = new Dictionary<Guid, Member>();

            foreach (var m in members)
            {
                memberDict.Add(m.id, m);
            }
        }

        /// <summary>
        /// Get member with the given GUID, returning null if no such member exists.
        /// </summary>
        public Member GetMemberByGuid(Guid id)
        {
            return memberDict.GetValueOrDefault(id, null);
        }

        /// <summary>
        /// Import members from CSV into database.
        /// </summary>
        /// <param name="pathToCsv">Path to CSV file</param>
        /// <param name="hasHeaders">Whether CSV includes headers</param>
        public void ImportMembersFromCSV(string pathToCsv, bool hasHeaders)
        {
            using var reader = new StreamReader(pathToCsv);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Use the defined Member CSV mapping class
            csv.Configuration.RegisterClassMap<MemberCsvMap>();
            // Whether the csv file has headers
            csv.Configuration.HasHeaderRecord = hasHeaders;
            // Since Member model has private set, this needs to be true
            csv.Configuration.IncludePrivateMembers = true;

            var records = csv.GetRecords<Member>().ToList();

            // TODO: Handle saving to database
        }

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
        /// a University of Waterloo student, an *active* member of the club in one of the
        /// past two terms, and must not have any outstanding debts with the club.
        ///
        /// TODO: Clarify what constitutes "active"
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
            var allMemberIds = previousTerm.members.Union(currentTerm.members);

            // Check eligiblity of every member in last two terms
            foreach (Guid guid in allMemberIds)
            {
                Member member = GetMemberByGuid(guid);
                bool wasActiveMemberPrevTerm = previousTerm.HasActiveMember(guid);
                bool isActiveMemberCurTerm = currentTerm.HasActiveMember(guid);
                bool isEligible = (wasActiveMemberPrevTerm || isActiveMemberCurTerm) && member.IsUWStudent();

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
            if (terms.Count < numTerms)
            {
                throw new InvalidOperationException("Not enough terms");
            }

            List<Term> sortedTerms = terms.OrderBy(t => t.startDate).ToList();
            return sortedTerms.GetRange(sortedTerms.Count - numTerms, numTerms);
        }
    }
}
