using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly HttpContext? httpContext;

    public AuthenticationController(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContext = httpContextAccessor.HttpContext;
    }
        
        
    [Route("callback")]
    [HttpGet]
    public void Callback()
    {
        Console.WriteLine("Callback called.");
    }
    
    [Route("login")]
    [HttpGet]
    public void Login()
    {
        Console.WriteLine($"Login called. SessionId: {httpContext?.Session.Id}");
    }
}
