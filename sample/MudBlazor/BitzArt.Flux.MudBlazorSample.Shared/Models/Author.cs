using System.Text.Json.Serialization;

namespace BitzArt.Flux.MudBlazorSample;

public class Author
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
