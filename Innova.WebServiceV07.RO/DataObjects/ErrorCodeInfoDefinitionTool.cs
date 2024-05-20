using Innova.DiagnosticReports;
using System.Collections.Generic;

namespace Innova.WebServiceV07.RO.DataObjects
{
    //Add ErrorCode Definitions to ToolInfo - Added by INNOVA DEV TEAM 6/20/2017
    /// <summary>
    /// The class which holds the error code definition, information about the error code.
    /// </summary>
    public class ErrorCodeInfoDefinitionTool : ErrorCodeInfoDefinition
    {
        /// <summary>
        ///
        /// </summary>
        public string ErrorCode = "";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected internal static List<ErrorCodeInfoDefinitionTool> GetWebServiceObjects(List<DiagnosticReportResultErrorCodeDefinition> sdkObjects, bool hasMultipleDefinitions)
        {
            var result = new List<ErrorCodeInfoDefinitionTool>();
            foreach (DiagnosticReportResultErrorCodeDefinition def in sdkObjects)
            {
                result.Add(GetWebServiceObject(def, hasMultipleDefinitions, true));
            }

            return result;
        }

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="DiagnosticReportResultErrorCodeDefinitionDisplay"/> object to create the object from.</param>
        /// <param name="hasMultipleDefinitions">A <see cref="bool"/> indicating whether or not there are multiple definitions.</param>
        /// <param name="includeLaymansTerms">A <see cref="bool"/> indicating whether or not to include laymans terms.</param>
        /// <returns><see cref="ErrorCodeInfoDefinition"/> object created from the supplied SDK object.</returns>
        protected internal static ErrorCodeInfoDefinitionTool GetWebServiceObject(DiagnosticReportResultErrorCodeDefinition sdkObject, bool hasMultipleDefinitions, bool includeLaymansTerms)
        {
            ErrorCodeInfoDefinitionTool wsObject = new ErrorCodeInfoDefinitionTool();

            wsObject.Title = sdkObject.Title_Translated;

            wsObject.ErrorCode = sdkObject.DTCCode.ErrorCode;
            wsObject.PossibleCauses = sdkObject.PossibleCauses_Translated;
            wsObject.Conditions = sdkObject.Conditions_Translated;

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
                wsObject.LaymansTermsTitle = sdkObject.LaymansTermTitle_Translated;
                wsObject.LaymansTermDescription = sdkObject.LaymansTermDescription_Translated;

                wsObject.LaymansTermsConditions = sdkObject.LaymansTermConditions_Translated;
                wsObject.LaymansTermSeverityLevel = sdkObject.LaymansTermSeverityLevel;
                wsObject.LaymansTermSeverityLevelDefinition = sdkObject.LaymansTermSeverityLevelDefinition_Translated;
                wsObject.LaymansTermEffectOnVehicle = sdkObject.LaymansTermEffectOnVehicle_Translated;
                wsObject.LaymansTermResponsibleComponentOrSystem = sdkObject.LaymansTermResponsibleComponentOrSystem_Translated;
                wsObject.LaymansTermWhyItsImportant = sdkObject.LaymansTermWhyItsImportant_Translated;
            }

            return wsObject;
        }
    }
}