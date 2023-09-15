using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class UsingRestExtension
{
    public static IFluxRestServiceBuilder UsingRest(this IFluxServicePreBuilder prebuilder, string? baseUrl = null)
    {
        var builder = new FluxRestServiceBuilder(prebuilder, baseUrl);

        var provider = builder.Provider;
        builder.Factory.Providers.Add(provider);

        builder.Services.AddHttpClient(provider.ServiceName, x =>
        {
            var configureHttpClient = builder.HttpClientConfiguration;
            if (configureHttpClient is not null) configureHttpClient(x);
        });

        builder.Services.AddSingleton(provider);

        return builder;
    }
}
