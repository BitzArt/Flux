namespace BitzArt.Flux.Rest;

/// <summary>
/// Extension methods for configuring the HttpClient in <see cref="IFluxRestServiceBuilder"/>
/// </summary>
public static class ConfigureHttpClientExtension
{
    /// <inheritdoc cref="ConfigureHttpClient(IFluxRestServiceBuilder,Action{IServiceProvider,HttpClient})"/>
    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<HttpClient> configure)
        => ConfigureHttpClient(builder, (_, httpClient) => configure.Invoke(httpClient));

    /// <summary>
    /// Configures the HttpClient for the <see cref="IFluxRestServiceBuilder"/>
    /// </summary>
    /// <param name="builder">The <see cref="IFluxRestServiceBuilder"/> to configure <see cref="HttpClient"/> for.</param>
    /// <param name="configure"><see cref="HttpClient"/> configuration action.</param>
    public static IFluxRestServiceBuilder ConfigureHttpClient(this IFluxRestServiceBuilder builder, Action<IServiceProvider, HttpClient> configure)
    {
        builder.ServiceConfiguration.HttpClientConfiguration = configure;

        return builder;
    }
}
