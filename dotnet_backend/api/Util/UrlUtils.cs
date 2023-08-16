using Microsoft.AspNetCore.Http.Extensions;

namespace api.Util;

public static class UrlUtils
{
    public static string GetOrigin(HttpContext httpContext)
    {
        return new Uri(httpContext.Request.GetDisplayUrl()).GetLeftPart(
            UriPartial.Authority);
    }
}