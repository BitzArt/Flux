using System.Text.Json;

namespace BitzArt.Flux;

public static class IFluxRestServiceBuilderExtensions
{
    public static IFluxRestServiceBuilder ConfigureJson(this IFluxRestServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure(builder.ServiceOptions.SerializerOptions);

        return builder;
    }

    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<HttpClient> configure)
    {
        builder.HttpClientConfiguration = configure;

        return builder;
    }
}
