using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Repositories;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("v1/motoristas")]
    public class MotoristaController : Controller
    {

        private readonly MotoristaService _motoristaService;
        private readonly VeiculoService _veiculoService;


        public MotoristaController()
        {
            _motoristaService = new MotoristaService();
            _veiculoService = new VeiculoService();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MotoristaDto))]
        public ActionResult<IEnumerable<MotoristaDto>> Listar()
        {
            var motoristas = _motoristaService.Listar();

            return Ok(motoristas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MotoristaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<MotoristaDto> Obter(Guid id)
        {
            var response = _motoristaService.Obter(id);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdicionarMotoristaRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<MotoristaDto> Adicionar([FromBody] AdicionarMotoristaRequest request)
        {
            var response = _motoristaService.Adicionar(request);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MotoristaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<MotoristaDto> Alterar(Guid id, [FromBody] AdicionarMotoristaRequest request)
        {
            var response = _motoristaService.Alterar(id, request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}/sacar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MotoristaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<MotoristaDto> SacarSaldo(Guid id, [FromBody] ValorRequest request)
        {
            var response = _motoristaService.SacarSaldo(id, request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}/depositar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MotoristaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<MotoristaDto> DepositarSaldo(Guid id, [FromBody] ValorRequest request)
        {
            var response = _motoristaService.DepositarSaldo(id, request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MotoristaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<MotoristaDto> Remover(Guid id)
        {
            var response = _motoristaService.Remover(id);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }
    }
}
