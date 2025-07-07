using System.Text.Json;

namespace BitzArt.Flux.Rest;

internal class FluxRestServiceConfiguration
{
    public string ServiceName { get; private init; }

    public string? BasePath { get; set; }

    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();
    public Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }

    public FluxRestServiceConfiguration(string serviceName, string? basePath)
    {
        ServiceName = serviceName;
        BasePath = basePath?.TrimEnd('/');
    }
}
