namespace api.Models.Csv;

public class PlaylistCsvRow
{
    public string PlaylistName { get; set; }
    public DateTime AddedAt { get; set; }
    public string ArtistNames { get; set; }
    public string AlbumName { get; set; }
    public string TrackName { get; set; }
    public string ReleaseDate { get; set; }
    public string IsLocal { get; set; }
    public string AddedBy { get; set; }
    public string Duration { get; set; }
    public string TrackId { get; set; }
}