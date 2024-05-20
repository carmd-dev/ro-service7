using System.ComponentModel;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The various fix states of a diagnostic report.
    /// </summary>
    public enum DiagnosticReportFixStatus
    {
        /// <summary>
        /// Fix Not Needed
        /// </summary>
        [Description("Fix Not Needed")]
        FixNotNeeded = 0,

        /// <summary>
        /// Fix Found
        /// </summary>
        [Description("Fix Found")]
        FixFound = 1,

        /// <summary>
        /// Fix Not Found
        /// </summary>
        [Description("Fix Not Found")]
        FixNotFound = 2,

        /// <summary>
        /// Fix Not Found - Lookup Canceled
        /// </summary>
        [Description("Fix Not Found - Lookup Canceled")]
        FixNotFoundLookupCanceled = 3
    }
}