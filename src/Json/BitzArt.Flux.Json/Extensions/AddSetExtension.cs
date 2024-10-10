using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for adding a set to the <see cref="IFluxJsonServiceBuilder"/>.
/// </summary>
public static class AddSetExtension
{
    /// <inheritdoc cref="AddSet{TModel,TKey}(IFluxJsonServiceBuilder,string)"/>/>
    public static IFluxJsonSetBuilder<TModel, object> AddSet<TModel>(this IFluxJsonServiceBuilder serviceBuilder,
        string? name = null)
        where TModel : class
        => AddSet<TModel, object>(serviceBuilder, name);

    /// <summary>
    /// Adds a set to the <see cref="IFluxJsonServiceBuilder"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// The key type of the set.
    /// </typeparam>
    /// <param name="serviceBuilder"></param>
    /// <param name="name">
    /// The optional name of the set.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel,TKey}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxJsonServiceBuilder serviceBuilder,
        string? name = null)
        where TModel : class
    {
        var builder = new FluxJsonSetBuilder<TModel, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceFactory = serviceBuilder.ServiceFactory;

        serviceFactory.AddSet<TModel, TKey>(builder.SetOptions, name);

        services.AddScoped(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetSetContext<TModel, TKey>(x, serviceFactory.ServiceName);
        });

        services.AddScoped<IFluxSetContext<TModel>>(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetSetContext<TModel, TKey>(x, serviceFactory.ServiceName);
        });

        return builder;
    }
}