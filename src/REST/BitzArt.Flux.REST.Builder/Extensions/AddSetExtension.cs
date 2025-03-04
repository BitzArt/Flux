using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for adding REST sets to <see cref="IFluxRestServiceBuilder"/>
/// </summary>
public static class AddSetExtension
{
    /// <inheritdoc cref="AddSet{TModel,TKey}(IFluxRestServiceBuilder,string,string)"/>/>
    public static IFluxRestSetBuilder<TModel, object> AddSet<TModel>(this IFluxRestServiceBuilder serviceBuilder, string? path = null, string? name = null)
        where TModel : class
        => AddSet<TModel, object>(serviceBuilder, path, name);

    /// <summary>
    /// Adds a REST set to the <see cref="IFluxRestServiceBuilder"/>
    /// </summary>
    public static IFluxRestSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxRestServiceBuilder serviceBuilder, string? path = null, string? name = null)
        where TModel : class
        where TKey : notnull
    {
        var builder = new FluxRestSetBuilder<TModel, TKey>(serviceBuilder, path);

        var services = serviceBuilder.ServiceCollection;
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
