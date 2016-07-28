namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Marimba.Utility;

    /// <summary>
    /// A budget entry tracked by Marimba
    /// </summary>
    public class BudgetItem
    {
        /// <summary>
        /// stores the magnitude of the revenue/expense
        /// </summary>
        public double value;

        /// <summary>
        /// string description of the item provided by user
        /// </summary>
        public string name;

        /// <summary>
        /// date the transaction took place
        /// </summary>
        public DateTime dateOccur;

        /// <summary>
        /// date of transaction in the actual accounts
        /// </summary>
        public DateTime dateAccount;

        /// <summary>
        /// allows users to organize transactions using string category
        /// </summary>
        public string cat;

        /// <summary>
        /// whether item is asset, depreciation, revenue, or expense
        /// </summary>
        public TransactionType type;

        /// <summary>
        /// index of the term that this transaction took place in
        /// </summary>
        public int term;

        /// <summary>
        /// record any additional information about the item
        /// </summary>
        public string comment;

        /// <summary>
        /// If depreciation, stores a reference to the asset it depreciates
        /// </summary>
        public BudgetItem depOfAsset;

        public IList<object> Export()
        {
            List<object> output = new List<object>
            {
                this.name, this.value, this.dateOccur, this.dateAccount, this.cat, this.type, this.term, this.comment
            };

            if (type == TransactionType.Depreciation)
            {
                output.Add(ClsStorage.currentClub.budget.IndexOf(this.depOfAsset));
            }

            return output;
        }
    }
}
