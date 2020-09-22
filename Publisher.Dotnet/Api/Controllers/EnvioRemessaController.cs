using Domain.EnvioRemessa;
using System.Net;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/digitalizacao-documentos")]
    public class EnvioRemessaController : ApiController
    {
        private readonly IDigitalizacaoDocumentosService _digitalizacaoDocumentosService;

        public EnvioRemessaController(IDigitalizacaoDocumentosService digitalizacaoDocumentosService)
        {
            _digitalizacaoDocumentosService = digitalizacaoDocumentosService;
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post()
        {
            var sucesso = _digitalizacaoDocumentosService.Post();

            if (!sucesso)
                return Content(HttpStatusCode.BadRequest, "Nenhum documento para ser digitalizado");

            return Ok();
        }
    }
}
