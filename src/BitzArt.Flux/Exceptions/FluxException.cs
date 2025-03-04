namespace BitzArt.Flux;

/// <summary>
/// An <see cref="Exception"/> thrown by the Flux library.
/// </summary>
public class FluxException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluxException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Inner exception.</param>
    public FluxException(string message, Exception? innerException = null) : base(message, innerException) { }
}
