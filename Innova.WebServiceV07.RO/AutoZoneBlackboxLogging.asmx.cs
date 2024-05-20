using Innova.WebServiceV07.RO.DataModels.AutoZoneBlackboxLoggingModels;
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
        [WebMethod(Description = "AutoZoneBlackboxLogging - Push transaction log to INNOVA")]
        public void PushTransactionLogToInnova(AutoZoneBlackboxLogWebServiceTransactionModel model)
        {
            try
            {
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