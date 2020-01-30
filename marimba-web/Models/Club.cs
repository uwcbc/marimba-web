using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;

using CsvHelper;

using marimba_web.Common;

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

        public void AddMembersCSV(string pathToCsv){
            FileStream file = new FileStream(pathToCsv, FileMode.Open, FileAccess.Read);
            using(StreamReader reader = new StreamReader(file)){
                while(!reader.EndOfStream){
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    DateTime signupTime = new DateTime(UInt32.Parse(values[0]));// should be DateTime?
                    string firstName = values[1];
                    string lastName = values[2];
                    uint studentID = UInt32.Parse(values[3]);
                    string email = values[4];
                    uint instrument = UInt32.Parse(values[5]);
                    uint faculty = UInt32.Parse(values[6]);
                    uint shirtSize = UInt32.Parse(values[7]);

                    Member bandMember= new Member(
                        firstName,
                        lastName,
                        (Marimba.StudentType) 0,
                        studentID, 
                        (Marimba.Faculty)faculty, 
                        (Marimba.Instrument)instrument,
                        new MailAddress(email),
                        (Marimba.ShirtSize )shirtSize
                        // nothing for signupTime?
                        );

                    //add and save to database

                }
            }
        }

    }
}
