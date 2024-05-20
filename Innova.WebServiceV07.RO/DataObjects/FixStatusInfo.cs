namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The fix status class includes information about the status of a report's fix.
    /// </summary>
    public class FixStatusInfo
    {
        /// <summary>
        /// Default constructor for the FixStatusInfo class.
        /// </summary>
        public FixStatusInfo()
        {
        }

        /// <summary>
        /// The <see cref="WebServiceSessionStatus"/> associated with the diagnostic report.  Determines if there were any errors retrieving the diagnostic report including user session problems.
        /// </summary>
        public WebServiceSessionStatus WebServiceSessionStatus;

        /// <summary>
        /// The <see cref="DiagReportInfo"/> object associated with the fix status
        /// </summary>
        public DiagReportInfo DiagnosticReportInfo;

        /// <summary>
        /// The <see cref="int"/> powertrain fix status, possible values: 0 - Fix Not Needed, 1 - Fix Found, 2 - Fix Not Found, 3 - FixNotFoundLookupCanceled
        /// </summary>
        public int PwrFixStatus = 2;

        /// <summary>
        /// The <see cref="int"/> powertrain fix status description
        /// </summary>
        public string PwrFixStatusDesc = "";

        /// <summary>
        /// The <see cref="string"/> powertrain explanation for why the fix lookup was cancelled
        /// </summary>
        public string PwrFixLookupCancelledReason = "";

        /// <summary>
        /// The <see cref="int"/> OBD1 status, possible values: 0 - Fix Not Needed, 1 - Fix Found, 2 - Fix Not Found, 3 - FixNotFoundLookupCanceled
        /// </summary>
        public int Obd1FixStatus = 2;

        /// <summary>
        /// The <see cref="int"/> OBD1 fix status description
        /// </summary>
        public string Obd1FixStatusDesc = "";

        /// <summary>
        /// The <see cref="string"/> OBD1 explanation for why the fix lookup was cancelled.
        /// </summary>
        public string Obd1FixLookupCancelledReason = "";

        /// <summary>
        /// The <see cref="int"/> ABS status, possible values: 0 - Fix Not Needed, 1 - Fix Found, 2 - Fix Not Found, 3 - FixNotFoundLookupCanceled
        /// </summary>
        public int AbsFixStatus = 2;

        /// <summary>
        /// The <see cref="int"/> ABS fix status description
        /// </summary>
        public string AbsFixStatusDesc = "";

        /// <summary>
        /// The <see cref="string"/> ABS explanation for why the fix lookup was cancelled
        /// </summary>
        public string AbsFixLookupCancelledReason = "";

        /// <summary>
        /// The <see cref="int"/> SRS status, possible values: 0 - Fix Not Needed, 1 - Fix Found, 2 - Fix Not Found, 3 - FixNotFoundLookupCanceled
        /// </summary>
        public int SrsFixStatus = 2;

        /// <summary>
        /// The <see cref="int"/> SRS fix status description
        /// </summary>
        public string SrsFixStatusDesc = "";

        /// <summary>
        /// The <see cref="string"/> SRS explanation for why the fix lookup was cancelled
        /// </summary>
        public string SrsFixLookupCancelledReason = "";
    }
}