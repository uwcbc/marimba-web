using System;
using System.Collections.Generic;

namespace marimba_web.Models
{
    /// <summary>
    /// Model for a Term
    /// </summary>
    public class Term
    {
        /// <summary>
        /// The name of the term (i.e. W2020)
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// List of GUIDs of all members who attended a rehearsal in the Term
        /// </summary>
        public IList<Guid> members { get; private set; }

        /// <summary>
        /// List of all rehearsals in the Term
        /// </summary>
        public IList<Rehearsal> rehearsals { get; private set; }

        /// <summary>
        /// Start date of the term
        /// </summary>
        public DateTime startDate { get; private set; }

        /// <summary>
        /// End date of the Term
        /// </summary>
        public DateTime endDate { get; private set; }

        /// <summary>
        /// Fee amount to be paid by Members in this term
        /// </summary>
        public decimal feeAmount { get; private set; }

        /// <summary>
        /// List of GUIDs of all "active" members in the Term.
        /// TODO: Clarify what "active" means
        /// </summary>
        private IList<Guid> activeMembers { get; set; }

        /// <summary>
        /// Creates an instance of the Term class
        /// </summary>
        /// <param name="name">The term name</param>
        /// <param name="members">List of all active members in the term</param>
        /// <param name="rehearsals">List of rehearsals in the term</param>
        /// <param name="startDate">Start date of the term</param>
        /// <param name="endDate">End date of the term</param>
        /// <param name="feeAmount">Membership fee amount</param>
        public Term(string name, IList<Guid> members, IList<Rehearsal> rehearsals,
            DateTime startDate, DateTime endDate, decimal feeAmount)
        {
            this.name = name;
            this.members = members;
            this.rehearsals = rehearsals;
            this.startDate = startDate;
            this.endDate = endDate;
            this.feeAmount = feeAmount;

            // TODO: Populate activeMembers list?
        }

        /// <summary>
        /// Returns whether the Member with given GUID has attended at least one rehearsal this term.
        /// </summary>
        /// <param name="memberId">Member GUID</param>
        /// <returns>true if member is present, otherwise false</returns>
        public bool HasMember(Guid memberId)
        {
            return members.Contains(memberId);
        }

        /// <summary>
        /// Returns whether the Member with given GUID is an active member this term.
        /// </summary>
        /// <param name="memberId">Member GUID</param>
        /// <returns>true if member is present and active, otherwise false</returns>
        public bool HasActiveMember(Guid memberId)
        {
            return activeMembers.Contains(memberId);
        }
    }
}
