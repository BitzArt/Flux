using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddSetExtension
{
    public static IFluxJsonSetBuilder<TModel> AddSet<TModel>(this IFluxJsonServiceBuilder serviceBuilder,
        string? name = null)
        where TModel : class
    {
        var builder = new FluxJsonSetBuilder<TModel>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceFactory = builder.ServiceFactory;

        serviceFactory.AddSet<TModel>(builder.SetOptions, name);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxFactory>();
            return factory.GetSetContext<TModel>(x, serviceFactory.ServiceName);
        });

        return builder;
    }

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