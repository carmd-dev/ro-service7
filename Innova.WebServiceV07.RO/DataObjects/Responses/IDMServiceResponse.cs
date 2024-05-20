namespace Innova.WebServiceV07.RO.Responses
{
    public class Message
    {
        public int Code { get; set; }

        public string Description { get; set; }
    }

    public class IDMServiceResponse
    {
        public Message Message { get; set; }
    }
}