using System.Text.Json;

namespace Flex;

public static class ICommunicatorRestServiceBuilderExtensions
{
    public static ICommunicatorRestServiceBuilder ConfigureJson(this ICommunicatorRestServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure(builder.ServiceOptions.SerializerOptions);

        return builder;
    }

    public static ICommunicatorRestServiceBuilder ConfigureHttpClient(this ICommunicatorRestServiceBuilder builder, Action<HttpClient> configure)
    {
        builder.HttpClientConfiguration = configure;

        return builder;
    }
}
