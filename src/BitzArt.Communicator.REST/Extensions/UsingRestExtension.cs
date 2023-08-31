using BitzArt.Communicator;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt;

public static class UsingRestExtension
{
    public static ICommunicatorRestServiceBuilder UsingRest(this ICommunicatorServicePreBuilder prebuilder, string? baseUrl = null)
    {
        var builder = new CommunicatorRestServiceBuilder(prebuilder, baseUrl);

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
