using System.Text.Json;

namespace BitzArt.Flux;

public class FluxRestServiceOptions
{
    public string? BaseUrl { get; set; }
    public JsonSerializerOptions SerializerOptions { get; set; }

    public FluxRestServiceOptions(string? baseUrl)
    {
        BaseUrl = baseUrl;
        SerializerOptions = new JsonSerializerOptions();
    }
}