namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when a Flux Service Provider is not found.
/// </summary>
public class FluxServiceProviderNotFoundException : FluxException
{
    internal const string DefaultMessage = "Requested Flux Service Provider was not found.";

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxServiceProviderNotFoundException"/> class.
    /// </summary>
    public FluxServiceProviderNotFoundException(string message = DefaultMessage, Exception? innerException = null)
        : base(message, innerException) { }
}
