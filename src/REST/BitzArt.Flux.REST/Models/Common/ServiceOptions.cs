using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class ServiceOptions(string? baseUrl)
{
    public string? BaseUrl { get; set; } = baseUrl;

    public JsonSerializerOptions SerializerOptions { get; set; } = new();
    public Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }
}
