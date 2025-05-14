using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class ServiceConfiguration
{
    public string? BasePath { get; set; }

    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();
    public Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }

    public ServiceConfiguration(string? basePath)
    {
        BasePath = basePath?.TrimEnd('/');
    }
}
