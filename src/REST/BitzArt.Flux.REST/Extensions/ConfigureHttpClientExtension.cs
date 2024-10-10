namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the HttpClient in <see cref="IFluxRestServiceBuilder"/>
/// </summary>
public static class ConfigureHttpClientExtension
{
    /// <summary>
    /// Configures the HttpClient for the <see cref="IFluxRestServiceBuilder"/>
    /// </summary>
    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<HttpClient> configure)
    {
        builder.HttpClientConfiguration = (_, client) => configure(client);

        return builder;
    }

    /// <inheritdoc cref="ConfigureHttpClient(IFluxRestServiceBuilder,Action{HttpClient})"/>
    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<IServiceProvider, HttpClient> configure)
    {
        builder.HttpClientConfiguration = configure;

        return builder;
    }
}
