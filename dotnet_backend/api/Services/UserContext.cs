namespace api.Services;

public class UserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string GetSessionId()
    {
        return _httpContextAccessor.HttpContext.Session.Id;
    }
}
