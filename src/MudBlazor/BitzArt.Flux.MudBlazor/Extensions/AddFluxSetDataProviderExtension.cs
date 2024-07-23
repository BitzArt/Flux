using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.MudBlazor;

public static class AddFluxSetDataProviderExtension
{
    public static IServiceCollection AddFluxSetDataProvider<TModel>(
        this IServiceCollection services,
        Func<IServiceProvider, IFluxSetContext<TModel>>? setContextImplementationFactory = null)
        where TModel : class
    {
        if (setContextImplementationFactory is null)
        {
            setContextImplementationFactory = (serviceProvider)
                => serviceProvider.GetRequiredService<IFluxSetContext<TModel>>();
        }

        services.AddScoped<FluxSetDataProvider<TModel>>();

        services.AddScoped<IFluxSetDataProvider<TModel>>(serviceProvider =>
        {
            var provider = serviceProvider.GetRequiredService<FluxSetDataProvider<TModel>>();
            var setContext = setContextImplementationFactory!.Invoke(serviceProvider);
            provider.SetContext = setContext;

            return provider;
        });

        return services;
    }
}
