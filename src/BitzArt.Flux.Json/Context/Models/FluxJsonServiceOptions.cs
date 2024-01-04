using System.Text.Json;

namespace BitzArt.Flux;

public class FluxJsonServiceOptions
{
    public string? BaseUrl { get; set; }
    public JsonSerializerOptions SerializerOptions { get; set; } = new();
}