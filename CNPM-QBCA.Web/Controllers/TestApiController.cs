using Microsoft.AspNetCore.Mvc;

namespace QBCA.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestApiController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok("Hello from API!");
        }
    }
}