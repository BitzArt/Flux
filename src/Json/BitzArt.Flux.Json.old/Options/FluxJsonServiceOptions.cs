using System.Text.Json;

namespace BitzArt.Flux;

internal class FluxJsonServiceOptions
{
    public JsonSerializerOptions SerializerOptions { get; set; } = new();
    public string? BaseFilePath { get; set; }
}