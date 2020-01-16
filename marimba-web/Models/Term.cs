using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    public class Term
    {
        public string name { get; set; } // i.e. W2020
        public IList<Member> members { get; set; }
        public IList<Member> limboMembers { get; set; }
        public IList<Rehearsal> rehearsals { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal feeAmount { get; set; }
    }
}
