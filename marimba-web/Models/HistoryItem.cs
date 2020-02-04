using System;
using marimba_web.Common;

namespace marimba_web.Models
{
    /// <summary>
    /// Model for Marimba action history items
    /// </summary>
    public class HistoryItem
    {
        /// <summary>
        /// The Member performing the action
        /// </summary>
        public Member ActionPerformer { get; private set; }

        /// <summary>
        /// The time the action was performed
        /// </summary>
        public DateTime ActionTime { get; private set; }

        /// <summary>
        /// The type of action performed
        /// </summary>
        public Marimba.ActionType Action { get; private set; }
    }
}
