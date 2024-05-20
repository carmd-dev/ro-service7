using System.ComponentModel;

namespace Innova.DiagnosticReports.Feedback
{
    /// <summary>
    /// Summary description for RepairedByType.
    /// </summary>
    public enum RepairedByType
    {
        /// <summary>
        /// Self
        /// </summary>
        [Description("Me")]
        Self = 0,

        /// <summary>
        /// Professional
        /// </summary>
        [Description("Professional")]
        Professional = 1
    }
}