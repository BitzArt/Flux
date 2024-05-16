using System.Text.Json.Serialization;

namespace BitzArt.Flux;

/// <summary>
/// Represents a request for a Flux request.
/// </summary>
public class FluxRequestInfo
{
    /// <summary>
    /// The request's parameters.
    /// </summary>
    [JsonPropertyName("parameters")]
    public IEnumerable<string> Parameters { get; set; } = null!;

    /// <summary>
    /// Creates a new instance of <see cref="FluxRequestInfo"/>.
    /// </summary>
    public FluxRequestInfo() { }

    /// <summary>
    /// Creates a new instance of <see cref="FluxRequestInfo"/>.
    /// </summary>
    /// <param name="parameters"></param>
    public FluxRequestInfo(object[] parameters)
        : this(parameters.Select(p => p.ToString()!)) { }

    /// <summary>
    /// Creates a new instance of <see cref="FluxRequestInfo"/>.
    /// </summary>
    /// <param name="parameters"></param>
    public FluxRequestInfo(IEnumerable<string> parameters)
    {
        Parameters = parameters;
    }

    /// <summary>
    /// Compares the two instances of <see cref="FluxRequestInfo"/>.
    /// </summary>
    /// <param name="other">The instance to compare with.</param>
    /// <returns>True if the instances are equal, otherwise false.</returns>
    public virtual bool Compare(FluxRequestInfo other)
    {
        return Parameters.SequenceEqual(other.Parameters);
    }
}
