using api.Gateways;
using api.Models;
using Newtonsoft.Json.Linq;

namespace api.Services;

public class SpotifyAuthorizationService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly SpotifyApiGateway _spotifyApiGateway;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SpotifyAuthorizationService(ISessionRepository sessionRepository, SpotifyApiGateway spotifyApiGateway, IHttpContextAccessor httpContextAccessor)
    {
        _sessionRepository = sessionRepository;
        _spotifyApiGateway = spotifyApiGateway;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<string> Authenticate(string authenticationCode, string state)
    {
        var stateElements = state.Split(":", 2);
        var sessionId = stateElements[0];
        var originalPath = stateElements[1];
        
        var spotifyAuthentication = await this._spotifyApiGateway.GetAccessToken(authenticationCode);

        await _sessionRepository.UpdateSessionWithTokens(sessionId, spotifyAuthentication);

        var redirectPath = GetRedirectUrlFromOriginalPath(originalPath);
        return redirectPath;
    }
    
    public async Task<bool> IsAuthenticated()
    {
        var sessionId = _httpContextAccessor.HttpContext.Session.Id;
        
        var session = await _sessionRepository.GetSessionOrNull(sessionId);

        return session?.HasValidAuthenticationToken() ?? false;
    }

    private string GetRedirectUrlFromOriginalPath(string originalPath)
    {
        return "/";
    }
}
