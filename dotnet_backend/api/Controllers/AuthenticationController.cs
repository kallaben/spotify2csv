using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly SpotifyAuthorizationService _spotifyAuthorizationService;
    private readonly HttpContext? _httpContext;

    public AuthenticationController(IHttpContextAccessor httpContextAccessor, SpotifyAuthorizationService spotifyAuthorizationService)
    {
        _spotifyAuthorizationService = spotifyAuthorizationService;
        _httpContext = httpContextAccessor.HttpContext;
    }
        
        
    [Route("callback")]
    [HttpGet]
    public async Task Callback(string code, string state)
    {
        await _spotifyAuthorizationService.Authenticate(code, state);
        _httpContext.Response.Redirect("http://localhost:4200/");
    }
    
    [Route("login")]
    [HttpGet]
    public void Login()
    {
        Console.WriteLine($"Login called. SessionId: {_httpContext?.Session.Id}");
    }
}
