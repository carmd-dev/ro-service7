using System;

namespace Innova.ORBlackBoxLogs
{
    public class ORBlackBoxWebServiceTransaction
    {
        public Guid? ExternalSystemId { get; set; }
        public string MethodInvoked { get; set; }
        public string MarketString { get; set; }
        public string LanguageString { get; set; }
        public int? Currency { get; set; }
        public string Region { get; set; }
        public string IPAddress { get; set; }
        public string WebServiceKey { get; set; }
        public bool? MixedLanguages { get; set; }
        public int? LanguageType { get; set; }
        public int? MethodType { get; set; }
        public DateTime? ReportDateTimeRequested { get; set; }
        public string StoreNumber { get; set; }
        public string ReportId { get; set; }
        public string VIN { get; set; }
        public int? AcesBaseVehicleId { get; set; }
        public int? AcesEngineBaseId { get; set; }
        public int? AcesSubModelId { get; set; }
        public int? VehicleMileage { get; set; }
        public string Payload { get; set; }
        public bool? IsError { get; set; }
        public int? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string EngineType { get; set; }
        public string ErrorMessage { get; set; }
    }
}