using api.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace api.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISessionRepository sessionRepository)
    {
        await _next(httpContext);
        var sessionId = httpContext.Session.Id;
        if (SessionIdHasAlreadyAuthenticated(sessionRepository, sessionId))
        {
            await _next(httpContext);
            return;
        }

        httpContext.Session.SetString("CustomSessionID", new Guid().ToString());
        var spotifySsoBaseUrl = "https://accounts.spotify.com/authorize";
        var queryParameters = new Dictionary<string, string?>
        {
            {"response_type", "code"},
            {"client_id", "543a4066a8a94ff7ab4705453913eb4e"},
            {"scope", "playlist-read-private"},
            {"redirect_uri", "http://localhost:4200/redirect"},
            {"state", httpContext.Session.Id} 
        };
        
        var spotifySsoUrl = QueryHelpers.AddQueryString(spotifySsoBaseUrl, queryParameters);
        
        httpContext.Response.Redirect(spotifySsoUrl);
    }

    private bool SessionIdHasAlreadyAuthenticated(ISessionRepository sessionRepository, string sessionId)
    {
        var session = sessionRepository.getSessionOrNull(sessionId);
        
        return session?.HasValidAuthenticationToken() ?? false;
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
