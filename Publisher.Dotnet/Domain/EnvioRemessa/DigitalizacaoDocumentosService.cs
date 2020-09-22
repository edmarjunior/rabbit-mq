using Common.Domain.Entities;
using Common.Domain.Infra.Queue;
using System.Linq;

namespace Domain.EnvioRemessa
{
    public class DigitalizacaoDocumentosService : IDigitalizacaoDocumentosService
    {
        private readonly IDigitalizacaoDocumentosRepository _digitalizacaoDocumentosRepository;
        private readonly IRabbitMqClient _rabbitMqClient;

        public DigitalizacaoDocumentosService(IDigitalizacaoDocumentosRepository digitalizacaoDocumentosRepository, IRabbitMqClient rabbitMqClient)
        {
            _digitalizacaoDocumentosRepository = digitalizacaoDocumentosRepository;
            _rabbitMqClient = rabbitMqClient;
        }

        public bool Post()
        {
            var documentos = _digitalizacaoDocumentosRepository.BuscaDocumentos().ToList();

            if (!documentos.Any())
                return false;


            _rabbitMqClient.Send(new QueueParams
            {
                Message = documentos,
                ExchangeName = "documentos_exchange",
                QueueName = "documentos_queue",
                ExchangeType = "direct",
                RoutingKey = ""
            });

            return true;
        }
    }
}
