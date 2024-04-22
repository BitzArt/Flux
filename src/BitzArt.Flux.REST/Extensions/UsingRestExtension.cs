using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace BitzArt.Flux;

public static class UsingRestExtension
{
    public static IFluxRestServiceBuilder UsingRest(this IFluxServicePreBuilder prebuilder, string? baseUrl = null)
    {
        var builder = new FluxRestServiceBuilder(prebuilder, baseUrl);

        var fluxServiceProvider = builder.ServiceFactory;
        builder.Factory.ServiceContexts.Add(fluxServiceProvider);

        // If configuration action is null, do nothing
        var httpClientConfiguration = builder.HttpClientConfiguration;
        httpClientConfiguration ??= (_, _) => { };

        builder.Services.AddHttpClient(fluxServiceProvider.ServiceName, httpClientConfiguration);

        builder.Services.AddScoped<IFluxServiceContext>(x =>
        {
            return new FluxServiceContext(fluxServiceProvider, x);
        });

        return builder;
    }
}
