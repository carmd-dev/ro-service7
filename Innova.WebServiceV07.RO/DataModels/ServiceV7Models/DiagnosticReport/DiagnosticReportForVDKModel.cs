namespace Innova.WebServiceV07.RO.DataModels.ServiceV7Models.DiagnosticReport
{
    public class DiagnosticReportForVDKModel
    {
        public string externalSystemUserIdGuidString { get; set; }
        public string externalSystemUserFirstName { get; set; }
        public string externalSystemUserLastName { get; set; }
        public string externalSystemUserEmailAddress { get; set; }
        public string externalSystemUserPhoneNumber { get; set; }
        public string externalSystemUserRegion { get; set; }
        public string vin { get; set; }
        public int mileage { get; set; }
        public string transmission { get; set; }
        public int softwareTypeInt { get; set; }
        public int toolTypeFormatInt { get; set; }
        public string rawUpload { get; set; }
        public string pwrFixNotFoundFixPromisedByDateTimeUTCString { get; set; }
        public string obd1FixNotFoundFixPromisedByDateTimeUTCString { get; set; }
        public string absFixNotFoundFixPromisedByDateTimeUTCString { get; set; }
        public string srsFixNotFoundFixPromisedByDateTimeUTCString { get; set; }
    }
}