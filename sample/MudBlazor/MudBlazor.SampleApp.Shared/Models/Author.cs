using System.Text.Json.Serialization;

namespace MudBlazor.SampleApp;

public class Author
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
