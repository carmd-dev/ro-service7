using System.ComponentModel;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Summary description for DiagnosticReportStatus.
    /// </summary>
    public enum DiagnosticReportFixFeedbackStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Description("Open")]
        Open = 0,

        /// <summary>
        /// Auto-Closed
        /// </summary>
        [Description("Auto-Closed")]
        AutoClosed = 1,

        /// <summary>
        /// Closed (No Fix)
        /// </summary>
        [Description("Closed (No Fix)")]
        ClosedNoFix = 2,

        /// <summary>
        /// Closed (New Fix Added)
        /// </summary>
        [Description("Closed (New Fix Added)")]
        ClosedNewFixAdded = 3,

        /// <summary>
        /// Closed (Existing Fix Selected)
        /// </summary>
        [Description("Closed (Existing Fix Selected)")]
        ClosedExistingFixSelected = 4,

        /// <summary>
        /// Pending Review (New Fix Added - Consultant Approved)
        /// </summary>
        [Description("Pending Review (Existing Fix Selected)")]
        PendingReviewExistingFixSelected = 5,

        /// <summary>
        /// Pending Review (New Fix Added)
        /// </summary>
        [Description("Pending Review (New Fix Added)")]
        PendingReviewNewFixAdded = 6,

        /// <summary>
        /// Pending Review (New Fix Added - Consultant Approved)
        /// </summary>
        [Description("Pending Review (New Fix Added - Consultant Approved)")]
        PendingReviewNewFixAddedConsultantApproved = 7,

        /// <summary>
        /// Pending (New Fix Added Pending Approval)
        /// </summary>
        [Description("Pending (New Fix Added Pending Approval)")]
        PendingNewFixAddedPendingApproval = 8,

        /// <summary>
        /// Closed (Rejected)
        /// </summary>
        [Description("Closed (Rejected)")]
        ClosedRejected = 9
    }
}