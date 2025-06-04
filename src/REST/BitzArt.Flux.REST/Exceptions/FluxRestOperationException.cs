namespace BitzArt.Flux.REST;

/// <summary>
/// Exception thrown when a REST operation fails.
/// </summary>
public class FluxRestOperationException : Exception
{
    /// <summary>
    /// The HTTP response message associated with the operation failure, if any.
    /// </summary>
    public HttpResponseMessage? Response { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestOperationException"/> class with a specified error message, inner exception, and HTTP response message.
    /// </summary>
    /// <param name="message">Error message that describes the error.</param>
    /// <param name="innerException">Inner exception that is the cause of the current exception, or null if no inner exception is specified.</param>
    /// <param name="response">Response message associated with the operation failure, if any.</param>
    public FluxRestOperationException(string message, Exception? innerException = null, HttpResponseMessage? response = null) : base(message, innerException)
    {
        Response = response;
    }
}
