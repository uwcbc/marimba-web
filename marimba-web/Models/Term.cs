using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    public class Term
    {
        private IList<Member> members;
        private IList<Member> limboMembers;
        private IList<Rehearsal> rehearsals;
        private DateTime startDate;
        private DateTime endDate;
    }
}
