using System.Collections;
using System.Collections.Generic;

namespace BitzArt.Flux;

/// <summary>
/// A collection of parameters to be used in a Flux operation.
/// </summary>
public class OperationParameters : IFluxOperationParameters
{
    ICollection IFluxOperationParameters.Values => Parameters;

    /// <summary>
    /// Parameters collection.
    /// </summary>
    public virtual List<object> Parameters { get; set; }

    /// <inheritdoc cref="OperationParameters(ICollection{object})"/>"
    public OperationParameters(params object[] parameters) : this((ICollection<object>)parameters) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationParameters"/> class.
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public OperationParameters(ICollection<object> parameters)
    {
        Parameters = [.. parameters];
    }
}
