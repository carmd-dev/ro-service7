using Innova.DiagnosticReports;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The class which holds the error code definition, information about the error code.
    /// </summary>
    public class ErrorCodeInfoDefinition
    {
        /// <summary>
        /// Default constructor for the error code definition.
        /// </summary>
        public ErrorCodeInfoDefinition()
        {
        }

        /// <summary>
        /// The <see cref="string"/> title of the definition of the error code.
        /// </summary>
        public string Title = "";

        public string LaymansTermsTitle = "";

        public string LaymansTermDescription = "";

        /// <summary>
        /// The <see cref="string"/> possible causes of the error code.
        /// </summary>
        public string PossibleCauses = "";

        /// <summary>
        /// The <see cref="string"/> conditions for the error code.
        /// </summary>
        public string Conditions = "";

        /// <summary>
        /// The <see cref="string"/> layman terms for the conditions.
        /// </summary>
        public string LaymansTermsConditions = "";

        /// <summary>
        /// Gets or sets the <see cref="int"/> layman's term for severity level.
        /// </summary>
        public int LaymansTermSeverityLevel;

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing the severity level.
        /// </summary>
        public string LaymansTermSeverityLevelDefinition = "";

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for effect on the vehicle.
        /// </summary>
        public string LaymansTermEffectOnVehicle = "";

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for responsible component or system.
        /// </summary>
        public string LaymansTermResponsibleComponentOrSystem = "";

        /// <summary>
        /// Gets or sets the <see cref="string"/> layman's term for describing why the component or system is important.
        /// </summary>
        public string LaymansTermWhyItsImportant = "";

        /// <summary>
        /// The <see cref="string"/> file name that contains the message indicator lamp information.
        /// </summary>
        public string MessageIndicatorLampFile = "";

        /// <summary>
        /// The <see cref="string"/> URL to the file that contains the message indicator lamp information.
        /// </summary>
        public string MessageIndicatorLampFileUrl = "";

        /// <summary>
        /// The <see cref="string"/> file name that contains the monitor information.
        /// </summary>
        public string MonitorFile = "";

        /// <summary>
        /// The <see cref="string"/> URL to the file that contains the monitor information.
        /// </summary>
        public string MonitorFileUrl = "";

        /// <summary>
        /// The <see cref="string"/> monitor type.
        /// </summary>
        public string MonitorType = "";

        /// <summary>
        /// The <see cref="string"/> file name that contains the passive anti-theft indicator lamp information.
        /// </summary>
        public string PassiveAntiTheftIndicatorLampFile = "";

        /// <summary>
        /// The <see cref="string"/> URL to the file that contains the passive anti-theft indicator lamp information.
        /// </summary>
        public string PassiveAntiTheftIndicatorLampFileUrl = "";

        /// <summary>
        /// The <see cref="string"/> file name that contains the service throttle soon indicator lamp information.
        /// </summary>
        public string ServiceThrottleSoonIndicatorLampFile = "";

        /// <summary>
        /// The <see cref="string"/> URL to the file that contains the service throttle soon indicator lamp information.
        /// </summary>
        public string ServiceThrottleSoonIndicatorLampFileUrl = "";

        /// <summary>
        /// The <see cref="string"/> file name that contains the transmission control indicator lamp information.
        /// </summary>
        public string TransmissionControlIndicatorLampFile = "";

        /// <summary>
        /// The <see cref="string"/> URL to the file that contains the transmission control indicator lamp information.
        /// </summary>
        public string TransmissionControlIndicatorLampFileUrl = "";

        /// <summary>
        /// The <see cref="string"/> trips.
        /// </summary>
        public int Trips = 0;

        /// <summary>
        /// The array of <see cref="ErrorCodeInfoDefinitionVehicle"/> objects that this definition applies to.  Only valid when multiple vehicles exist for an error code definition.
        /// </summary>
        public ErrorCodeInfoDefinitionVehicle[] ErrorCodeDefinitionVehicles = null;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="DiagnosticReportResultErrorCodeDefinitionDisplay"/> object to create the object from.</param>
        /// <param name="hasMultipleDefinitions">A <see cref="bool"/> indicating whether or not there are multiple definitions.</param>
        /// <param name="includeLaymansTerms">A <see cref="bool"/> indicating whether or not to include laymans terms.</param>
        /// <returns><see cref="ErrorCodeInfoDefinition"/> object created from the supplied SDK object.</returns>
        protected internal static ErrorCodeInfoDefinition GetWebServiceObject(DiagnosticReportResultErrorCodeDefinitionDisplay sdkObject, bool hasMultipleDefinitions, bool includeLaymansTerms)
        {
            ErrorCodeInfoDefinition wsObject = new ErrorCodeInfoDefinition();

            wsObject.Title = XmlHelper.CleanInvalidXmlChars(sdkObject.Title_Translated);

            wsObject.PossibleCauses = XmlHelper.CleanInvalidXmlChars(sdkObject.PossibleCauses_Translated);
            wsObject.Conditions = XmlHelper.CleanInvalidXmlChars(sdkObject.Conditions_Translated);

            wsObject.MessageIndicatorLampFile = sdkObject.MessageIndicatorLampFile;
            wsObject.MessageIndicatorLampFileUrl = Global.DtcInfoRootUrl + sdkObject.MessageIndicatorLampFile;
            wsObject.MonitorFile = sdkObject.MonitorFile;
            wsObject.MonitorFileUrl = Global.DtcInfoRootUrl + sdkObject.MonitorFile;
            wsObject.MonitorType = sdkObject.MonitorType;
            wsObject.PassiveAntiTheftIndicatorLampFile = sdkObject.PassiveAntiTheftIndicatorLampFile;
            wsObject.PassiveAntiTheftIndicatorLampFileUrl = Global.DtcInfoRootUrl + sdkObject.PassiveAntiTheftIndicatorLampFile;
            wsObject.ServiceThrottleSoonIndicatorLampFile = sdkObject.ServiceThrottleSoonIndicatorLampFile;
            wsObject.ServiceThrottleSoonIndicatorLampFileUrl = Global.DtcInfoRootUrl + sdkObject.ServiceThrottleSoonIndicatorLampFile;
            wsObject.TransmissionControlIndicatorLampFile = sdkObject.TransmissionControlIndicatorLampFile;
            wsObject.TransmissionControlIndicatorLampFileUrl = Global.DtcInfoRootUrl + sdkObject.TransmissionControlIndicatorLampFile;
            wsObject.Trips = sdkObject.Trips;

            if (includeLaymansTerms)
            {
                wsObject.LaymansTermsTitle = XmlHelper.CleanInvalidXmlChars(sdkObject.LaymansTermTitle_Translated);
                wsObject.LaymansTermDescription = XmlHelper.CleanInvalidXmlChars(sdkObject.LaymansTermDescription_Translated);

                wsObject.LaymansTermsConditions = XmlHelper.CleanInvalidXmlChars(sdkObject.LaymansTermConditions_Translated);
                wsObject.LaymansTermSeverityLevel = sdkObject.LaymansTermSeverityLevel;
                wsObject.LaymansTermSeverityLevelDefinition = XmlHelper.CleanInvalidXmlChars(sdkObject.LaymansTermSeverityLevelDefinition_Translated);
                wsObject.LaymansTermEffectOnVehicle = XmlHelper.CleanInvalidXmlChars(sdkObject.LaymansTermEffectOnVehicle_Translated);
                wsObject.LaymansTermResponsibleComponentOrSystem = XmlHelper.CleanInvalidXmlChars(sdkObject.LaymansTermResponsibleComponentOrSystem_Translated); // Updated on 2017-12-14 by Nam Lu - INNOVA Dev Team
                wsObject.LaymansTermWhyItsImportant = XmlHelper.CleanInvalidXmlChars(sdkObject.LaymansTermWhyItsImportant_Translated); // Updated on 2017-12-14 by Nam Lu - INNOVA Dev Team
            }

            //if we have multiple definitions then we need to add the vehicles to the list
            if (hasMultipleDefinitions)
            {
                if (sdkObject.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles != null && sdkObject.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles.Count > 0)
                {
                    //set the vehicles here
                    wsObject.ErrorCodeDefinitionVehicles = new ErrorCodeInfoDefinitionVehicle[sdkObject.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles.Count];

                    for (int i = 0; i < sdkObject.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles.Count; i++)
                    {
                        DiagnosticReportResultErrorCodeDefinitionDisplayVehicle defv = sdkObject.DiagnosticReportResultErrorCodeDefinitionDisplayVehicles[i];

                        ErrorCodeInfoDefinitionVehicle ecidv = new ErrorCodeInfoDefinitionVehicle();
                        ecidv.BodyCode = defv.BodyCode;
                        ecidv.EngineType = defv.EngineType;

                        wsObject.ErrorCodeDefinitionVehicles[i] = ecidv;
                    }
                }
                else
                {
                    //empty array
                    wsObject.ErrorCodeDefinitionVehicles = new ErrorCodeInfoDefinitionVehicle[0];
                }
            }

            return wsObject;
        }
    }
}