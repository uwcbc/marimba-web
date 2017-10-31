namespace Marimba.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Type of transaction performed
    /// </summary>
    public enum TransactionType { Asset = 0, Depreciation = 1, Expense = 2, Revenue = 3 }

    /// <summary>
    /// An enumeration to keep track of what task the email browser is doing
    /// </summary>
    public enum EmailPurpose { Receive, Send, Bcc, MassEmail, Reply, Forward, Election }

    /// <summary>
    /// The type of change made, for the purpose of recording history of changes.
    /// NOTE: You need to always add new types to the end, since we store to file using ints.
    /// </summary>
    public enum ChangeType
    {
        Signup = 0,
        Signin = 1,
        EditMember = 2,
        Unsubscribe = 3,
        Deactivate = 4,
        AddUser = 5,
        SentEmail = 6,
        AddFees = 7,
        AddBudget = 8,
        EditBudget = 9,
        DeleteBudget = 10,
        SetupElection = 11,
        NewTerm = 12,
        ImportMembers = 13,
        EditAttendance = 14,
        RemoveFromTerm = 15,
        PurgeMembers = 16,
        EditUser = 17,
        DeleteUser = 18,
        AddToTerm = 19
    }
}
