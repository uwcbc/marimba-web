using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CsvHelper;
using CsvHelper.Configuration;

namespace marimba_web.Models
{
    public class Term
    {
        public string Name { get; set; } // i.e. W2020
        public IList<Member> AllMembers { get; set; }
        public IList<Member> LimboMembers { get; set; } // Subset of allMembers
        public IList<Rehearsal> Rehersals { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FeeAmount { get; set; }

        // Internal structures for faster lookup by GUID.
        private Dictionary<Guid, Member> memberDict;
        private Dictionary<Guid, Member> limboMemberDict;

        /// <summary>
        /// Creates an instance of the Term class
        /// </summary>
        /// <param name="name">The term name</param>
        /// <param name="allMembers">List of all members in the term</param>
        /// <param name="limboMembers">List of all limbo members in the term</param>
        /// <param name="rehearsals">List of rehearsals in the term</param>
        /// <param name="startDate">Start date of the term</param>
        /// <param name="endDate">End date of the term</param>
        /// <param name="feeAmount">Membership fee amount</param>
        public Term(string name, IList<Member> allMembers, IList<Member> limboMembers, IList<Rehearsal> rehearsals,
            DateTime startDate, DateTime endDate, decimal feeAmount)
        {
            this.Name = name;
            this.AllMembers = allMembers;
            this.LimboMembers = limboMembers;
            this.Rehersals = rehearsals;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.FeeAmount = feeAmount;

            InitializeDicts();
        }

        /// <summary>
        /// Initialize Guid-to-Member dictionaries for all members and limbo members.
        /// Used for more efficient lookup of Members by GUID.
        /// </summary>
        private void InitializeDicts()
        {
            memberDict = AllMembers.ToDictionary(member => member.id, member => member);
            limboMemberDict = LimboMembers.ToDictionary(member => member.id, member => member);
        }

        /// <summary>
        /// Get member with the given GUID, returning null if no such member exists.
        /// </summary>
        public Member GetMemberByGuid(Guid id)
        {
            return memberDict.GetValueOrDefault(id, null);
        }

        /// <summary>
        /// Return true if member with given GUID exists in term, otherwise false.
        /// </summary>
        public bool IsMember(Guid id)
        {
            return memberDict.ContainsKey(id);
        }

        /// <summary>
        /// Return true if member with given GUID is limbo, otherwise false.
        /// </summary>
        public bool IsLimboMember(Guid id)
        {
            return limboMemberDict.ContainsKey(id);
        }

        /// <summary>
        /// Return set of all member GUIDs.
        /// </summary>
        public HashSet<Guid> GetAllMemberIds()
        {
            return memberDict.Keys.ToHashSet();
        }

        /// <summary>
        /// Return set of all limbo member GUIDs.
        /// </summary>
        public HashSet<Guid> GetLimboMemberIds()
        {
            return limboMemberDict.Keys.ToHashSet();
        }

        /// <summary>
        /// Return set of all non-limbo member GUIDs.
        /// </summary>
        public HashSet<Guid> GetNonLimboMemberIds()
        {
            return memberDict.Keys.Except(limboMemberDict.Keys).ToHashSet();
        }

        /// <summary>
        /// Generates a CSV file containing all active member profile data.
        /// </summary>
        /// <param name="csvPath">String path to the CSV file to be exported.</param>
        public void GenerateActiveMemberCSV(string csvPath)
        {
            GenerateMemberCSV<MemberProfileMap>(GetActiveMembers(), csvPath);
        }

        /// <summary>
        /// Generates a CSV file containing both inactive and active members.
        /// </summary>
        /// <param name="csvPath">String path to the CSV to be exported.</param>
        public void GenerateAllMemberCSV(string csvPath)
        {
            GenerateMemberCSV<MemberProfileMap>(AllMembers, csvPath);
        }

        /// <summary>
        /// Exports a CSV of the aggregated financial information on members.
        /// </summary>
        /// <param name="csvPath">String path to the export CSV location.</param>
        public void GenerateMemberFinancialCSV(string csvPath)
        {
            GenerateMemberCSV<MemberFinancialMap>(GetActiveMembers(), csvPath);
        }

        /// <summary>
        /// Generates a CSV file of the data of the given members to be exported. Properties are mapped
        /// according to the passed in ClassMap type.
        /// </summary>
        /// <typeparam name="T">The type of mapping to perform on the members.</typeparam>
        /// <param name="membersToExport">The list of members to be exported.</param>
        /// <param name="csvPath">String path to the CSV file export location.</param>
        public void GenerateMemberCSV<T>(IEnumerable<Member> membersToExport, string csvPath) where T : ClassMap<Member>
        {
            using StreamWriter writer = new StreamWriter(csvPath);
            using CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.RegisterClassMap<T>();
            csv.WriteRecords(membersToExport);
        }

        /// <summary>
        /// Gets a list of all non-limbo (active) members for this term.
        /// </summary>
        /// <returns>List of all active members in this term.</returns>
        public List<Member> GetActiveMembers()
        {
            // Take the current list of members and filter out all limbo members to get active members.
            return new List<Member>(AllMembers.Where(member => !limboMemberDict.ContainsKey(member.id)));
        }
    }
}
