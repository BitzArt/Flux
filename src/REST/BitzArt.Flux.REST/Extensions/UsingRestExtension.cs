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
    public static IFluxRestServiceBuilder UsingRest(this IFluxServicePreBuilder prebuilder, string? baseUrl = null)
    {
        var builder = new FluxRestServiceBuilder(prebuilder, baseUrl);

        var fluxServiceProvider = builder.ServiceFactory;
        builder.Factory.ServiceContexts.Add(fluxServiceProvider);

        // If configuration action is null, do nothing
        builder.HttpClientConfiguration ??= (_, _) => { };

        var httpClientBuilder = builder.Services.AddHttpClient(fluxServiceProvider.ServiceName, (serviceProvider, httpClient) =>
        {
            builder.HttpClientConfiguration(serviceProvider, httpClient);
        });

        builder.Services.AddScoped<IFluxServiceContext>(x =>
        {
            return new FluxServiceContext(fluxServiceProvider, x);
        });

        return builder;
    }

    /// <inheritdoc cref="UsingRest(IFluxServicePreBuilder,string)"/>
    public static IFluxRestServiceBuilder UsingRest<THandler>(this IFluxServicePreBuilder prebuilder, string? baseUrl = null)
        where THandler : DelegatingHandler
    {
        var builder = new FluxRestServiceBuilder(prebuilder, baseUrl);

        var fluxServiceProvider = builder.ServiceFactory;
        builder.Factory.ServiceContexts.Add(fluxServiceProvider);

        // If configuration action is null, do nothing
        builder.HttpClientConfiguration ??= (_, _) => { };

        builder.Services.AddHttpClient(fluxServiceProvider.ServiceName, (serviceProvider, httpClient) =>
        {
            builder.HttpClientConfiguration(serviceProvider, httpClient);
        }).AddHttpMessageHandler<THandler>();

        builder.Services.AddScoped<IFluxServiceContext>(x =>
        {
            return new FluxServiceContext(fluxServiceProvider, x);
        });

        return builder;
    }
}
