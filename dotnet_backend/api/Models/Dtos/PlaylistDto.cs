using System.Runtime.Serialization;

namespace api.Models.Dtos;

[DataContract]
public class PlaylistDto
{
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public DateTime createdAt { get; set; }
    [DataMember]
    public string creator { get; set; }
    [DataMember]
    public string id { get; set; }
}
