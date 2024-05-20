using Innova.DiagnosticReports;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The class that holds error code information from the tool.  Contains Primary Code (MIL ON, Freeze Frame), Stored Codes and Pending Codes
    /// </summary>
    public class ErrorCodeInfo
    {
        /// <summary>
        /// Default constructor for the error code info object.
        /// </summary>
        public ErrorCodeInfo()
        {
        }

        /// <summary>
        /// The <see cref="string"/> code (OBD code)
        /// </summary>
        public string Code = "";

        /// <summary>
        /// The <see cref="int"/> code type. The possibilities are: (0 - Primary Error Code, 1 - First Stored Code, 2 - Additional Stored Code, 3 - First Pending Code, 4 - Additional Pending Code
        /// </summary>
        public int CodeType;

        /// <summary>
        /// The <see cref="int"/> error code system type.   Possibilities are (0 - Powertrain, 1 - OBD1, 2 - ABS, 3 - SRS, 4 - Enhanced)
        /// </summary>
        public int ErrorCodeSystemType;

        /// <summary>
        /// The <see cref="string"/> error code Repair Status.   Possibilities are (Recommend Repair, Warning)
        /// </summary>
        public string RepairStatus { get; set; }

        /// <summary>
        /// The array of <see cref="ErrorCodeInfoDefinition"/> objects associated with this error code.  Typically only one exists, but sometimes multiple definitions exist based on Body Codes and/or Engine Types that cannot be decoded.
        /// </summary>
        public ErrorCodeInfoDefinition[] ErrorCodeDefinitions;

        /// <summary>
        /// The <see cref="bool"/> flag indicating whether or not the error code has multiple definitions
        /// </summary>
        public bool HasMultipleDefinitions = false;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Vehicles.Recall"/> object to create the object from.</param>
        /// <param name="includeLaymansTerms">A <see cref="bool"/> indicating whether or not to include laymans terms.</param>
        /// <returns><see cref="ErrorCodeInfo"/> object created from the supplied SDK object.</returns>
        protected internal static ErrorCodeInfo GetWebServiceObject(Innova.DiagnosticReports.DiagnosticReportResultErrorCode sdkObject, bool includeLaymansTerms)
        {
            ErrorCodeInfo wsObject = new ErrorCodeInfo();

            //set the error code string
            wsObject.Code = sdkObject.ErrorCode;
            //set the error code type
            wsObject.CodeType = (int)sdkObject.DiagnosticReportErrorCodeType;
            //set the error code system type
            wsObject.ErrorCodeSystemType = (int)sdkObject.DiagnosticReportErrorCodeSystemType;

            //now loop through the definitions
            DiagnosticReportResultErrorCodeDefinitionDisplayCollection defs = sdkObject.GetDiagnosticReportResultErrorCodeDefinitions(DiagnosticReportResultType.CarScan);

            //set the local variable equal to 1 if the defininitions have
            wsObject.ErrorCodeDefinitions = new ErrorCodeInfoDefinition[defs.Count];

            //determine whether or not the object has multiple definitions
            wsObject.HasMultipleDefinitions = defs.Count > 1;

            for (int i = 0; i < defs.Count; i++)
            {
                //get the web service object here, pass in to the next method whether or not there are multiple definitions
                wsObject.ErrorCodeDefinitions[i] = ErrorCodeInfoDefinition.GetWebServiceObject(defs[i], wsObject.HasMultipleDefinitions, includeLaymansTerms);
            }

            return wsObject;
        }
    }
}