using Microsoft.AspNetCore.Mvc;

namespace ColdrunLogistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]       
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
