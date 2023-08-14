namespace api.Models;

public class PlaylistTrack
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Artists { get; set; }
    public string Duration { get; set; }
    public string AlbumName { get; set; }
    public bool IsLocal { get; set; }
    public string ReleaseDate { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedAt { get; set; }
}