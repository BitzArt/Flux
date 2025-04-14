using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring a REST Flux service.
/// </summary>
public static class UsingRestExtension
{
    /// <summary>
    /// Implements a REST Flux service.
    /// </summary>
    /// <returns>
    /// A <see cref="IFluxRestServiceBuilder"/> for further configuration.
    /// </returns>
    public static IFluxRestServiceBuilder UsingRest(this IFluxServiceBuilder prebuilder, string? baseUrl = null)
    {
        var builder = new FluxRestServiceBuilder(prebuilder, baseUrl);

        var fluxServiceProvider = builder.Registration;
        builder.ServiceRegistration.ServiceRegistrations.Add(fluxServiceProvider);

        // If configuration action is null, do nothing
        builder.HttpClientConfiguration ??= (_, _) => { };

        var httpClientBuilder = builder.ServiceCollection.AddHttpClient(fluxServiceProvider.ServiceName, (serviceProvider, httpClient) =>
        {
            builder.HttpClientConfiguration(serviceProvider, httpClient);
        });

        builder.ServiceCollection.AddScoped<IFluxServiceContext>(x =>
        {
            return new FluxServiceContext(fluxServiceProvider, x);
        });

        return builder;
    }

    /// <inheritdoc cref="UsingRest(IFluxServiceBuilder,string)"/>
    public static IFluxRestServiceBuilder UsingRest<THandler>(this IFluxServiceBuilder prebuilder, string? baseUrl = null)
        where THandler : DelegatingHandler
    {
        var builder = new FluxRestServiceBuilder(prebuilder, baseUrl);

        var fluxServiceProvider = builder.Registration;
        builder.ServiceRegistration.ServiceRegistrations.Add(fluxServiceProvider);

        builder.ServiceCollection.AddHttpClient(fluxServiceProvider.ServiceName, (serviceProvider, httpClient) =>
        {
            builder.HttpClientConfiguration?.Invoke(serviceProvider, httpClient);
        }).AddHttpMessageHandler<THandler>();

        builder.ServiceCollection.AddScoped<IFluxServiceContext>(x =>
        {
            return new FluxServiceContext(fluxServiceProvider, x);
        });

        return builder;
    }
}
