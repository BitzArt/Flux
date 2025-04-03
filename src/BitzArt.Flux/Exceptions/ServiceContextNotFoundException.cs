namespace BitzArt.Flux;

/// <summary>
/// Indicates that the requested Flux Service Context was not found.
/// </summary>
public class ServiceContextNotFoundException : Exception
{
    internal const string DefaultMessage = "Requested Flux Service Context was not found.";

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceContextNotFoundException"/> class.
    /// </summary>
    public ServiceContextNotFoundException(string message = DefaultMessage, Exception? innerException = null)
        : base(message, innerException) { }
}
