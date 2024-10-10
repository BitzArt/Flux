namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when an error occurs during the request handling.
/// </summary>
public class FluxRestRequestHandlingException : FluxRestException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestRequestHandlingException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public FluxRestRequestHandlingException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestRequestHandlingException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public FluxRestRequestHandlingException(string message) : base(message) { }
}
