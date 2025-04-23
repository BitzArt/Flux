using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BitzArt.Flux.REST;

/// <summary>
/// Extension methods for configuring a REST Flux service.
/// </summary>
public static class UsingRestExtension
{
    /// <inheritdoc cref="UsingRest(IFluxServiceBuilder,string)"/>
    public static IFluxRestServiceBuilder UsingRest<THandler>(this IFluxServiceBuilder sourceBuilder, string? baseUrl = null)
        where THandler : DelegatingHandler
        => sourceBuilder.UsingRest(baseUrl, (builder) => builder.AddHttpMessageHandler<THandler>());

    /// <summary>
    /// Implements a REST Flux service.
    /// </summary>
    /// <returns>
    /// A <see cref="IFluxRestServiceBuilder"/> for further configuration.
    /// </returns>
    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Interface cast is required to access default implementation methods.")]
    public static IFluxRestServiceBuilder UsingRest(this IFluxServiceBuilder sourceBuilder, string? baseUrl = null, Action<IHttpClientBuilder>? configureHttpClient = null)
    {
        IFluxRestServiceBuilder builder = new FluxRestServiceBuilder(sourceBuilder, baseUrl);

        var httpClientBuilder = builder.ServiceCollection
            .AddHttpClient(builder.ServiceName, (serviceProvider, httpClient) =>
            {
                builder.HttpClientConfiguration?.Invoke(serviceProvider, httpClient);
            });

        configureHttpClient?.Invoke(httpClientBuilder);

        return builder;
    }
}
