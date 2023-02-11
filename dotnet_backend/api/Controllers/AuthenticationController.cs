using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [Route("callback")]
    [HttpGet]
    public void Callback()
    {
        Console.WriteLine("Callback called.");
    }
}
