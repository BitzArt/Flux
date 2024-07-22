using System.Text.Json.Serialization;

namespace BitzArt.Flux.MudBlazorSample;

public class Book
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("authorId")]
    public int? AuthorId { get; set; }

    [JsonPropertyName("publishYear")]
    public int? PublishYear { get; set; }

    public string PublishYearDisplay
    {
        get
        {
            if (!PublishYear.HasValue) return string.Empty;
            if (PublishYear < 0) return $"~{-PublishYear} B.C.";
            if (PublishYear < 1000) return $"{PublishYear} A.D.";
            return PublishYear.ToString()!;
        }
    }
}
