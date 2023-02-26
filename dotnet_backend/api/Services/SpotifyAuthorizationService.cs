using api.Gateways;
using api.Models;
using Newtonsoft.Json.Linq;

namespace api.Services;

public class SpotifyAuthorizationService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly SpotifyApiGateway _spotifyApiGateway;

    public SpotifyAuthorizationService(ISessionRepository sessionRepository, SpotifyApiGateway spotifyApiGateway)
    {
        _sessionRepository = sessionRepository;
        _spotifyApiGateway = spotifyApiGateway;
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

    private string GetRedirectUrlFromOriginalPath(string originalPath)
    {
        return "/";
    }
}
