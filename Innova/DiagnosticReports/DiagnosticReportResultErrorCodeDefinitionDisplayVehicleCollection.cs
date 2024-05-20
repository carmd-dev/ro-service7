namespace Innova.DiagnosticReports
{
    /// <summary>
    /// Collection of <see cref="DiagnosticReportResultErrorCodeDefinitionDisplayVehicle"/> objects.
    /// </summary>
    public class DiagnosticReportResultErrorCodeDefinitionDisplayVehicleCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiagnosticReportResultErrorCodeDefinitionDisplayVehicleCollection()
        {
        }

        /// <summary>
        /// Indexer, used to return the <see cref="DiagnosticReportResultErrorCodeDefinitionDisplayVehicle"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public DiagnosticReportResultErrorCodeDefinitionDisplayVehicle this[int index]
        {
            get
            {
                return (DiagnosticReportResultErrorCodeDefinitionDisplayVehicle)List[index];
            }
        }

        /// <summary>
        /// Adds a <see cref="DiagnosticReportResultErrorCodeDefinitionDisplayVehicle"/> object to the list.
        /// </summary>
        /// <param name="diagnosticReportResultErrorCodeDefinitionDisplayVehicle"><see cref="DiagnosticReportResultErrorCodeDefinitionDisplayVehicle"/> object to add to the list.</param>
        public void Add(DiagnosticReportResultErrorCodeDefinitionDisplayVehicle diagnosticReportResultErrorCodeDefinitionDisplayVehicle)
        {
            base.Add(diagnosticReportResultErrorCodeDefinitionDisplayVehicle);
        }
    }
}