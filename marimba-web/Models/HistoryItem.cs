using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    public class HistoryItem
    {
        public Member who { get; set; }
        public DateTime when { get; set; }
        public string what { get; set; }
    }
}
