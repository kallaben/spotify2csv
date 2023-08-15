using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly SpotifyAuthorizationService _spotifyAuthorizationService;
    private readonly HttpContext? _httpContext;

    public AuthenticationController(
        IHttpContextAccessor httpContextAccessor,
        SpotifyAuthorizationService spotifyAuthorizationService)
    {
        _spotifyAuthorizationService = spotifyAuthorizationService;
        _httpContext = httpContextAccessor.HttpContext;
    }


    [Route("callback")]
    [HttpGet]
    public async Task Callback(string code, string state)
    {
        var redirectPath =
            await _spotifyAuthorizationService.Authenticate(code, state);
        _httpContext.Response.Redirect($"http://localhost:4200{redirectPath}");
    }

    [Route("login")]
    [HttpGet]
    public async Task Login()
    {
        var redirectPath =
            await _spotifyAuthorizationService.Login();
        _httpContext.Response.Redirect(redirectPath);
    }

    [Route("is-authenticated")]
    [HttpGet]
    public async Task<bool> IsAuthenticated()
    {
        return await _spotifyAuthorizationService.IsAuthenticated();
    }
}