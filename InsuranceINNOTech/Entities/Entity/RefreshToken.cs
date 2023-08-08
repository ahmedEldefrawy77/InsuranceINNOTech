using System.Text.Json.Serialization;

namespace InsuranceINNOTech;

public class RefreshToken : BaseEntity
{
    public string? Value { get; set; }
    public DateTime DateCreatedAt { get; set; }
    public DateTime DateExAt { get; set; }
    [JsonIgnore]
    public User? user { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}
