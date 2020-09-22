using Common.Domain.Entities;
using Common.Domain.Infra.Queue;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Common.Infra.Queue
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly Parameters _parameters;

        public RabbitMqClient(Parameters parameters)
        {
            _parameters = parameters;
        }

        public void Send(QueueParams queueParams)
        {
            // montando a conexão
            var factory = new ConnectionFactory
            {
                UserName = _parameters.User,
                Password = _parameters.Password,
                VirtualHost = "/",
                HostName = _parameters.Host,
                Port = _parameters.Port
            };

            using (var conn = factory.CreateConnection()) // conectando com no rabbitMQ
            {
                using (var channel = conn.CreateModel()) // criando um canal de conexão
                {
                    // criando a fila
                    CreateQueue(channel, queueParams.QueueName);

                    // criando o exchange
                    CreateExchange(channel, queueParams.ExchangeName, queueParams.ExchangeType);

                    // vinculando a fila com a exchange
                    channel.QueueBind(queueParams.QueueName, queueParams.ExchangeName, queueParams.RoutingKey, null);

                    // transformando a mensagem em bytes
                    var messageBodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(queueParams.Message));

                    // enviando mensagem
                    channel.BasicPublish(queueParams.ExchangeName, queueParams.RoutingKey, null, messageBodyBytes);
                }
            }
        }

        private static void CreateQueue(IModel channel, string queueName)
        {
            // criando a fila
            channel.QueueDeclare(queue: queueName,
                                durable: true,
                                autoDelete: false,
                                exclusive: false,
                                arguments: null);
        }

        private static void CreateExchange(IModel channel, string exchangeName, string exchangeType)
        {
            // criando o exchange
            channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
        }
    }
}
