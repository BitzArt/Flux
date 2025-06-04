using System.Text.Json;

namespace BitzArt.Flux.Json;

internal class FluxJsonServiceConfiguration
{
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();
    public string? BaseFilePath { get; set; }

    public FluxJsonServiceConfiguration(string? baseFilePath)
    {
        BaseFilePath = baseFilePath?.TrimEnd('/');
    }
}
