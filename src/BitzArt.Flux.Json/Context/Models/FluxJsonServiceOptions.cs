using System.Text.Json;

namespace BitzArt.Flux;

public class FluxJsonServiceOptions
{
    public JsonSerializerOptions SerializerOptions { get; set; } = new();
}