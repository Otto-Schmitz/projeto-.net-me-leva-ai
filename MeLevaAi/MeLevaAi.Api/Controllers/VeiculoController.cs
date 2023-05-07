using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Repositories;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("v1/veiculos")]
    public class VeiculoController : Controller
    {

        private readonly VeiculoService _veiculoService;


        public VeiculoController()
        {
            _veiculoService = new VeiculoService();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VeiculoDto))]
        public ActionResult<IEnumerable<VeiculoDto>> Listar()
        {
            var veiculos = _veiculoService.Listar();

            return Ok(veiculos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VeiculoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<VeiculoDto> Obter(Guid id)
        {
            var response = _veiculoService.Obter(id);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdicionarVeiculoRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<VeiculoDto> Adicionar([FromBody] AdicionarVeiculoRequest request)
        {
            var response = _veiculoService.Adicionar(request);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VeiculoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<VeiculoDto> Alterar(Guid id, [FromBody] AdicionarVeiculoRequest request)
        {
            var response = _veiculoService.Alterar(id, request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VeiculoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<VeiculoDto> Remover(Guid id)
        {
            var response = _veiculoService.Remover(id);

            if (!response.IsValid())
                return NotFound(new ErrorResponse(response.Notifications));

            return Ok(response);
        }
    }
}
