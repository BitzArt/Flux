using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

/// <summary>
/// <para>
/// Defines methods for intercepting HTTP requests and responses in a REST client pipeline.
/// </para>
/// <para>
/// Implement this interface to customize the behavior of HTTP requests and responses. This can be used
/// for scenarios such as logging, modifying requests or responses, adding custom headers, or handling specific response
/// conditions.
/// </para>
/// <para>
/// When registered with a <see cref="ServiceLifetime.Transient"/> lifetime,
/// a single instance of the interceptor will be created for each request,
/// handling both the request and response phases.
/// </para>
/// </summary>
public interface IFluxRestInterceptor
{
    /// <summary>
    /// Intercepts the HTTP request before it is sent.
    /// </summary>
    /// <param name="client">The HTTP client that will be sending the request.</param>
    /// <param name="request">The HTTP request message to intercept.</param>
    /// <param name="cancellationToken"> A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task OnRequestAsync(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken);

    /// <summary>
    /// Intercepts the HTTP response after it is received.
    /// </summary>
    /// <param name="client"> The HTTP client that received the response.</param>
    /// <param name="response">The HTTP response message to intercept.</param>
    /// <param name="cancellationToken"> A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task OnResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken);
}
