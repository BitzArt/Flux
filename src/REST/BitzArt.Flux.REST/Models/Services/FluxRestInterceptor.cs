namespace BitzArt.Flux.REST;

/// <summary>
/// Base class for intercepting HTTP requests and responses in a REST client pipeline.
/// </summary>
/// <remarks>
/// This class provides default implementations for the <see cref="IFluxRestInterceptor"/> interface methods.
/// </remarks>
public abstract class FluxRestInterceptor : IFluxRestInterceptor
{
    /// <inheritdoc/>
    public virtual Task OnRequestAsync(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task OnResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
