using System;
using System.Collections.Generic;

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
        /// List of GUIDs of members who attended the rehearsal
        /// </summary>
        public IList<Guid> members { get; private set; }

        public Rehearsal( DateTime date, IList<Guid> members )
        {
           this.date = date;
           this.members = members;
        }
        public void UpdateRehearsal( DateTime date, IList<Guid> members )
        {
           this.date = date;
           this.members = members;
        }

    }

   
}    
