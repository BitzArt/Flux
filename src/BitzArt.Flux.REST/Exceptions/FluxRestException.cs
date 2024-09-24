namespace BitzArt.Flux;

/// <summary>
/// A exception thrown when a generic error occurs in Flux.REST.
/// </summary>
public class FluxRestException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public FluxRestException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public FluxRestException(string message) : base(message) { }
}
