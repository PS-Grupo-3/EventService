using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventCategoryController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok(new { Message = "Category endpoint is working!" });
        }
    }
}
