namespace Innova.DiagnosticReports
{
    /// <summary>
    /// The enumeration of diagnostic report result types.  Used to determine the type of sorting to use for displaying error code definitions.
    /// </summary>
    public enum DiagnosticReportResultType
    {
        /// <summary>
        /// The external system type
        /// </summary>
        ExternalSystem,

        /// <summary>
        /// The OBDFix type
        /// </summary>
        OBDFix,

        /// <summary>
        /// The CanOBD2 type
        /// </summary>
        CanOBD2,

        /// <summary>
        /// The CarScan type
        /// </summary>
        CarScan,
    }
}