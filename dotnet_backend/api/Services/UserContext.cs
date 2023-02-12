namespace api.Services;

public class UserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISessionRepository _sessionRepository;

    public UserContext(IHttpContextAccessor httpContextAccessor, ISessionRepository sessionRepository)
    {
        this._httpContextAccessor = httpContextAccessor;
        _sessionRepository = sessionRepository;
    }

    public string GetSessionId()
    {
        return _httpContextAccessor.HttpContext.Session.Id;
    }

    public async Task<string> GetSpotifyApiAccessTokenForCurrentUser()
    {
        var session = await _sessionRepository.GetSessionOrNull(this.GetSessionId());
        
        return session.AccessToken;
    }
}
