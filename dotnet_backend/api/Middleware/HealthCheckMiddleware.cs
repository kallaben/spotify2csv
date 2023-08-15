using System.Net;

namespace api.Middleware;

public class HealthCheckMiddleware
{
    private readonly RequestDelegate _next;

    public HealthCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        ILogger<HealthCheckMiddleware> logger)
    {
        if (httpContext.Request.Path == "/Health")
        {
            logger.LogInformation("Health check called.");
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            return;
        }

        await _next(httpContext);
    }
}

public static class HealthCheckMiddlewareExtensions
{
    public static IApplicationBuilder UseHealthCheck(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HealthCheckMiddleware>();
    }
}