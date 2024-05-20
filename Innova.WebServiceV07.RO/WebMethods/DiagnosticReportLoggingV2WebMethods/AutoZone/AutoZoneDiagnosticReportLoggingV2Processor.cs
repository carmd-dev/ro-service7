using Innova.Web;
using Innova.WebServiceV07.RO.Responses;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Innova.WebServiceV07.RO.WebMethods.DiagnosticReportLoggingV2WebMethods.AutoZone
{
    public class AutoZoneDiagnosticReportLoggingV2Processor
    {
        public static void PushPayloadToIDMService(string vin, string mileage, string reportId, string payload, string language, string logId)
        {
            Task.Run(() =>
            {
                DoPushPayloadToIDMService(vin, mileage, reportId, payload, language, logId);
            });
        }

        private static void DoPushPayloadToIDMService(string vin, string mileage, string reportId, string payload, string language, string logId)
        {
            string errorMessage = string.Empty;

            var source = "AutoZone";

            try
            {
                var header = $"api-auth-token={Global.IDM_API_AUTH_TOKEN}";

                var bodyRequest = new Dictionary<string, string>
                {
                    { "vin", vin },
                    { "mileage", mileage.ToString() },
                    { "language", language },
                    { "reportId", reportId },
                    { "storeNumber", string.Empty },
                    { "payload", payload },
                    { "source", source }
                };

                var resonse = HttpHelper.Post(url: $"{Global.IDM_API_URL}/reports", contentValue: bodyRequest, connectionTimeout: Global.IDM_API_CONNECTION_TIMEOUT, headers: header);
                var responseContent = resonse.Content.ReadAsStringAsync().Result;

                if (resonse.IsSuccessStatusCode)
                {
                    var idmServiceResponse = JsonConvert.DeserializeObject<IDMServiceResponse>(responseContent);

                    if (idmServiceResponse.Message.Code == 0)
                    {
                        Logger.Write($"AutoZoneDiagnosticReportLoggingV2 => DoPushPayloadToIDMService successfully => logId: {logId} => reportId: {reportId}, vin: {vin}");
                        return;
                    }
                    errorMessage = $"resonse.StatusCode: {resonse.StatusCode} - idmServiceResponse => Description: {idmServiceResponse.Message.Description}";
                }
                else
                {
                    errorMessage = $"resonse.StatusCode: {resonse.StatusCode} - resonse.Content: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Exception: {ex}";
            }
            finally
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Logger.Write($"AutoZoneDiagnosticReportLoggingV2 => DoPushPayloadToIDMService failed => logId: {logId} => source: {source} => vin: {vin} | mileage: {mileage} | reportId: {reportId} | language: {language} => errorMessage: {errorMessage}");
                }
            }
        }
    }
}