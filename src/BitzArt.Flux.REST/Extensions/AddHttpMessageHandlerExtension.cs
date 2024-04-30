namespace BitzArt.Flux;

public static class AddHttpMessageHandlerExtension
{
    public static IFluxRestServiceBuilder AddHttpMessageHandler(this IFluxRestServiceBuilder builder, Func<IServiceProvider, DelegatingHandler> configureHandler)
    {
        builder.ConfigureHandler = configureHandler;

        return builder;
    }
}
