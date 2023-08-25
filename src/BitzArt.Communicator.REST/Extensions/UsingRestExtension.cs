using BitzArt.Communicator;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt;

public static class UsingRestExtension
{
    public static IRestCommunicatorServiceBuilder UsingRest(this ICommunicatorServicePreBuilder prebuilder, string baseUrl)
    {
        var builder = new RestCommunicatorServiceBuilder(prebuilder, baseUrl);

        var provider = builder.Provider;
        builder.Factory.Providers.Add(provider);

        builder.Services.AddHttpClient(provider.ServiceName, x =>
        {
            x.BaseAddress = new Uri(baseUrl);
        });

        builder.Services.AddSingleton(provider);

        return builder;
    }
}
