using System.Collections;

namespace BitzArt.Flux;

/// <summary>
/// Represents a collection of parameters to be used in a Flux operation.
/// </summary>
public interface IRequestParameters
{
    /// <summary>
    /// Values collection.
    /// </summary>
    public ICollection Values { get; }
}
