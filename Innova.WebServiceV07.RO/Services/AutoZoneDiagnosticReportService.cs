using Innova.WebServiceV07.RO.DataModels.RabbitMQModels;
using Innova.WebServiceV07.RO.DataModels.ServiceV6Models.LogDiagnosticReport;
using Innova.WebServiceV07.RO.Helpers;
using Innova.WebServiceV07.RO.RabbitMQPublishers;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Innova.WebServiceV07.RO.Services
{
    public class AutoZoneDiagnosticReportService
    {
        public static void PushDiagnosticReportToQueue(RabbitMQRequestModel<LogDiagnosticReportModel> model)
        {
            Task.Run(() =>
            {
                DoPushDiagnosticReportToQueue(model);
            });
        }

        private static void DoPushDiagnosticReportToQueue(RabbitMQRequestModel<LogDiagnosticReportModel> model)
        {
            var rabbitMQHandler = new RabbitMQHandler();
            (bool isOk, Exception rabbitMQHandlerEx) = rabbitMQHandler.SendRequest(model, Global.RabbitMQ_QueueName_AutoZoneDiagnosticReportLogging);

            if (!isOk)
            {
                Logger.Write($"RO AutoZoneBlackboxLogging => Push Payload Log To RabbitMQ Failed => queueName: {Global.RabbitMQ_QueueName_AutoZoneDiagnosticReportLogging} => Exception: {rabbitMQHandlerEx}");

                #region Save payload to file

                var payloadInfo = model.PayloadInfo;

                if (payloadInfo.Any())
                {
                    string externalSystemName = model.ExternalSystemName;

                    string faliedFileName = $"{externalSystemName}_Payload_" + DateTime.Now.ToString("yyyyMMdd HHmmss.fffffff") + "_" + Guid.NewGuid() + ".txt";

                    Logger.Write($"Send Request To RabbitMQ Failed => SavePayloadToFile => externalSystemName: {externalSystemName} => faliedFileName: {faliedFileName}");

                    PayloadHelper.SavePayloadToFile(externalSystemName, Global.ServiceReadOnly_NewPayloadFolder_When_SendRequest_ToRabbitMQ_Failed, faliedFileName, payloadInfo);
                }

                #endregion Save payload to file
            }
        }
    }
}