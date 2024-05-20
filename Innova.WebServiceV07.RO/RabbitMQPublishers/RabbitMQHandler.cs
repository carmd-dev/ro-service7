using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Text;

namespace Innova.WebServiceV07.RO.RabbitMQPublishers
{
    public interface IRabbitMQHandler
    {
        (bool isOk, Exception exception) SendRequest(object dataRequest, string queueName = "");
    }

    public class RabbitMQHandler : IRabbitMQHandler
    {
        private readonly string _RabbitMQ_HostName = ConfigurationManager.AppSettings.Get("RabbitMQ_HostName");
        private readonly int _RabbitMQ_Port = int.Parse(ConfigurationManager.AppSettings["RabbitMQ_Port"]);
        private readonly string _RabbitMQ_UserName = ConfigurationManager.AppSettings["RabbitMQ_UserName"];
        private readonly string _RabbitMQ_Password = ConfigurationManager.AppSettings["RabbitMQ_Password"];
        private readonly string _RabbitMQ_QueueName_Default = ConfigurationManager.AppSettings["RabbitMQ_QueueName_Default"];

        public (bool isOk, Exception exception) SendRequest(object dataRequest, string queueName = "")
        {
            bool isOk;
            Exception exception = null;

            try
            {
                var queue = string.Empty;

                if (!string.IsNullOrEmpty(queueName))
                {
                    queue = queueName;
                }
                else
                {
                    queue = _RabbitMQ_QueueName_Default;
                }

                // Set up the RabbitMQ connection and channel
                var connectionFactory = new ConnectionFactory
                {
                    HostName = _RabbitMQ_HostName,
                    Port = _RabbitMQ_Port,
                    UserName = _RabbitMQ_UserName,
                    Password = _RabbitMQ_Password,
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(30)
                };

                //Create the RabbitMQ connection using connection factory details as i mentioned above
                using (var connection = connectionFactory.CreateConnection())
                //Here we create channel with session and model
                using (var channel = connection.CreateModel())
                {
                    //declare the queue after mentioning name and a few property related to that
                    channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    // Publishing a durable message
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true; // Make the message durable

                    //Serialize the message
                    var json = JsonConvert.SerializeObject(dataRequest);
                    var body = Encoding.UTF8.GetBytes(json);

                    //put the data on to the queue
                    channel.BasicPublish(exchange: string.Empty, routingKey: queue, basicProperties: properties, body: body);
                }

                isOk = true;
            }
            catch (Exception ex)
            {
                isOk = false;
                exception = ex;
            }

            return (isOk, exception);
        }
    }
}