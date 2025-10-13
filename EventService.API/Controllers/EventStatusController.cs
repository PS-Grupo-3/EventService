using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventStatusController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok(new { Message = "Status endpoint is working!" });
        }
    }
}
