namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Contains the diagnostic report error code object with the associated data.
    /// </summary>
    public class DiagnosticReportErrorCode
    {
        private string errorCode;
        private DiagnosticReportErrorCodeType diagnosticReportErrorCodeType;
        private PossibleCauseCollection possibleCauses;
        private bool unableToFindCodeData;

        /// <summary>
        /// The error code stringl.
        /// </summary>
        public string ErrorCode
        {
            get
            {
                return errorCode;
            }
        }

        /// <summary>
        /// Gets the <see cref="PossibleCauseCollection"/> of <see cref="PossibleCause"/> objects that are associated with the error code. Multiple is possible if the correct vehicle is not found.
        /// </summary>
        public PossibleCauseCollection PossibleCauses
        {
            get
            {
                return this.possibleCauses;
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag that indicates whether or not data was found for the code.
        /// </summary>
        public bool UnableToFindCodeData
        {
            get
            {
                return this.unableToFindCodeData;
            }
        }

        /// <summary>
        /// Gets the <see cref="DiagnosticReportErrorCodeType"/> of error code.
        /// </summary>
        public DiagnosticReportErrorCodeType DiagnosticReportErrorCodeType
        {
            get
            {
                return this.diagnosticReportErrorCodeType;
            }
        }
    }
}