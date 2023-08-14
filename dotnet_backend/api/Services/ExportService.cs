using api.Gateways;
using api.Models.Dtos;

namespace api.Services;

public class ExportService
{
    private readonly UserContext _userContext;
    private readonly SpotifyApiGateway _spotifyApiGateway;
    private readonly ILogger<ExportService> _logger;

    public ExportService(UserContext userContext, SpotifyApiGateway spotifyApiGateway, ILogger<ExportService> logger)
    {
        _userContext = userContext;
        _spotifyApiGateway = spotifyApiGateway;
        _logger = logger;
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

    public async Task<IEnumerable<PlaylistDto>> GetPlaylists()
    {
        var spotifyApiAccessToken = await _userContext.GetSpotifyApiAccessTokenForCurrentUser();
        var playlistsResponse = await _spotifyApiGateway.GetPlaylistsForUser(spotifyApiAccessToken);
        
        _logger.LogInformation("Request successfully made to https://api.spotify.com/v1/me/playlists");
        var playlists = playlistsResponse.items.Select(playlist => new PlaylistDto
        {
            creator = playlist.owner.display_name,
            createdAt = DateTime.UtcNow,
            id = playlist.id,
            name = playlist.name
        });

        return playlists;
    }
}
