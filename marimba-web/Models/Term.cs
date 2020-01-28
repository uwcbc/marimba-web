using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    public class Term
    {
        public string name { get; set; } // i.e. W2020
        public IList<Member> allMembers { get; set; }
        public IList<Member> limboMembers { get; set; } // Subset of allMembers
        public IList<Rehearsal> rehearsals { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal feeAmount { get; set; }

        // Internal structures for faster lookup by GUID.
        private Dictionary<Guid, Member> memberDict;
        private Dictionary<Guid, Member> limboMemberDict;

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

        /*
         * Initialize Guid-to-Member dictionaries for all members and limbo members.
         * Used for more efficient lookup of Members by GUID.
         */
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

        /*
         * Get member with the given GUID, returning null if no such member exists.
         */
        public Member GetMemberByGuid(Guid id)
        {
            return memberDict.GetValueOrDefault(id, null);
        }

        /*
         * Return true if member with given GUID exists in term, otherwise false.
         */ 
        public bool IsMember(Guid id)
        {
            return memberDict.ContainsKey(id);
        }

        /*
         * Return true if member with given GUID is limbo, otherwise false.
         */
        public bool IsLimboMember(Guid id)
        {
            return limboMemberDict.ContainsKey(id);
        }

        /*
         * Return set of all member GUIDs.
         */
        public HashSet<Guid> GetAllMemberIds()
        {
            return memberDict.Keys.ToHashSet();
        }

        /*
         * Return set of all limbo member GUIDs.
         */
        public HashSet<Guid> GetLimboMemberIds()
        {
            return limboMemberDict.Keys.ToHashSet();
        }

        /*
         * Return set of all non-limbo member GUIDs.
         */
        public HashSet<Guid> GetNonLimboMemberIds()
        {
            return memberDict.Keys.Except(limboMemberDict.Keys).ToHashSet();
        }
    }
}
