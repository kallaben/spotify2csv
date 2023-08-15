using System.Net;
using api.Services;

namespace api.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    private readonly List<string> _whitelistedPaths = new List<string>
    {
        "/Authentication/callback",
        "/Authentication/is-authenticated",
        "/Authentication/login",
    };

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        ISessionRepository sessionRepository)
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

        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
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