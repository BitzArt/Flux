using System.Text.Json.Serialization;

namespace BitzArt.Flux;

internal class TestModelUpdateResponse
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    public TestModelUpdateResponse(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }

    public TestModelUpdateResponse()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
