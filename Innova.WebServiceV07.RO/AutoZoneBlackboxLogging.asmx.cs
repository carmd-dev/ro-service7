using Innova.ORBlackBoxLogs;
using Innova.WebServiceV07.RO.Common.Enums;
using Innova.WebServiceV07.RO.DataModels.RabbitMQModels;
using Innova.WebServiceV07.RO.DataModels.ServiceV6Models.LogDiagnosticReport;
using Innova.WebServiceV07.RO.DataModels.ServiceV7Models.WebServiceKey;
using Innova.WebServiceV07.RO.DataObjects;
using Innova.WebServiceV07.RO.Helpers;
using Innova.WebServiceV07.RO.WebMethods.DiagnosticReportLoggingV2WebMethods.AutoZone;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace Innova.WebServiceV07.RO
{
    /// <summary>
    /// Summary description for AutoZoneBlackboxLogging
    /// </summary>
    [WebService(Namespace = "http://webservice.innova.com/Logging/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoZoneBlackboxLogging : WebServiceBase
    {
        [WebMethod(Description = "Creates a diagnostic report using the provided VIN and payload V2.")]
        public DiagReportInfo CreateDiagnosticReportWithMileage(
            WebServiceKey key,
            string externalSystemUserIdString,
            string vin,
            string rawToolPayload,
            string reportID,
            int vehicleMileage,
            string createdDateTimeUTCString)
        {
            DiagReportInfo drInfo = new DiagReportInfo();
            WebServiceSessionStatus errors = new WebServiceSessionStatus();
            drInfo.WebServiceSessionStatus = errors;

            var logId = Guid.NewGuid().ToString();

            try
            {
                if (!this.ValidateKey(key))
                {
                    Logger.Write($"RO AutoZoneDiagnosticReportLoggingV2 => AutoZoneCreateDiagnosticReportWithMileage => Invalid key => key: {key.Key}");
                    errors.AddValidationFailure("00001", "Invalid key");

                    return drInfo;
                }

                if (!IsUserIdValid(externalSystemUserIdString))
                {
                    Logger.Write($"RO AutoZoneDiagnosticReportLoggingV2 => AutoZoneCreateDiagnosticReportWithMileage => ExternalSystemUserIdString format is not valid => externalSystemUserIdString: {externalSystemUserIdString}");
                    errors.AddValidationFailure("40004", "ExternalSystemUserIdString format is not valid");

                    return drInfo;
                }

                Logger.Write($"RO AutoZoneDiagnosticReportLoggingV2 => AutoZoneCreateDiagnosticReportWithMileage => logId: {logId} => reportId: {reportID}, vin: {vin}");

                if (Global.ForwardDataToIDMService)
                {
                    AutoZoneDiagnosticReportLoggingV2Processor.PushPayloadToIDMService(vin, vehicleMileage.ToString(), reportID, rawToolPayload, key.LanguageString, logId);
                }

                #region Gets the payload bytes' length and set serviceName

                var serviceName = ServiceTypeEnum.DiagnosticReportLoggingServiceV6.ToString();

                // Gets the payload bytes' length
                var arr = Convert.FromBase64String(rawToolPayload);

                if (arr.Length >= 10597) // Posts the payload to DiagnosticReportLoggingV2
                {
                    serviceName = ServiceTypeEnum.DiagnosticReportLoggingServiceV7.ToString();
                }

                #endregion

                #region Service ReadOnly - Send request to RabbitMQ

                var rabbitMQRequestModel = new RabbitMQRequestModel<LogDiagnosticReportModel>
                {
                    ServiceName = serviceName,
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
                        serviceName,
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
                    SendRequestToRabbitMQ(rabbitMQRequestModel, logId, Global.RabbitMQ_QueueName_AutoZoneDiagnosticReportLogging);
                });

                #endregion Service ReadOnly - Send request to RabbitMQ

                #region Save to local file by day

                Task.Run(() =>
                {
                    SaveToLocalFolder(rabbitMQRequestModel, Global.AutoZoneReportFromBBFolderPath);
                });

                #endregion Save to local file by day
            }
            catch (Exception ex)
            {
                Logger.Write($"RO AutoZoneDiagnosticReportLoggingV2 => AutoZoneCreateDiagnosticReportWithMileage => logId: {logId} => Exception: {ex}");
                errors.AddValidationFailure("90000", $"Exception: {ex}");
            }

            return drInfo;
        }

        [WebMethod(Description = "ORBlackBoxLog - Write log to database")]
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
                    SendRequestToRabbitMQ(rabbitMQRequestModel, logId, Global.RabbitMQ_QueueName_AutoZoneBlackBoxLog);
                });
            }
            catch (Exception ex)
            {
                Logger.Write($"RO OreillyDiagnosticReportLoggingV2 => WriteORBlackBoxLogTransaction => logId: {logId} => Exception: {ex}");
            }

            #endregion Service ReadOnly - Send request to RabbitMQ
        }

    }
}
