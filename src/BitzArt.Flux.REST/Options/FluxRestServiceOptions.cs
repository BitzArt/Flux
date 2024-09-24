using System.Text.Json;

namespace BitzArt.Flux;

public class FluxRestServiceOptions(string? baseUrl)
{
    public string? BaseUrl { get; set; } = baseUrl;
    public JsonSerializerOptions SerializerOptions { get; set; } = new JsonSerializerOptions();
}