namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Summary description for ErrorCodeCollection.
    /// </summary>
    public class DiagnosticReportErrorCodeCollection : Metafuse3.BusinessObjects.SmartCollection
    {
        /// <summary>
        ///
        /// </summary>
        public DiagnosticReportErrorCodeCollection() : base()
        {
        }

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportErrorCode"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public new DiagnosticReportErrorCode this[int index]
        {
            get
            {
                return (DiagnosticReportErrorCode)List[index];
            }
        }

        /// <summary>
        /// Adds an <see cref="DiagnosticReportErrorCode"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="DiagnosticReportErrorCode"/> to add.</param>
        public void Add(DiagnosticReportErrorCode value)
        {
            base.Add(value);
        }
    }
}