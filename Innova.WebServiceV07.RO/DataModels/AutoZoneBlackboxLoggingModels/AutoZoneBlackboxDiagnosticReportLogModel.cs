using Innova.WebServiceV07.RO.DataModels.ServiceV7Models.WebServiceKey;

namespace Innova.WebServiceV07.RO.DataModels.AutoZoneBlackboxLoggingModels
{
    public class AutoZoneBlackboxDiagnosticReportLogModel
    {
        public WebServiceKeyModel webServiceKey { get; set; }
        public string externalSystemUserIdString { get; set; }
        public string vin { get; set; }
        public string rawToolPayload { get; set; }
        public string reportId { get; set; }
        public int vehicleMileage { get; set; }
        public string createdDateTimeUTCString { get; set; }
    }
}