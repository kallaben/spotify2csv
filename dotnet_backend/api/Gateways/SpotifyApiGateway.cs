using Newtonsoft.Json.Linq;

namespace api.Gateways;

public class SpotifyApiGateway
{
    private readonly HttpClient _httpClient;

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
}
