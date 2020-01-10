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


        private decimal value;
        private string name; // using name as per architecture, but think we should use Member class
        private DateTime transactionDate;
        private DateTime processedDate;
        private FinanceCategory financeCategory;
        private FinanceType financeType;
        private Term term;
        private string comment;
        // TODO: depreciation
    }
}
