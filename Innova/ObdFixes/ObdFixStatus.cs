using System.ComponentModel;

namespace Innova.ObdFixes
{
    /// <summary>
    /// Summary description for ObdFixStatus.
    /// </summary>
    public enum ObdFixStatus
    {
        /// <summary>
        /// PendingReview.
        /// </summary>
        [Description("Pending Review")]
        PendingReview = 0,

        /// <summary>
        /// Rejected.
        /// </summary>
        [Description("Rejected")]
        Rejected = 1,

        /// <summary>
        /// Used For Creation Of New Fix.
        /// </summary>
        [Description("Used For Creation Of New Fix")]
        UsedForCreationOfNewFix = 2
    }
}