using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    /// <summary>
    /// Model for a Rehearsal
    /// </summary>
    public class Rehearsal
    {
        /// <summary>
        /// The date of the rehearsal
        /// </summary>
        public DateTime date { get; private set; }

        /// <summary>
        /// List of Members who attended the rehearsal
        /// </summary>
        public IList<Member> members { get; private set; }
    }
}
