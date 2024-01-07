namespace BitzArt.Flux;

public static class ConfigureHttpClientExtension
{
    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<HttpClient> configure)
    {
        builder.HttpClientConfiguration = configure;

        return builder;
    }
}
