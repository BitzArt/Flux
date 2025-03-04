using System.Collections;

namespace BitzArt.Flux;

/// <summary>
/// Parameters for a REST request operation.
/// </summary>
public interface IFluxRestOperationParameters : IFluxOperationParameters
{
    /// <summary>
    /// A map of parameter names and values.
    /// </summary>
    public Dictionary<string, object> ValueMap { get; }

    ICollection IFluxOperationParameters.Values => ValueMap.Values;
}
