using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace BitzArt.Flux.Rest;

/// <summary>
/// Extension methods for configuring a REST Flux service.
/// </summary>
public static class UsingRestExtension
{
    /// <inheritdoc cref="UsingRest(IFluxServiceProtocolConfigurator,string)"/>
    /// <typeparam name="THandler"><see cref="DelegatingHandler"/> type for the HTTP client to use.</typeparam>
    public static IFluxRestServiceBuilder UsingRest<THandler>(this IFluxServiceProtocolConfigurator protocolConfigurator, string? baseUrl = null)
        where THandler : DelegatingHandler
        => protocolConfigurator.UsingRest(baseUrl, (builder) => builder.AddHttpMessageHandler<THandler>());

    /// <summary>
    /// Configures the service to use REST Flux implementation.
    /// </summary>
    /// <returns>
    /// A <see cref="IFluxRestServiceBuilder"/> for further REST service configuration.
    /// </returns>
    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Interface cast necessary to access default implementation methods.")]
    public static IFluxRestServiceBuilder UsingRest(this IFluxServiceProtocolConfigurator protocolConfigurator, string? baseUrl = null, Action<IHttpClientBuilder>? configureHttpClient = null)
    {
        IFluxRestServiceBuilder builder = new FluxRestServiceBuilder(protocolConfigurator.FluxBuilder, protocolConfigurator.ServiceName, baseUrl);

        builder.ServiceCollection.AddLogging();

        builder.ServiceCollection.TryAddSingleton<IHttpRequestMessageResolver, HttpRequestMessageResolver>();

        var httpClientBuilder = builder.ServiceCollection
            .AddHttpClient(builder.ServiceName, (serviceProvider, httpClient) =>
            {
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    httpClient.BaseAddress = new(baseUrl);
                }

                builder.ServiceConfiguration.HttpClientConfiguration?.Invoke(serviceProvider, httpClient);
            });

        configureHttpClient?.Invoke(httpClientBuilder);

        protocolConfigurator.OnServiceProtocolConfigured(builder);

        return builder;
    }
}
