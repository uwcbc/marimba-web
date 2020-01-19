using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marimba_web.Common;

namespace marimba_web.Models
{
    public class HistoryItem
    {
        public Member ActionPerformer { get; set; }
        public DateTime ActionTime { get; set; }
        public Marimba.ActionType Action { get; set; }
    }
}
