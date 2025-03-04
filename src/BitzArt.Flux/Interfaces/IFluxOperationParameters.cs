using System.Collections;

namespace BitzArt.Flux;

/// <summary>
/// Represents a collection of parameters to be used in a Flux operation.
/// </summary>
public interface IFluxOperationParameters
{
    /// <summary>
    /// Parameter value collection.
    /// </summary>
    public ICollection Values { get; }
}
