using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BitzArt.Flux.REST;

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
    /// Configures the <see cref="IFluxServiceProtocolConfigurator"/> by configuring it to use a REST service.
    /// </summary>
    /// <returns>
    /// A <see cref="IFluxRestServiceBuilder"/> for further service configuration.
    /// </returns>
    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Interface cast is necessary to access default implementation methods.")]
    public static IFluxRestServiceBuilder UsingRest(this IFluxServiceProtocolConfigurator protocolConfigurator, string? baseUrl = null, Action<IHttpClientBuilder>? configureHttpClient = null)
    {
        IFluxRestServiceBuilder builder = new FluxRestServiceBuilder(protocolConfigurator.FluxBuilder, protocolConfigurator.ServiceName, baseUrl);

        var httpClientBuilder = builder.ServiceCollection
            .AddHttpClient(builder.ServiceName, (serviceProvider, httpClient) =>
            {
                builder.ServiceConfiguration.HttpClientConfiguration?.Invoke(serviceProvider, httpClient);
            });

        configureHttpClient?.Invoke(httpClientBuilder);

        // Notify the source builder of a
        // service termination by a REST implementation,
        // ensuring that the service builder can not be used for
        // further terminations.
        protocolConfigurator.OnServiceProtocolConfigured(builder.ServiceName, builder);

        return builder;
    }
}
