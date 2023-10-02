using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddSetExtension
{
    public static IFluxRestSetBuilder<TModel> AddSet<TModel>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TModel : class
    {
        var builder = new FluxRestSetBuilder<TModel>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceContext = builder.ServiceFactory;

        serviceContext.AddSet<TModel>(builder.SetOptions);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxFactory>();
            return factory.GetSetContext<TModel>(x, serviceContext.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TModel : class
    {
        var builder = new FluxRestSetBuilder<TModel, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceContext = serviceBuilder.ServiceFactory;

        serviceContext.AddSet<TModel, TKey>(builder.SetOptions);

        services.AddScoped(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetSetContext<TModel, TKey>(x, serviceContext.ServiceName);
        });

        services.AddScoped<IFluxSetContext<TModel>>(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetSetContext<TModel, TKey>(x, serviceContext.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }
}
