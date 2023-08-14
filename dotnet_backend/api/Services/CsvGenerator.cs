using System.Globalization;
using api.Models;
using api.Models.Csv;
using CsvHelper;

namespace api.Services;

public class CsvGenerator
{
    public string GenerateCsvFromPlaylists(IList<Playlist> playlists)
    {
        var rows = playlists.SelectMany(MapPlaylistToRow);

        using (var strWriter = new StringWriter())
        using (var csvWriter =
               new CsvWriter(strWriter, CultureInfo.InvariantCulture))
        {
            csvWriter.Context.RegisterClassMap<PlaylistCsvRowMap>();
            csvWriter.WriteRecords(rows);


            return strWriter.ToString();
        }
    }

    private IEnumerable<PlaylistCsvRow> MapPlaylistToRow(Playlist playlist)
    {
        return playlist.Tracks.Select(track => new PlaylistCsvRow
            {
                PlaylistName = playlist.Name,
                TrackId = track.Id,
                TrackName = track.Name,
                Duration = track.Duration,
                AddedAt = track.AddedAt,
                AddedBy = track.AddedBy,
                AlbumName = track.AlbumName,
                IsLocal = track.IsLocal ? "Yes" : "No",
                ReleaseDate = track.ReleaseDate,
                ArtistNames = String.Join(", ", track.Artists),
                PlaylistId = playlist.Id,
            }
        );
    }
}