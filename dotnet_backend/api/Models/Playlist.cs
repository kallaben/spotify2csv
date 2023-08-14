namespace api.Models;

public class Playlist
{
    public string Name { get; set; }
    public string Id { get; set; }
    public IEnumerable<PlaylistTrack> Tracks { get; set; }
    public string Creator { get; set; }
}