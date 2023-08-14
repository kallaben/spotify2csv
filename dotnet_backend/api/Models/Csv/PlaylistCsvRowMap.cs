using CsvHelper.Configuration;

namespace api.Models.Csv;

public class PlaylistCsvRowMap : ClassMap<PlaylistCsvRow>
{
    public PlaylistCsvRowMap()
    {
        Map(playlist => playlist.PlaylistName).Index(0).Name("Playlist");
        Map(playlist => playlist.AddedAt).Index(1).Name("Track Added At");
        Map(playlist => playlist.ArtistNames).Index(2).Name("Artists");
        Map(playlist => playlist.AlbumName).Index(3).Name("Album");
        Map(playlist => playlist.TrackName).Index(4).Name("Track Name");
        Map(playlist => playlist.ReleaseDate).Index(5).Name("Released At");
        Map(playlist => playlist.IsLocal).Index(6).Name("Is Local File");
        Map(playlist => playlist.AddedBy).Index(7).Name("Added By");
        Map(playlist => playlist.Duration).Index(8).Name("Track Duration");
        Map(playlist => playlist.TrackId).Index(9).Name("Spotify Track ID");
    }
}