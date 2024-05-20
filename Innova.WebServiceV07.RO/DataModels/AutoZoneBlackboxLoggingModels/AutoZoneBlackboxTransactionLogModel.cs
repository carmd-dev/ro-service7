using Innova.WebServiceV07.RO.DataObjects;

namespace Innova.WebServiceV07.RO.DataModels.AutoZoneBlackboxLoggingModels
{
    public class AutoZoneBlackboxTransactionLogModel
    {
        public WebServiceKey webServiceKey { get; set; }
        public string wsLanguageString { get; set; }
        public string wsRegion { get; set; }
        public string wsMarketString { get; set; }
        public string ipAddress { get; set; }
        public string method { get; set; }
        public bool mixedLanguages { get; set; }
        public int languageType { get; set; }
        public int methodType { get; set; }
        public string reportID { get; set; }
        public string vin { get; set; }
        public int vehicleMileage { get; set; }
        public string rawToolPayload { get; set; }
        public bool isError { get; set; }
        public int year { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string enginetype { get; set; }
        public string errorMessage { get; set; }
    }
}