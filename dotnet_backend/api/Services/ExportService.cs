using api.Gateways;
using api.Models;
using api.Models.Dtos;

namespace api.Services;

public class ExportService
{
    private readonly UserContext _userContext;
    private readonly SpotifyApiGateway _spotifyApiGateway;
    private readonly ILogger<ExportService> _logger;
    private readonly CsvGenerator _csvGenerator;

    public ExportService(
        UserContext userContext,
        SpotifyApiGateway spotifyApiGateway,
        ILogger<ExportService> logger,
        CsvGenerator csvGenerator)
    {
        _userContext = userContext;
        _spotifyApiGateway = spotifyApiGateway;
        _logger = logger;
        _csvGenerator = csvGenerator;
    }

    public async Task<string> GetSpotifyPlaylistsAsCsv(IEnumerable<string> playlistIds)
    {
        var spotifyApiAccessToken = await _userContext.GetSpotifyApiAccessTokenForCurrentUser();
        var playlistsResponse = await _spotifyApiGateway.GetPlaylistsForUser(spotifyApiAccessToken);

        var playlists = new List<Playlist>();
        foreach (var playlistId in playlistIds)
        {
            var getTracksResponse = await _spotifyApiGateway.GetPlaylistTracks(spotifyApiAccessToken, playlistId);
            var playlist = playlistsResponse.items.First(playlist => playlist.id == playlistId);

            playlists.Add(MapGetTracksResponseToPlaylist(getTracksResponse, playlist));
        }

        return _csvGenerator.GenerateCsvFromPlaylists(playlists);
    }

    public async Task<IEnumerable<PlaylistDto>> GetPlaylists()
    {
        var spotifyApiAccessToken = await _userContext.GetSpotifyApiAccessTokenForCurrentUser();
        var playlistsResponse = await _spotifyApiGateway.GetPlaylistsForUser(spotifyApiAccessToken);

        _logger.LogInformation("Request successfully made to https://api.spotify.com/v1/me/playlists");
        var playlists = playlistsResponse.items.Select(playlist => new PlaylistDto
        {
            creator = playlist.owner.display_name,
            id = playlist.id,
            name = playlist.name
        });

        return playlists;
    }

    private Playlist MapGetTracksResponseToPlaylist(GetTracksResponse getTracksResponse, Item playlist)
    {
        return new Playlist
        {
            Creator = playlist.owner.display_name,
            Id = playlist.id,
            Name = playlist.name,
            Tracks = getTracksResponse.items.Select(track => new PlaylistTrack
            {
                Artists = track.track.artists.Select(artist => artist.name),
                Name = track.track.name,
                Id = track.track.id,
                Duration = convertFromMsToReadableFormat(track.track.duration_ms),
                AddedAt = track.added_at,
                AddedBy = track.added_by.uri,
                AlbumName = track.track.album.name,
                IsLocal = track.is_local,
                ReleaseDate = track.track.album.release_date
            })
        };
    }

    private string convertFromMsToReadableFormat(int trackDurationMs)
    {
        var timeSpan = TimeSpan.FromMilliseconds(trackDurationMs);

        return $"{timeSpan.Minutes}:{timeSpan.Seconds}";
    }
}