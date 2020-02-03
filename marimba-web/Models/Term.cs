using System;
using System.Collections.Generic;
using System.Linq;

namespace marimba_web.Models
{
    /// <summary>
    /// Model for a Term
    /// </summary>
    public class Term
    {
        /// <summary>
        /// The name of the term (i.e. W2020
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// List of all Members in the Term
        /// </summary>
        public IList<Member> allMembers { get; private set; }

        /// <summary>
        /// List of all Members in limbo in the Term(subset of allMembers)
        /// </summary>
        public IList<Member> limboMembers { get; private set; }

        /// <summary>
        /// List of all rehearsals in the Term
        /// </summary>
        public IList<Rehearsal> rehearsals { get; private set; }

        /// <summary>
        /// Start date of the term
        /// </summary>
        public DateTime startDate { get; private set; }

        /// <summary>
        /// End date of the Term
        /// </summary>
        public DateTime endDate { get; private set; }

        /// <summary>
        /// Fee amount to be paid by Members in this term
        /// </summary>
        public decimal feeAmount { get; private set; }

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
            this.name = name;
            this.allMembers = allMembers;
            this.limboMembers = limboMembers;
            this.rehearsals = rehearsals;
            this.startDate = startDate;
            this.endDate = endDate;
            this.feeAmount = feeAmount;

            InitializeDicts();
        }

        /// <summary>
        /// Initialize Guid-to-Member dictionaries for all members and limbo members.
        /// Used for more efficient lookup of Members by GUID.
        /// </summary>
        private void InitializeDicts()
        {
            memberDict = new Dictionary<Guid, Member>();
            foreach (var m in allMembers)
            {
                memberDict.Add(m.id, m);
            }

            limboMemberDict = new Dictionary<Guid, Member>();
            foreach (var m in limboMembers)
            {
                limboMemberDict.Add(m.id, m);
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
    }
}
