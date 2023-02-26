using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class Session
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string SessionId { get; set; }
    public SpotifyAuthentication? SpotifyAuthentication { get; set; }
    
    public bool HasValidAuthenticationToken()
    {
        return SpotifyAuthentication != null;
    }
}
