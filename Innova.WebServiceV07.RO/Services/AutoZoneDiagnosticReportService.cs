using Innova.WebServiceV07.RO.DataModels.RabbitMQModels;
using Innova.WebServiceV07.RO.DataModels.ServiceV6Models.LogDiagnosticReport;
using Innova.WebServiceV07.RO.RabbitMQPublishers;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
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
            }
        }
    }
}