using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marimba_web.Models
{
    public class Finance    // think we should use Transaction instead of Finance
    {

        // TODO: determine values for these enums
        public enum FinanceCategory
        {

        }
        public enum FinanceType
        {

        }


        public decimal value { get; set; }
        public string name { get; set; } // using name as per architecture, but think we should use Member class
        public DateTime transactionDate { get; set; }
        public DateTime processedDate { get; set; }
        public FinanceCategory financeCategory { get; set; }
        public FinanceType financeType { get; set; }
        public Term term { get; set; }
        public string comment { get; set; }
        // TODO: depreciation
    }
}
