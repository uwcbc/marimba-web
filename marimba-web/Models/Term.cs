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

        public Member GetMemberByGuid(Guid id)
        {
            return memberDict.GetValueOrDefault(id, null);
        }

        public bool IsMember(Guid id)
        {
            return memberDict.ContainsKey(id);
        }

        public bool IsLimboMember(Guid id)
        {
            return limboMemberDict.ContainsKey(id);
        }

        public HashSet<Guid> GetAllMemberIds()
        {
            return memberDict.Keys.ToHashSet();
        }

        public HashSet<Guid> GetLimboMemberIds()
        {
            return limboMemberDict.Keys.ToHashSet();
        }

        public HashSet<Guid> GetNonLimboMemberIds()
        {
            return memberDict.Keys.Except(limboMemberDict.Keys).ToHashSet();
        }
    }
}
