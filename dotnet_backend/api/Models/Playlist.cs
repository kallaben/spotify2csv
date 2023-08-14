namespace api.Models;

public class Playlist
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Id { get; set; }
    public IList<PlaylistTrack> Tracks { get; set; }
}