using System.ComponentModel;

namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The enumeration which defines what system type of diagnostic report error code an error code is when displayed on the diagnostic report.
    /// </summary>
    public enum DiagnosticReportErrorCodeSystemType
    {
        /// <summary>
        /// Indicates that the error code is a OBD2 powertrain code.
        /// </summary>
        [Description("OBD2")]
        PowertrainObd2 = 0,

        /// <summary>
        /// Indicates that the error code is a OBD1 powertrain code.
        /// </summary>
        [Description("OBD1")]
        PowertrainOBD1 = 1,

        /// <summary>
        /// Indicates that the error code is an ABS code.
        /// </summary>
        [Description("ABS")]
        ABS = 2,

        /// <summary>
        /// Indicates that the error code is an SRS code.
        /// </summary>
        [Description("SRS")]
        SRS = 3,

        /// <summary>
        /// Indicates that the error code is an Enhanced code.
        /// </summary>
        [Description("Enhanced")]
        Enhanced = 4,

        /// <summary>
        /// Indicates that the error code is TCM code.
        /// </summary>
        [Description("TCM")]
        TCM = 5,

        /// <summary>
        /// Indicates that the error code is TPMS code.
        /// </summary>
        [Description("TPMS")]
        TPMS = 6
    }
}