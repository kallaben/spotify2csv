using api.Models;
using api.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace api.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    private readonly List<string> _whitelistedPaths = new List<string>
    {
        "/Authentication/callback", 
        "/Authentication/is-authenticated"
    };

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISessionRepository sessionRepository)
    {
        if (_whitelistedPaths.Contains(httpContext.Request.Path))
        {
            await _next(httpContext);
            return;
        }
        
        var sessionId = httpContext.Session.Id;
        var session = await sessionRepository.GetSessionOrNull(sessionId);

        if (session?.HasValidAuthenticationToken() ?? false)
        {
            await _next(httpContext);
            return;
        }
        
        httpContext.Session.SetString("CustomSessionID", new Guid().ToString());

        if (session == null)
        {
            var newSession = new Session
            {
                SessionId = sessionId
            };
            
            await sessionRepository.InsertSession(newSession);
        }

        var spotifySsoBaseUrl = "https://accounts.spotify.com/authorize";
        var queryParameters = new Dictionary<string, string?>
        {
            {"response_type", "code"},
            {"client_id", "543a4066a8a94ff7ab4705453913eb4e"},
            {"scope", "playlist-read-private"},
            {"redirect_uri", "http://localhost:4200/redirect"},
            {"state", $"{sessionId}:{httpContext.Request.Path}"} 
        };
        
        var spotifySsoUrl = QueryHelpers.AddQueryString(spotifySsoBaseUrl, queryParameters);
        
        httpContext.Response.Redirect(spotifySsoUrl);
    }
}

public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseSpotifyAuthentication(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}
