using Microsoft.AspNetCore.Mvc;

namespace Pau.Transport.Http.Controllers;

[Route("api/[controller]")]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
            
        return Ok("123");
    }
}
