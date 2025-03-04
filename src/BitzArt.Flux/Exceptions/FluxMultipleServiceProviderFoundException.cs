namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when multiple Flux Service Providers are found.
/// </summary>
public class FluxMultipleServiceProviderFoundException : FluxException
{
    internal const string DefaultMessage = "Multiple matching Flux Service Providers were found.";

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxMultipleServiceProviderFoundException"/> class.
    /// </summary>
    public FluxMultipleServiceProviderFoundException(string message = DefaultMessage, Exception? innerException = null)
        : base(message, innerException) { }
}