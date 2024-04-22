namespace BitzArt.Flux;

public static class ConfigureHttpClientExtension
{
    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<HttpClient> configure)
    {
        builder.HttpClientConfiguration = (_, client) => configure(client);

        return builder;
    }

    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<IServiceProvider, HttpClient> configure)
    {
        builder.HttpClientConfiguration = configure;

        return builder;
    }
}
