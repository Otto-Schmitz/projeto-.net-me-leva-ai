using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Repositories;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("v1/passageiros")]
    public class PassageiroController : Controller
    {
        private readonly PassageiroRepository _passageiroRepository;
        private readonly PassageiroService _passageiroService;

        public PassageiroController()
        {
            _passageiroRepository = new PassageiroRepository();
            _passageiroService = new PassageiroService();
        }

        //[HttpGet]
        //public 

        [HttpPost]
        public ActionResult<PassageiroDto> Cadastrar([FromBody] CadastrarPassageiroRequest request)
        {
            var response = _passageiroService.Cadastrar(request);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Created("Created", response);
        }

        // o request por algum motivo nao esta "chegando" por parametro
    }
}
