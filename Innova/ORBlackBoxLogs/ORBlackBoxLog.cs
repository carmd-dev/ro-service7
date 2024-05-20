using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;

namespace Innova.ORBlackBoxLogs
{
    public class ORBlackBoxLog
    {
        public static void WriteORBlackBoxLogToDatabase(Registry registry, ORBlackBoxWebServiceTransaction model)
        {
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.CommandTimeout = 30;
                dr.ProcedureName = "WebServiceTransaction_Create";
                dr.AddGuid("ExternalSystemId", model.ExternalSystemId);
                dr.AddNVarChar("MethodInvoked", model.MethodInvoked);
                dr.AddNVarChar("MarketString", model.MarketString);
                dr.AddNVarChar("LanguageString", model.LanguageString);
                dr.AddInt32("Currency", model.Currency);
                dr.AddNVarChar("Region", model.Region);
                dr.AddNVarChar("IPAddress", model.IPAddress);
                dr.AddNVarChar("WebServiceKey", model.WebServiceKey);
                dr.AddBoolean("MixedLanguages", model.MixedLanguages);
                dr.AddInt32("LanguageType", model.LanguageType);
                dr.AddInt32("MethodType", model.MethodType);
                dr.AddDateTime("ReportDateTimeRequested", model.ReportDateTimeRequested);
                dr.AddNVarChar("StoreNumber", model.StoreNumber);
                dr.AddNVarChar("ReportId", model.ReportId);
                dr.AddNVarChar("VIN", model.VIN);
                dr.AddInt32("AcesBaseVehicleId", model.AcesBaseVehicleId);
                dr.AddInt32("AcesEngineBaseId", model.AcesEngineBaseId);
                dr.AddInt32("AcesSubModelId", model.AcesSubModelId);
                dr.AddInt32("VehicleMileage", model.VehicleMileage);
                dr.AddNVarChar("Payload", model.Payload);
                dr.AddBoolean("IsError", model.IsError);
                dr.AddInt32("Year", model.Year);
                dr.AddNVarChar("Make ", model.Make);
                dr.AddNVarChar("Model", model.Model);
                dr.AddNVarChar("EngineType", model.EngineType);
                dr.AddNVarChar("ErrorMessage", model.ErrorMessage);
                dr.Execute();
            }
        }
    }
}