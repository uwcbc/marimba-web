using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    public class Rehearsal
    {
        public DateTime date { get; set; }
        public IList<Member> members { get; set; }
    }
}
