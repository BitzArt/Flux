using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class UsingRestExtension
{
    public static IFluxRestServiceBuilder UsingRest(this IFluxServicePreBuilder prebuilder, string? baseUrl = null)
    {
        var builder = new FluxRestServiceBuilder(prebuilder, baseUrl);

        var serviceContext = builder.ServiceContext;
        builder.Factory.ServiceContexts.Add(serviceContext);

        builder.Services.AddHttpClient(serviceContext.ServiceName, x =>
        {
            var configureHttpClient = builder.HttpClientConfiguration;
            if (configureHttpClient is not null) configureHttpClient(x);
        });

        return builder;
    }
}
