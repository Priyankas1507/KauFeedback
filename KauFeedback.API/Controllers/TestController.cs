using Microsoft.AspNetCore.Mvc;

namespace KauFeedback.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("KauFeedback API Working");
    }
}