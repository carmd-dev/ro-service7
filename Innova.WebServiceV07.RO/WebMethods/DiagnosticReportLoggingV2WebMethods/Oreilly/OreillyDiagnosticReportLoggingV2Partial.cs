using Innova.ORBlackBoxLogs;
using Innova.WebServiceV07.RO.Common.Enums;
using Innova.WebServiceV07.RO.DataModels.RabbitMQModels;
using Innova.WebServiceV07.RO.DataModels.ServiceV6Models.LogDiagnosticReport;
using Innova.WebServiceV07.RO.DataModels.ServiceV7Models.WebServiceKey;
using Innova.WebServiceV07.RO.DataObjects;
using Innova.WebServiceV07.RO.DiagnosticReportLoggingV2WebMethods.Oreilly;
using Innova.WebServiceV07.RO.Helpers;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace Innova.WebServiceV07.RO
{
    public partial class DiagnosticReportLoggingV2
    {
        #region Oreilly

        /// <summary>
        /// Creates a diagnostic report using the provided VIN and payload V2.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="externalSystemUserIdString"></param>
        /// <param name="vin"></param>
        /// <param name="rawToolPayload"></param>
        /// <param name="reportID"></param>
        /// <param name="vehicleMileage"></param>
        /// <param name="createdDateTimeUTCString"></param>
        /// <returns></returns>
        [WebMethod(Description = "Creates a diagnostic report using the provided VIN and payload V2.")]
        public DiagReportInfo CreateDiagnosticReportWithMileage(WebServiceKey key, string externalSystemUserIdString, string vin, string rawToolPayload, string reportID, int vehicleMileage, string createdDateTimeUTCString)
        {
            DiagReportInfo drInfo = new DiagReportInfo();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            drInfo.WebServiceSessionStatus = errors;

            var logId = Guid.NewGuid().ToString();

            try
            {
                if (!this.ValidateKey(key))
                {
                    Logger.Write($"RO OreillyDiagnosticReportLoggingV2 => CreateDiagnosticReportWithMileage => Invalid key => key: {key.Key}");
                    errors.AddValidationFailure("00001", "Invalid key");

                    return drInfo;
                }

                if (!IsUserIdValid(externalSystemUserIdString))
                {
                    Logger.Write($"RO OreillyDiagnosticReportLoggingV2 => CreateDiagnosticReportWithMileage => ExternalSystemUserIdString format is not valid => externalSystemUserIdString: {externalSystemUserIdString}");
                    errors.AddValidationFailure("40004", "ExternalSystemUserIdString format is not valid");

                    return drInfo;
                }

                Logger.Write($"RO OreillyDiagnosticReportLoggingV2 => CreateDiagnosticReportWithMileage => logId: {logId} => reportId: {reportID}, vin: {vin}");

                if (Global.ForwardDataToIDMService)
                {
                    OreillyDiagnosticReportLoggingV2Processor.PushPayloadToIDMService(vin, vehicleMileage.ToString(), reportID, rawToolPayload, key.LanguageString, logId);
                }

                #region Service ReadOnly - Send request to RabbitMQ

                var rabbitMQRequestModel = new RabbitMQRequestModel<LogDiagnosticReportModel>
                {
                    ServiceName = ServiceTypeEnum.DiagnosticReportLoggingServiceV7.ToString(),
                    MethodName = MethodDiagnosticReportLoggingServiceEnum.LogDiagnosticReportWithMileage.ToString(),
                    ExternalSystemName = GetExternalSystemName(),
                    WebServiceKey = new WebServiceKeyModel
                    {
                        Key = key.Key,
                        LanguageString = key.LanguageString,
                        Region = key.Region,
                        Currency = key.Currency,
                        MarketString = key.MarketString
                    },
                    PayloadInfo = PayloadHelper.BuildPayloadInfo
                   (
                       ServiceTypeEnum.DiagnosticReportLoggingServiceV7.ToString(),
                       MethodDiagnosticReportLoggingServiceEnum.LogDiagnosticReportWithMileage.ToString(),
                       reportID,
                       externalSystemUserIdString,
                       vin,
                       vehicleMileage.ToString(),
                       rawToolPayload
                   ),
                    Data = new LogDiagnosticReportModel
                    {
                        externalSystemUserIdString = externalSystemUserIdString,
                        vin = vin,
                        rawToolPayload = rawToolPayload,
                        reportID = reportID,
                        vehicleMileage = vehicleMileage,
                        createdDateTimeUTCString = createdDateTimeUTCString
                    },
                };

                Task.Run(() =>
                {
                    SendRequestToRabbitMQ(rabbitMQRequestModel, logId, Global.RabbitMQ_QueueName_DiagnosticReportLogging);
                });

                #endregion Service ReadOnly - Send request to RabbitMQ

                #region Save to local file by day

                Task.Run(() =>
                {
                    SaveToLocalFolder(rabbitMQRequestModel);
                });

                #endregion Save to local file by day
            }
            catch (Exception ex)
            {
                Logger.Write($"RO OreillyDiagnosticReportLoggingV2 => CreateDiagnosticReportWithMileage => logId: {logId} => Exception: {ex}");
                errors.AddValidationFailure("90000", $"Exception: {ex}");
            }

            return drInfo;
        }

        [WebMethod(Description = "ORBlackBoxLog - write log to database")]
        public void WriteORBlackBoxLogTransaction(ORBlackBoxWebServiceTransaction dataLog)
        {
            #region Service ReadOnly - Send request to RabbitMQ

            var logId = Guid.NewGuid().ToString();

            try
            {
                if (string.IsNullOrEmpty(dataLog.IPAddress))
                {
                    dataLog.IPAddress = HttpContext.Current.Request.UserHostAddress;
                }

                var rabbitMQRequestModel = new RabbitMQRequestModel<ORBlackBoxWebServiceTransaction>
                {
                    ServiceName = ServiceTypeEnum.DiagnosticReportLoggingServiceV7.ToString(),
                    MethodName = MethodDiagnosticReportLoggingServiceEnum.WriteORBlackBoxLogTransaction.ToString(),
                    Data = dataLog
                };

                Task.Run(() =>
                {
                    SendRequestToRabbitMQ(rabbitMQRequestModel, logId, Global.RabbitMQ_QueueName_OreillyBlackBoxLog);
                });
            }
            catch (Exception ex)
            {
                Logger.Write($"RO OreillyDiagnosticReportLoggingV2 => WriteORBlackBoxLogTransaction => logId: {logId} => Exception: {ex}");
            }

            #endregion Service ReadOnly - Send request to RabbitMQ
        }

        #endregion Oreilly
    }
}