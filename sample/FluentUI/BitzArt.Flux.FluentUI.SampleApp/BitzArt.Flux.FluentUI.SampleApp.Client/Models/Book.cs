using System.Text.Json.Serialization;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class Book
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("authorId")]
    public int? AuthorId { get; set; }
}
