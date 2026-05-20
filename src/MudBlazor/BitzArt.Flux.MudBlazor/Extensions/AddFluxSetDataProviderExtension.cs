using BitzArt.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.MudBlazor;

/// <summary>
/// Extension methods for adding <see cref="IFluxSetDataProvider{TModel}"/> to the service collection.
/// </summary>
public static class AddFluxSetDataProviderExtension
{
    /// <summary>
    /// Adds an <see cref="IFluxSetDataProvider{TModel}"/> to the service collection.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the provider to</param>
    /// <param name="setContextImplementationFactory">Can be used to provide a non-default <see cref="IFluxSetContext{TModel}"/></param>
    /// <returns> <see cref="IServiceCollection"/> to allow chaining </returns>
    public static IServiceCollection AddFluxSetDataProvider<TModel>(
        this IServiceCollection services,
        Func<IServiceProvider, IFluxSetContext<TModel>>? setContextImplementationFactory = null)
        where TModel : class
    {
        services.AddFluxSetDataProvider<PageResult<TModel, PageRequest>, TModel>(setContextImplementationFactory);
        return services;
    }

    /// <summary>
    /// Adds an <see cref="IFluxSetDataProvider{TRequest, TModel}"/> to the service collection.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the provider to</param>
    /// <param name="setContextImplementationFactory">Can be used to provide a non-default <see cref="IFluxSetContext{TModel}"/></param>
    /// <returns> <see cref="IServiceCollection"/> to allow chaining </returns>
    public static IServiceCollection AddFluxSetDataProvider<TRequest, TModel>(
        this IServiceCollection services,
        Func<IServiceProvider, IFluxSetContext<TModel>>? setContextImplementationFactory = null)
        where TRequest : class
        where TModel : class
    {
        setContextImplementationFactory ??= (serviceProvider)
            => serviceProvider.GetRequiredService<IFluxSetContext<TModel>>();

        services.AddTransient<FluxSetDataProvider<TRequest, TModel>>();

        services.AddTransient<IFluxSetDataProvider<TRequest, TModel>>(serviceProvider =>
        {
            var provider = serviceProvider.GetRequiredService<FluxSetDataProvider<TRequest, TModel>>();
            var setContext = setContextImplementationFactory!.Invoke(serviceProvider);
            provider.SetContext = setContext;

            return provider;
        });

        return services;
    }
}
