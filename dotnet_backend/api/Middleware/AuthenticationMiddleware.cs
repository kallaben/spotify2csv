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
        var sessionId = httpContext.Session.Id;
        if (SessionIdHasAlreadyAuthenticated(sessionRepository, sessionId))
        {
            await _next(httpContext);
            return;
        }


        var spotifySsoBaseUrl = "https://accounts.spotify.com/authorize";
        var queryParameters = new Dictionary<string, string?>
        {
            {"response_type", "code"},
            {"client_id", "543a4066a8a94ff7ab4705453913eb4e"},
            {"scope", "playlist-read-private"},
            {"redirect_uri", "http://localhost:4200/api/callback"},
            {"state", "sessionId"} 
        };
        
        var spotifySsoUrl = QueryHelpers.AddQueryString(spotifySsoBaseUrl, queryParameters);
        
        httpContext.Response.Redirect(spotifySsoUrl);
    }

    private bool SessionIdHasAlreadyAuthenticated(ISessionRepository sessionRepository, string sessionId)
    {
        var session = sessionRepository.getSessionOrNull(sessionId);
        
        return session?.hasvalidauthenticationtoken() ?? false;
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
