using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("v1/passageiros")]
    public class PassageiroController : Controller
    {
        private readonly PassageiroService _passageiroService;

        public PassageiroController()
        {
            _passageiroService = new PassageiroService();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassageiroDto))]
        public ActionResult<IEnumerable<PassageiroDto>> Listar()
        {
            var passageiros = _passageiroService.Listar();

            return Ok(passageiros);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassageiroDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<PassageiroDto> Obter(Guid id)
        {
            var response = _passageiroService.Obter(id);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassageiroDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<PassageiroDto> Adicionar([FromBody] AdicionarPassageiroRequest request)
        {
            var response = _passageiroService.Adicionar(request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Created("Created", response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassageiroDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<PassageiroDto> Alterar(Guid id, [FromBody] AdicionarPassageiroRequest request)
        {
            var response = _passageiroService.Alterar(id, request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}/sacar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassageiroDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<PassageiroDto> SacarSaldo(Guid id, [FromBody] ValorRequest request)
        {
            var response = _passageiroService.SacarSaldo(id, request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}/depositar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassageiroDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<PassageiroDto> DepositarSaldo(Guid id, [FromBody] ValorRequest request)
        {
            var response = _passageiroService.DepositarSaldo(id, request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

    }
}
