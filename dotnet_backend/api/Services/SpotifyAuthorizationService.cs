using api.Models;
using Newtonsoft.Json.Linq;

namespace api.Services;

public class SpotifyAuthorizationService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly HttpClient _httpClient;
    private readonly string _clientId = "543a4066a8a94ff7ab4705453913eb4e";
    private readonly string _clientSecret = "";
        

    public SpotifyAuthorizationService(IHttpClientFactory httpClientFactory, ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
        _httpClient = httpClientFactory.CreateClient();
    }


    public async Task Authenticate(string authenticationCode, string sessionId)
    {
        var authString = $"{_clientId}:{_clientSecret}";
        var base64AuthString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authString));
        var basicAuthString = $"Basic {base64AuthString}";
        
        var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
        {
            Headers =
            {
                {"Authorization", basicAuthString}
            },
            Content = new FormUrlEncodedContent(new Dictionary<string, string> 
            {
                {"code", authenticationCode},
                {"redirect_uri", "http://localhost:4200/redirect"},
                {"grant_type", "authorization_code"}
            })
        };
        
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JObject.Parse(responseBody);
        var accessToken= result["access_token"].ToString();
        var refreshToken = result["refresh_token"].ToString();

        await _sessionRepository.UpdateSessionWithTokens(sessionId, accessToken, refreshToken);
    }
}
