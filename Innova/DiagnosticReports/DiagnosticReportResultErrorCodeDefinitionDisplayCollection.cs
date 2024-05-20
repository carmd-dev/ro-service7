namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Collection class which holds the <see cref="DiagnosticReportResultErrorCodeDefinition"/> object.
    /// </summary>
    public class DiagnosticReportResultErrorCodeDefinitionDisplayCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Default constructor for the list.
        /// </summary>
        public DiagnosticReportResultErrorCodeDefinitionDisplayCollection()
        {
        }

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportResultErrorCodeDefinitionDisplay"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DiagnosticReportResultErrorCodeDefinitionDisplay this[int index]
        {
            get
            {
                return (DiagnosticReportResultErrorCodeDefinitionDisplay)List[index];
            }
        }

        /// <summary>
        /// Adds a <see cref="DiagnosticReportResultErrorCodeDefinitionDisplay"/> object to the list.
        /// </summary>
        /// <param name="diagnosticReportResultErrorCodeDefinitionDisplay"><see cref="DiagnosticReportResultErrorCodeDefinitionDisplay"/> object to add.</param>
        public void Add(DiagnosticReportResultErrorCodeDefinitionDisplay diagnosticReportResultErrorCodeDefinitionDisplay)
        {
            base.Add(diagnosticReportResultErrorCodeDefinitionDisplay);
        }
    }
}