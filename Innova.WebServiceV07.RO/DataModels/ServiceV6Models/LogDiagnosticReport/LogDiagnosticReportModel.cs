namespace Innova.WebServiceV07.RO.DataModels.ServiceV6Models.LogDiagnosticReport
{
    public class LogDiagnosticReportModel
    {
        public string externalSystemUserIdString { get; set; }
        public string vin { get; set; }
        public string rawToolPayload { get; set; }
        public string reportID { get; set; }
        public int vehicleMileage { get; set; }
        public string createdDateTimeUTCString { get; set; }
    }
}