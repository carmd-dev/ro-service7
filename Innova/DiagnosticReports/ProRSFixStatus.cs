using System.ComponentModel;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Summary description for DiagnosticReportStatus.
    /// </summary>
    public enum ProRSFixStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Description("Pending Review")]
        PendingReview = 0,

        /// <summary>
        /// Rejected
        /// </summary>
        [Description("Rejected")]
        Rejected = 1,

        /// <summary>
        /// Used For Creation Of New Fix
        /// </summary>
        [Description("Used For Creation Of New Fix)")]
        UsedForCreationOfNewFix = 2
    }
}