using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marimba_web.Common;

namespace marimba_web.Models
{
    public class Transaction
    {

        public decimal value { get; set; }
        public string name { get; set; } 
        public DateTime transactionDate { get; set; }
        public DateTime processedDate { get; set; }
        public Marimba.FinanceCategory category { get; set; }
        public Marimba.FinanceType type { get; set; }
        public Term term { get; set; }
        public string comment { get; set; }
        // might change to reference to another Transaction, need to talk to Skye
        public decimal depreciation { get; set; }

        public Transaction(decimal value, string name, DateTime transactionDate, DateTime processedDate, Marimba.FinanceCategory category,
            Marimba.FinanceType type, Term term, string comment = "")
        {
            this.value = value;
            this.name = name;
            this.transactionDate = transactionDate;
            this.processedDate = processedDate;
            this.category = category;
            this.type = type;
            this.term = term;
            this.comment = comment;
        }
    }
}
