using System.ComponentModel;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Summary description for DiagnosticReportStatus.
    /// </summary>
    public enum DiagnosticReportType
    {
        /// <summary>
        /// Powertrain OBD1
        /// </summary>
        [Description("Powertrain OBD1")]
        PowertrainObd1 = 0,

        /// <summary>
        /// Powertrain OBD2
        /// </summary>
        [Description("Powertrain OBD2")]
        PowertrainObd2 = 1,

        /// <summary>
        /// ABS
        /// </summary>
        [Description("ABS")]
        ABS = 2,

        /// <summary>
        /// SRS
        /// </summary>
        [Description("SRS")]
        SRS = 3
    }
}