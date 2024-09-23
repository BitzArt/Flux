using System.Net;

namespace BitzArt.Flux;

/// <summary>
/// Exception thrown when a non-success status code is returned by the REST service.
/// </summary>
public class FluxRestNonSuccessStatusCodeException : FluxRestRequestHandlingException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestNonSuccessStatusCodeException"/> class.
    /// </summary>
    /// <param name="response"></param>
    public FluxRestNonSuccessStatusCodeException(HttpResponseMessage response)
        : this(response.StatusCode) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestNonSuccessStatusCodeException"/> class.
    /// </summary>
    /// <param name="statusCode"></param>
    public FluxRestNonSuccessStatusCodeException(HttpStatusCode statusCode)
        : this($"External REST Service responded with http status code '{statusCode}'.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestNonSuccessStatusCodeException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public FluxRestNonSuccessStatusCodeException(string message)
        : base(message) { }
}
