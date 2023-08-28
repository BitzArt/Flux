using System.Text.Json;

namespace BitzArt.Communicator;

public static class RestCommunicatorServiceBuilderExtensions
{
    public static IRestCommunicatorServiceBuilder ConfigureJson(this IRestCommunicatorServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure(builder.Options.SerializerOptions);

        return builder;
    }

    public static IRestCommunicatorServiceBuilder ConfigureHttpClient(this IRestCommunicatorServiceBuilder builder, Action<HttpClient> configure)
    {
        builder.HttpClientConfiguration = configure;

        return builder;
    }
}
