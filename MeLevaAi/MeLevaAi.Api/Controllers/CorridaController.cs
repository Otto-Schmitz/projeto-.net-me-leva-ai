using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("v1/corridas")]
    public class CorridaController : Controller
    {
        private readonly MotoristaService _motoristaService;
        private readonly VeiculoService _veiculoService;
        private readonly CorridaService _corridaService;
        public CorridaController()
        {
            _motoristaService = new MotoristaService();
            _veiculoService = new VeiculoService();
            _corridaService = new CorridaService();
        }

        [HttpPost("chamar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChamarCorridaRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<ChamarCorridaDto> Chamar([FromBody] ChamarCorridaRequest request)
        {
            var response = _corridaService.Chamar(request);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}/iniciar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChamarCorridaRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<IniciarCorridaDto> Iniciar(Guid id)
        {
            var response = _corridaService.Iniciar(id);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}/finalizar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinalizarCorridaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<FinalizarCorridaDto> Finalizar(Guid id)
        {
            var response = _corridaService.Finalizar(id);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }
      
        [HttpPut("{id}/avaliar/passageiro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AvaliarPessoaRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<AvaliarPassageiroDto> AvaliarPassageiro(Guid id, [FromBody] AvaliarPessoaRequest request)
        {
            var response = _corridaService.AvaliarPassageiro(id, request);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}/avaliar/motorista")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AvaliarPessoaRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<AvaliarMotoristaDto> AvaliarMotorista(Guid id, [FromBody] AvaliarPessoaRequest request)
        {
            var response = _corridaService.AvaliarMotorista(id, request);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }
    }


}
