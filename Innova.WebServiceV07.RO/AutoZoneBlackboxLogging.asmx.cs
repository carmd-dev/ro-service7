using Innova.WebServiceV07.RO.DataModels.AutoZoneBlackboxLoggingModels;
using Innova.WebServiceV07.RO.DataModels.ServiceV7Models.WebServiceKey;
using Innova.WebServiceV07.RO.DataObjects;
using Innova.WebServiceV07.RO.Services;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Web;
using System.Web.Services;

namespace Innova.WebServiceV07.RO
{
    /// <summary>
    /// Innova - AutoZone Logging Service. Contains the web methods used to log Innova diagnostic reports.
    /// </summary>
    [WebService(Namespace = "http://webservice.innova.com/Logging/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AutoZoneBlackboxLogging : WebServiceBase
    {
        [WebMethod(Description = "AutoZoneBlackboxLogging - Push payload to INNOVA")]
        public void PushPayloadToInnova(
            WebServiceKey key,
            string externalSystemUserIdString,
            string vin,
            string rawToolPayload,
            string reportID,
            int vehicleMileage,
            string createdDateTimeUTCString)
        {
            var logId = Guid.NewGuid().ToString();

            try
            {
                if (!this.ValidateKey(key))
                {
                    Logger.Write($"AutoZoneBlackboxLogging => PushPayloadToInnova => Invalid key => key: {key.Key}");
                    return;
                }

                Logger.Write($"AutoZoneBlackboxLogging => PushPayloadToInnova => logId: {logId} => data received => reportId: {reportID}, vin: {vin}, languageString: {key.LanguageString}");

                var model = new AutoZoneBlackboxDiagnosticReportLogModel
                {
                    webServiceKey = new WebServiceKeyModel
                    {
                        Key = key.Key,
                        LanguageString = key.LanguageString,
                        Region = key.Region,
                        Currency = key.Currency,
                        MarketString = key.MarketString
                    },
                    externalSystemUserIdString = externalSystemUserIdString,
                    vin = vin,
                    rawToolPayload = rawToolPayload,
                    reportId = reportID,
                    vehicleMileage = vehicleMileage,
                    createdDateTimeUTCString = createdDateTimeUTCString
                };

                if (Global.ForwardDataToIDMService)
                {
                    AutoZoneBlackboxLoggingService.PushPayloadToIDMService(model, logId);
                }

                AutoZoneBlackboxLoggingService.PushPayloadToQueue(model, logId);

                AutoZoneBlackboxLoggingService.SavePayloadToLocalFolder(model);
            }
            catch (Exception ex)
            {
                Logger.Write($"AutoZoneBlackboxLogging => PushPayloadToInnova => logId: {logId} => Exception: {ex}");
            }
        }

        [WebMethod(Description = "AutoZoneBlackboxLogging - Push transaction log to INNOVA")]
        public void PushTransactionLogToInnova(AutoZoneBlackboxTransactionLogModel model)
        {
            try
            {
                if (!this.ValidateKey(model.webServiceKey))
                {
                    Logger.Write($"AutoZoneBlackboxLogging => PushTransactionLogToInnova => Invalid key => key: {model.webServiceKey}");
                    return;
                }

                if (string.IsNullOrEmpty(model.ipAddress))
                {
                    model.ipAddress = HttpContext.Current.Request.UserHostAddress;
                }

                AutoZoneBlackboxLoggingService.PushTransactionLogToQueue(model);
            }
            catch (Exception ex)
            {
                Logger.Write($"AutoZoneBlackboxLogging => PushLogTransactionToInnova => Exception: {ex}");
            }
        }
    }
}