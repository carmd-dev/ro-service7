using Innova.WebServiceV07.RO.DataModels.AutoZoneBlackboxLoggingModels;
using Innova.WebServiceV07.RO.RabbitMQPublishers;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Threading.Tasks;

namespace Innova.WebServiceV07.RO.Services
{
    public class AutoZoneBlackboxLoggingService
    {
        public static void PushTransactionLogToQueue(AutoZoneBlackboxLogWebServiceTransactionModel model)
        {
            Task.Run(() =>
            {
                DoPushTransactionLogToQueue(model);
            });
        }

        private static void DoPushTransactionLogToQueue(AutoZoneBlackboxLogWebServiceTransactionModel model)
        {
            var rabbitMQHandler = new RabbitMQHandler();
            (bool isOk, Exception rabbitMQHandlerEx) = rabbitMQHandler.SendRequest(model, Global.RabbitMQ_QueueName_AutoZoneBlackboxTransactionLog);

            if (!isOk)
            {
                Logger.Write($"RO AutoZoneBlackboxLogging => Push Transaction Log To RabbitMQ Failed => queueName: {Global.RabbitMQ_QueueName_AutoZoneBlackboxTransactionLog} => Exception: {rabbitMQHandlerEx}");
            }
        }
    }
}