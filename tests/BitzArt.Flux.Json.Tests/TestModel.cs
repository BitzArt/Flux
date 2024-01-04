using System.Text.Json.Serialization;

namespace BitzArt.Flux;

internal class TestModel
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
}
