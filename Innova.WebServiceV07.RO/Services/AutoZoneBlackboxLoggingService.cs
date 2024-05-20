using Innova.Web;
using Innova.WebServiceV07.RO.DataModels.AutoZoneBlackboxLoggingModels;
using Innova.WebServiceV07.RO.Helpers;
using Innova.WebServiceV07.RO.RabbitMQPublishers;
using Innova.WebServiceV07.RO.Responses;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Innova.WebServiceV07.RO.Services
{
    public class AutoZoneBlackboxLoggingService
    {
        public static void PushPayloadToIDMService(AutoZoneBlackboxDiagnosticReportLogModel model, string logId)
        {
            Task.Run(() =>
            {
                DoPushPayloadToIDMService(model, logId);
            });
        }

        private static void DoPushPayloadToIDMService(AutoZoneBlackboxDiagnosticReportLogModel model, string logId)
        {
            string errorMessage = string.Empty;

            var source = "AutoZone";

            try
            {
                var header = $"api-auth-token={Global.IDM_API_AUTH_TOKEN}";

                var bodyRequest = new Dictionary<string, string>
                {
                    { "vin", model.vin },
                    { "mileage", model.vehicleMileage.ToString() },
                    { "language", model.webServiceKey.LanguageString },
                    { "reportId", model.reportId },
                    { "storeNumber", string.Empty },
                    { "payload", model.rawToolPayload },
                    { "source", source }
                };

                var resonse = HttpHelper.Post(url: $"{Global.IDM_API_URL}/reports", contentValue: bodyRequest, connectionTimeout: Global.IDM_API_CONNECTION_TIMEOUT, headers: header);
                var responseContent = resonse.Content.ReadAsStringAsync().Result;

                if (resonse.IsSuccessStatusCode)
                {
                    var idmServiceResponse = JsonConvert.DeserializeObject<IDMServiceResponse>(responseContent);

                    if (idmServiceResponse.Message.Code == 0)
                    {
                        Logger.Write($"AutoZoneBlackboxLogging => Push Payload To IDM Service Successfully => logId: {logId}");
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
                    Logger.Write($"AutoZoneBlackboxLogging => Push Payload To IDM Service Failed => logId: {logId} => errorMessage: {errorMessage}");
                }
            }
        }

        public static void PushPayloadToQueue(AutoZoneBlackboxDiagnosticReportLogModel model, string logId)
        {
            Task.Run(() =>
            {
                DoPushPayloadToQueue(model, logId);
            });
        }

        private static void DoPushPayloadToQueue(AutoZoneBlackboxDiagnosticReportLogModel model, string logId)
        {
            var rabbitMQHandler = new RabbitMQHandler();
            (bool isOk, Exception rabbitMQHandlerEx) = rabbitMQHandler.SendRequest(model, Global.RabbitMQ_QueueName_AutoZoneBlackboxTransactionLog);

            if (isOk)
            {
                Logger.Write($"AutoZoneBlackboxLogging => Push Payload To RabbitMQ Successfully => logId: {logId}");
            }
            else
            {
                Logger.Write($"AutoZoneBlackboxLogging => Push Payload To RabbitMQ Failed => logId: {logId} => queueName: {Global.RabbitMQ_QueueName_AutoZoneBlackboxDiagnosticReportLog} => Exception: {rabbitMQHandlerEx}");

                var jsonPayloadModel = JsonConvert.SerializeObject(model);
                string faliedFileName = $"AutoZone_Payload_{DateTime.Now.ToString("yyyyMMdd HHmmss.fffffff")}_{Guid.NewGuid()}.txt";

                PayloadHelper.SavePayloadToFile("AutoZone", Global.ServiceReadOnly_NewPayloadFolder_When_SendRequest_ToRabbitMQ_Failed, faliedFileName, jsonPayloadModel);
            }
        }

        public static void SavePayloadToLocalFolder(AutoZoneBlackboxDiagnosticReportLogModel model)
        {
            Task.Run(() =>
            {
                DoSavePayloadToLocalFolder(model);
            });
        }

        private static void DoSavePayloadToLocalFolder(AutoZoneBlackboxDiagnosticReportLogModel model)
        {
            try
            {
                var jsonPayloadModel = JsonConvert.SerializeObject(model);
                string reportFilename = $"AutoZone_Payload_{DateTime.Now.ToString("yyyyMMdd HHmmss.fffffff")}_{Guid.NewGuid()}.txt";

                PayloadHelper.SavePayloadToFileByDay("AutoZone", Global.AutoZoneReportFromBBFolderPath, reportFilename, jsonPayloadModel);
            }
            catch (Exception ex)
            {
                Logger.Write($"AutoZoneBlackboxLogging => Save Payload To Local Folder Failed => Exception: {ex}");
            }
        }

        public static void PushTransactionLogToQueue(AutoZoneBlackboxTransactionLogModel model)
        {
            Task.Run(() =>
            {
                DoPushTransactionLogToQueue(model);
            });
        }

        private static void DoPushTransactionLogToQueue(AutoZoneBlackboxTransactionLogModel model)
        {
            var rabbitMQHandler = new RabbitMQHandler();
            (bool isOk, Exception rabbitMQHandlerEx) = rabbitMQHandler.SendRequest(model, Global.RabbitMQ_QueueName_AutoZoneBlackboxTransactionLog);

            if (!isOk)
            {
                Logger.Write($"AutoZoneBlackboxLogging => Push Transaction Log To RabbitMQ Failed => queueName: {Global.RabbitMQ_QueueName_AutoZoneBlackboxTransactionLog} => Exception: {rabbitMQHandlerEx}");
            }
        }
    }
}