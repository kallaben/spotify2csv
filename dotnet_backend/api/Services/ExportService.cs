using api.Gateways;

namespace api.Services;

public class ExportService
{
    private readonly UserContext _userContext;
    private readonly SpotifyApiGateway _spotifyApiGateway;

    public ExportService(UserContext userContext, SpotifyApiGateway spotifyApiGateway)
    {
        _userContext = userContext;
        _spotifyApiGateway = spotifyApiGateway;
    }

    public async Task<string> GetSpotifyPlaylistsAsCsv()
    {
        var spotifyApiAccessToken = await _userContext.GetSpotifyApiAccessTokenForCurrentUser();
        var getPlaylistsResponse = await _spotifyApiGateway.GetPlaylistsForUser(spotifyApiAccessToken);
        
        var playlist = getPlaylistsResponse.items.First();

        var allSongNames = new List<string>();
        for (var offset = 0; offset < playlist.tracks.total; offset += 50)
        {
            var getTracksResponse = await _spotifyApiGateway.GetPlaylistTracks(spotifyApiAccessToken, playlist.id, offset);
            var songNames = getTracksResponse.items.Select(trackItem => trackItem.track.name);
            allSongNames.AddRange(songNames);
        }

        return $"Playlist: {playlist.name}\n{String.Join("\n", allSongNames)}";
    }
}
