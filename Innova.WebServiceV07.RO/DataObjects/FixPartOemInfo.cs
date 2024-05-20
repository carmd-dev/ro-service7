using Innova.DiagnosticReports;

namespace Innova.WebServiceV07.RO.DataObjects
{
    public class FixPartOemInfo
    {
        public string retailer = "";
        public string manufacturer = "";
        public string oemPartNumber = "";

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="DiagnosticReportResultFixPartPartOem"/> object to create the object from.</param>
        /// <returns><see cref="FixPartOemInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixPartOemInfo GetWebServiceObject(Innova.DiagnosticReports.DiagnosticReportResultFixPartPartOem sdkObject)
        {
            FixPartOemInfo wsObject = new FixPartOemInfo();

            wsObject.retailer = sdkObject.Retailer;
            wsObject.manufacturer = sdkObject.Manufacturer;
            wsObject.oemPartNumber = sdkObject.OemPartNumber;

            return wsObject;
        }
    }
}