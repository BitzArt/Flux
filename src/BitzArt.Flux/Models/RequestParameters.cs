using System.Collections;

namespace BitzArt.Flux;

/// <inheritdoc/>
public sealed class RequestParameters(params object?[] parameters) : RequestParameters<object?>(parameters)
{
}

/// <summary>
/// A collection of parameters to be used in a Flux operation.
/// </summary>
/// <typeparam name="T">Type of the parameters.</typeparam>
public class RequestParameters<T> : IRequestParameters
{
    ICollection IRequestParameters.Values => Parameters;

    /// <summary>
    /// Parameters collection.
    /// </summary>
    public virtual List<T> Parameters { get; set; }

    /// <inheritdoc cref="RequestParameters{T}(ICollection{T})"/>"
    public RequestParameters(params T[] parameters) : this((ICollection<T>)parameters) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestParameters{T}"/> class.
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public RequestParameters(ICollection<T> parameters)
    {
        Parameters = [.. parameters];
    }
}
