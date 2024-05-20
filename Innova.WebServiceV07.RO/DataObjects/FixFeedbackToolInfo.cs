using System;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    ///
    /// </summary>
	public class FixFeedbackToolInfo
    {
        /// <summary>
        /// Gets the <see cref="Guid"/> id of the FixFeedbackInfo this part is associated with.
        /// </summary>
        public Guid DiagnosticReportFixFeedbackId;

        /// <summary>
        /// Gets the <see cref="string"/> part name.
        /// </summary>
        public string ToolNumber = "";

        /// <summary>
        /// Gets the <see cref="string"/> part number.
        /// </summary>
        public string ToolDescription = "";

        /// <summary>
        /// Gets the <see cref="string"/> part quantity.
        /// </summary>
        public int Quantity = 0;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.DiagnosticReports.DiagnosticReportFixFeedbackTool"/> object to create the object from.</param>
        /// <returns><see cref="FixFeedbackToolInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixFeedbackToolInfo GetWebServiceObject(Innova.DiagnosticReports.DiagnosticReportFixFeedbackTool sdkObject)
        {
            FixFeedbackToolInfo wsObject = new FixFeedbackToolInfo();

            if (sdkObject.DiagnosticReportFixFeedback != null)
            {
                wsObject.DiagnosticReportFixFeedbackId = sdkObject.DiagnosticReportFixFeedback.Id;
            }
            wsObject.ToolNumber = sdkObject.ToolNumber;
            wsObject.ToolDescription = sdkObject.ToolDescription;
            wsObject.Quantity = sdkObject.Quantity;

            return wsObject;
        }
    }
}