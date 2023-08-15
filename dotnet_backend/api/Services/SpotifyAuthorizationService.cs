using api.Gateways;
using api.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace api.Services;

public class SpotifyAuthorizationService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly SpotifyApiGateway _spotifyApiGateway;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SpotifyAuthorizationService(
        ISessionRepository sessionRepository,
        SpotifyApiGateway spotifyApiGateway,
        IHttpContextAccessor httpContextAccessor)
    {
        _sessionRepository = sessionRepository;
        _spotifyApiGateway = spotifyApiGateway;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<string> Authenticate(
        string authenticationCode,
        string state)
    {
        var stateElements = state.Split(":", 2);
        var sessionId = stateElements[0];
        var originalPath = stateElements[1];

        var spotifyAuthentication =
            await this._spotifyApiGateway.GetAccessToken(authenticationCode);

        await _sessionRepository.UpdateSessionWithTokens(sessionId,
            spotifyAuthentication);

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

    public async Task<string> Login()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var sessionId = httpContext.Session.Id;
        var session = await _sessionRepository.GetSessionOrNull(sessionId);

        httpContext.Session.SetString("CustomSessionID", new Guid().ToString());

        if (session == null)
        {
            var newSession = new Session
            {
                SessionId = sessionId
            };

            await _sessionRepository.InsertSession(newSession);
        }

        var spotifySsoBaseUrl = "https://accounts.spotify.com/authorize";
        var queryParameters = new Dictionary<string, string?>
        {
            { "response_type", "code" },
            { "client_id", "543a4066a8a94ff7ab4705453913eb4e" },
            { "scope", "playlist-read-private" },
            { "redirect_uri", "http://localhost:4200/redirect" },
            { "state", $"{sessionId}:/" }
        };

        var spotifySsoUrl =
            QueryHelpers.AddQueryString(spotifySsoBaseUrl, queryParameters);

        return spotifySsoUrl;
    }
}