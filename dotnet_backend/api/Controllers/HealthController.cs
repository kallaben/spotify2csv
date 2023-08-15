using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController
{
    private readonly HttpContext? _httpContext;

    public HealthController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    [Route("")]
    [HttpGet]
    public void Check()
    {
        _httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
    }
}