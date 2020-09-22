namespace Common.Domain.Entities
{
    public class QueueParams
    {
        public object Message { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string ExchangeType { get; set; }
    }
}
