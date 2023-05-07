using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("v1/corridas")]
    public class CorridaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
