using System.Text.Json.Serialization;

namespace BitzArt.Flux;

public class FluxRequestInfo
{
    [JsonPropertyName("parameters")]
    public IEnumerable<string> Parameters { get; set; } = null!;

    public FluxRequestInfo() { }

    public FluxRequestInfo(object[] parameters)
        : this(parameters.Select(p => p.ToString()!)) { }

    public FluxRequestInfo(IEnumerable<string> parameters)
    {
        Parameters = parameters;
    }

    public virtual bool Compare(FluxRequestInfo other)
    {
        return Parameters.SequenceEqual(other.Parameters);
    }
}
