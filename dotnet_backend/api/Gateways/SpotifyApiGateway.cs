using api.Models;
using Newtonsoft.Json.Linq;

namespace api.Gateways;

public class SpotifyApiGateway
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId = "543a4066a8a94ff7ab4705453913eb4e";
    private readonly string _clientSecret = "";


    public SpotifyApiGateway(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<GetPlaylistsResponse> GetPlaylistsForUser(string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/playlists?limit=50")
        {
            Headers =
            {
                {"Authorization", $"Bearer {accessToken}"}
            }
        };
        
        var response = await _httpClient.SendAsync(request);
        return JToken
            .Parse(await response.Content.ReadAsStringAsync())
            .ToObject<GetPlaylistsResponse>();
    }

    public async Task<GetTracksResponse> GetPlaylistTracks(string accessToken, string playlistId, int offset)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/playlists/{playlistId}/tracks?limit=50&offset={offset}")
        {
            Headers =
            {
                {"Authorization", $"Bearer {accessToken}"}
            }
        };
        
        var response = await _httpClient.SendAsync(request);
        
        return JToken
            .Parse(await response.Content.ReadAsStringAsync())
            .ToObject<GetTracksResponse>();
    }
    
    public async Task<SpotifyAuthentication> GetAccessToken(string authenticationCode)
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
        
        return new SpotifyAuthentication()
        {
            AccessToken = result["access_token"].ToString(),
            RefreshToken = result["refresh_token"].ToString()
        };
    }
}
