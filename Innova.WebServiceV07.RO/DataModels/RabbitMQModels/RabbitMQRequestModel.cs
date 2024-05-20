using Innova.WebServiceV07.RO.DataModels.ServiceV7Models.WebServiceKey;
using System.Collections.Generic;

namespace Innova.WebServiceV07.RO.DataModels.RabbitMQModels
{
    public class RabbitMQRequestModel<T> : BaseRabbitMQRequestModel
    {
        public T Data { get; set; }

        public string NewDiagnosticReportId { get; set; }
    }

    public class BaseRabbitMQRequestModel
    {
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public string ExternalSystemName { get; set; }
        public WebServiceKeyModel WebServiceKey { get; set; }
        public IEnumerable<string> PayloadInfo { get; set; } = new List<string>();
    }
}