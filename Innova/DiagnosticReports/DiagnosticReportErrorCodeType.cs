namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The enumeration which defines what type of diagnostic report error code an error code is when displayed on the diagnostic report.
    /// </summary>
    public enum DiagnosticReportErrorCodeType
    {
        /// <summary>
        /// Indicates that the error code should be shown as the primary diagnostic report code.
        /// </summary>
        PrimaryDiagnosticReportErrorCode,

        /// <summary>
        /// Indicates that the error code should be shown as the first stored diagnostic report code.
        /// </summary>
        FirstStoredDiagnosticReportErrorCode,

        /// <summary>
        /// Indicates that the error code should be shown as an additional stored diagnostic report code.
        /// </summary>
        AdditionalStoredDiagnosticReportErrorCode,

        /// <summary>
        /// Indicates that the error code should be shown as the first pending diagnostic report code.
        /// </summary>
        FirstPendingDiagnosticReportErrorCode,

        /// <summary>
        /// Indicates that the error code should be shown as an additional pending diagnostic report code.
        /// </summary>
        AdditionalPendingDiagnosticReportErrorCode,

        /// <summary>
        /// Indicates that the error code should be shown as the first permanent diagnostic report code.
        /// </summary>
        FirstPermanentDiagnosticReportErrorCode,

        /// <summary>
        /// Indicates that the error code should be shown as an additional permanent diagnostic report code.
        /// </summary>
        AdditionalPermanentDiagnosticReportErrorCode
    }
}