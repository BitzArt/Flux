using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class FluxRestServiceConfiguration
{
    public string? BasePath { get; set; }

    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();
    public Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }

    public FluxRestServiceConfiguration(string? basePath)
    {
        BasePath = basePath?.TrimEnd('/');
    }
}
