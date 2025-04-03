namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when a Flux Service Provider is not found.
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
