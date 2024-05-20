using Innova.DiagnosticReports;
using System;

namespace Innova.WebServiceV07.RO.DataObjects
{
    public class FixFeedbackInfo
    {
        /// <summary>
        /// Default constructor for the fix feedback info object.
        /// </summary>
        public FixFeedbackInfo()
        {
        }

        /// <summary>
        /// Gets the <see cref="Guid"/> of the diagnostic report the fis feedback applies to.
        /// </summary>
        public Guid DiagnosticReportId;

        /// <summary>
        /// Gets the <see cref="bool"/> indicating if the report is valid.
        /// </summary>
        public bool IsReportValid = true;

        /// <summary>
        /// Gets the <see cref="string"/> reason the vehicle could not be fixed.
        /// </summary>
        public string CouldNotFixReason = "";

        /// <summary>
        /// Gets the <see cref="string"/> primary error code for the fix.
        /// </summary>
        public string PrimaryErrorCode = "";

        /// <summary>
        /// Gets the <see cref="string"/> name of the error code system type.
        /// </summary>
        public string DiagnosticReportErrorCodeSystemType = "";

        /// <summary>
        /// Gets the <see cref="string"/> name of the fix.
        /// </summary>
        public string Fix = "";

        /// <summary>
        /// Gets the <see cref="int"/> average diagnostic time in minutes.
        /// </summary>
        public int AverageDiagnosticTimeMinutes = 0;

        /// <summary>
        /// Gets the <see cref="int"/> frequency with which this fix has been encountered.
        /// </summary>
        public int FrequencyEncountered = 0;

        /// <summary>
        /// Gets the <see cref="string"/> description of the fix difficulty rating.
        /// </summary>
        public string FixDifficultyRating = "";

        /// <summary>
        /// Gets the <see cref="string"/> list of error codes that the technician thinks should apply to the fix.
        /// </summary>
        public string ErrorCodesThatApply = "";

        /// <summary>
        /// Gets the <see cref="string"/> technician comments.
        /// </summary>
        public string TechComments = "";

        /// <summary>
        /// Gets the <see cref="string"/> basic tools required.
        /// </summary>
        public string BasicToolsRequired = "";

        /// <summary>
        /// Gets <see cref="string"/> specialty tools required.
        /// </summary>
        public string SpecialtyToolsRequired = "";

        /// <summary>
        /// Gets or sets <see cref="string"/> tips and tricks.
        /// </summary>
        public string TipsAndTricks = "";

        /// <summary>
        /// Gets the array of <see cref="FixFeedbackPartInfo"/> objects associated with this fix feedback.
        /// </summary>
        public FixFeedbackPartInfo[] Parts;

        //ToolDB_
        /// <summary>
        /// Gets the array of <see cref="FixFeedbackToolInfo"/> objects associated with this fix feedback.
        /// </summary>
        public FixFeedbackToolInfo[] Tools;

        //ToolDB_

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.DiagnosticReports.DiagnosticReportFixFeedback"/> object to create the object from.</param>
        /// <returns><see cref="FixFeedbackInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixFeedbackInfo GetWebServiceObject(Innova.DiagnosticReports.DiagnosticReportFixFeedback sdkObject)
        {
            FixFeedbackInfo wsObject = new FixFeedbackInfo();

            wsObject.DiagnosticReportId = sdkObject.DiagnosticReport.Id;
            wsObject.IsReportValid = sdkObject.IsReportValid;
            wsObject.CouldNotFixReason = sdkObject.CouldNotFixReason;
            wsObject.PrimaryErrorCode = sdkObject.PrimaryErrorCode;
            wsObject.DiagnosticReportErrorCodeSystemType = sdkObject.Registry.GetEnumDescription(sdkObject.DiagnosticReportErrorCodeSystemType);
            if (sdkObject.Fix != null)
            {
                wsObject.Fix = sdkObject.Fix.FixName.Description;
            }
            wsObject.AverageDiagnosticTimeMinutes = sdkObject.AverageDiagnosticTime;
            wsObject.FrequencyEncountered = sdkObject.FrequencyEncountered;
            wsObject.FixDifficultyRating = sdkObject.Registry.GetEnumDescription(sdkObject.FixDifficultyRating);
            wsObject.ErrorCodesThatApply = sdkObject.ErrorCodesThatApply;
            wsObject.TechComments = sdkObject.TechComments;
            wsObject.BasicToolsRequired = sdkObject.BasicToolsRequired;
            wsObject.SpecialtyToolsRequired = sdkObject.SpecialtyToolsRequired;
            wsObject.TipsAndTricks = sdkObject.TipsAndTricks;

            DiagnosticReportFixFeedbackPartCollection parts = sdkObject.Parts;
            wsObject.Parts = new FixFeedbackPartInfo[parts.Count];

            for (int i = 0; i < parts.Count; i++)
            {
                wsObject.Parts[i] = FixFeedbackPartInfo.GetWebServiceObject(parts[i]);
            }

            //ToolDB_
            DiagnosticReportFixFeedbackToolCollection tools = sdkObject.Tools;
            wsObject.Tools = new FixFeedbackToolInfo[tools.Count];

            for (int i = 0; i < tools.Count; i++)
            {
                wsObject.Tools[i] = FixFeedbackToolInfo.GetWebServiceObject(tools[i]);
            }
            //ToolDB_

            return wsObject;
        }
    }
}