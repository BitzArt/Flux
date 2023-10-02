using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddSetExtension
{
    public static IFluxRestSetBuilder<TModel> AddSet<TModel>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null, string? name = null)
        where TModel : class
    {
        var builder = new FluxRestSetBuilder<TModel>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceFactory = builder.ServiceFactory;

        serviceFactory.AddSet<TModel>(builder.SetOptions, name);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxFactory>();
            return factory.GetSetContext<TModel>(x, serviceFactory.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null, string? name = null)
        where TModel : class
    {
        var builder = new FluxRestSetBuilder<TModel, TKey>(serviceBuilder);

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

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }
}
