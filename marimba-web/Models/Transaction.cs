using System;
using marimba_web.Common;

namespace marimba_web.Models
{
    /// <summary>
    /// Model for financial Transactions
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Monetary value of the transaction
        /// </summary>
        public decimal value { get; private set; }

        /// <summary>
        /// Name of the Transaction
        /// </summary>
        public string name { get; private set; } 

        /// <summary>
        /// Date that the Transaction occurred
        /// </summary>
        public DateTime transactionDate { get; private set; }

        /// <summary>
        /// Date that the Transaction was processed into records
        /// </summary>
        public DateTime processedDate { get; private set; }

        /// <summary>
        /// Category that the Transaction falls into
        /// </summary>
        public Marimba.FinanceCategory category { get; private set; }

        /// <summary>
        /// The general type of the Transaction (i.e. Revenue, Expense, etc.)
        /// </summary>
        public Marimba.FinanceType type { get; private set; }

        /// <summary>
        /// Term that the Transaction was made in
        /// </summary>
        public Term term { get; private set; }

        /// <summary>
        /// Comment on the Transaction
        /// </summary>
        public string comment { get; private set; }

        /// <summary>
        /// Depreciation of the Transaction
        /// </summary>
        // might change to reference to another Transaction, need to talk to Skye
        public decimal depreciation { get; private set; }

        /// <summary>
        /// Constructs a Transaction object
        /// </summary>
        /// <param name="value">The value of the Transaction</param>
        /// <param name="name">The name of the Transaction</param>
        /// <param name="transactionDate">The date the Transaction occurred</param>
        /// <param name="processedDate">The date the Transaction was processed into records</param>
        /// <param name="category">The category the Transaction falls into</param>
        /// <param name="type">The general type of Transaction (i.e. Expense, Revenue)</param>
        /// <param name="term">The Term the Transaction was made in</param>
        /// <param name="comment">Comment on the Transaction</param>
        public Transaction(
            decimal value, 
            string name, 
            DateTime transactionDate, 
            DateTime processedDate, 
            Marimba.FinanceCategory category,
            Marimba.FinanceType type, 
            Term term, 
            string comment = "")
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

        public void SetValue(decimal value){
        
            this.value = value;

        }

        public void SetName(string name){
        
            this.name = name;

        }

        public void SetTransactionDate(DateTime transactionDate){
        
            this.transactionDate = transactionDate;

        }
        public void SetProcessedDate(DateTime processedDate){
        
            this.processedDate = processedDate;

        }
        public void SetCategory(Marimba.FinanceCategory category){
        
            this.category = category

        }
        public void SetTransactionDate(Marimba.FinanceType type){
        
           this.type = type;

        }
        public void SetTerm(Term term){
        
           this.term = term;

        }
        public void SetComment(string comment){
        
           this.comment = comment;
        }

        

         public void UpdateAll(
            decimal value, 
            string name, 
            DateTime transactionDate, 
            DateTime processedDate, 
            Marimba.FinanceCategory category,
            Marimba.FinanceType type, 
            Term term, 
            string comment = "")
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
