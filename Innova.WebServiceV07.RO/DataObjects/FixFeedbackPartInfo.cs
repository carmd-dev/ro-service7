using System;

namespace Innova.WebServiceV07.RO.DataObjects
{
    public class FixFeedbackPartInfo
    {
        public FixFeedbackPartInfo()
        {
        }

        /// <summary>
        /// Gets the <see cref="Guid"/> id of the FixFeedbackInfo this part is associated with.
        /// </summary>
        public Guid DiagnosticReportFixFeedbackId;

        /// <summary>
        /// Gets the <see cref="string"/> part name.
        /// </summary>
        public string PartName = "";

        /// <summary>
        /// Gets the <see cref="string"/> part number.
        /// </summary>
        public string PartNumber = "";

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.DiagnosticReports.DiagnosticReportFixFeedbackPart"/> object to create the object from.</param>
        /// <returns><see cref="FixFeedbackPartInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixFeedbackPartInfo GetWebServiceObject(Innova.DiagnosticReports.DiagnosticReportFixFeedbackPart sdkObject)
        {
            FixFeedbackPartInfo wsObject = new FixFeedbackPartInfo();

            if (sdkObject.DiagnosticReportFixFeedback != null)
            {
                wsObject.DiagnosticReportFixFeedbackId = sdkObject.DiagnosticReportFixFeedback.Id;
            }
            wsObject.PartName = sdkObject.PartName;
            wsObject.PartNumber = sdkObject.PartNumber;

            return wsObject;
        }
    }
}