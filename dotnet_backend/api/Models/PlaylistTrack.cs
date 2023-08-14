namespace api.Models;

public class PlaylistTrack
{
    private string Id { get; set; }
    private string Name { get; set; }
    private IList<string> Artists { get; set; }
    private string Duration { get; set; }
    private string AlbumName { get; set; }
    private string IsLocal { get; set; }
    private DateTime ReleaseDate { get; set; }
    private string AddedBy { get; set; }
    private DateTime AddedAt { get; set; }
}