using Domain.EnvioRemessa;
using Domain.EnvioRemessa.Dto;
using System.Collections.Generic;

namespace Repository
{
    public class DigitalizacaoDocumentosRepository : IDigitalizacaoDocumentosRepository
    {
        public IEnumerable<DocumentoDto> BuscaDocumentos()
        {
            return GetMockDocumentos();
        }

        private static IEnumerable<DocumentoDto> GetMockDocumentos()
        {
            var documentos = new List<DocumentoDto>();

            for (var i = 1; i < 11; i++)
            {
                documentos.Add(new DocumentoDto { Id = i });
            }

            return documentos;
        }
    }
}
