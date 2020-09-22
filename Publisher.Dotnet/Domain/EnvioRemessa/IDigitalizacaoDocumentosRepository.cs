using Domain.EnvioRemessa.Dto;
using System.Collections.Generic;

namespace Domain.EnvioRemessa
{
    public interface IDigitalizacaoDocumentosRepository
    {
        IEnumerable<DocumentoDto> BuscaDocumentos();
    }
}
