using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marimba.Utility
{
    public class Enumerations
    {
        public enum TransactionType { Asset = 0, Depreciation = 1, Expense = 1, Revenue = 2 }

        public enum EmailPurpose { Receive, Send, Bcc, MassEmail, Reply, Forward, Election };
    }
}
