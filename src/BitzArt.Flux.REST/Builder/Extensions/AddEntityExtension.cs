using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddEntityExtension
{
    public static IFluxRestEntityBuilder<TEntity> AddEntity<TEntity>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TEntity : class
    {
        var builder = new FluxRestEntityBuilder<TEntity>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceContext = builder.ServiceFactory;

        serviceContext.AddEntity<TEntity>(builder.EntityOptions);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxFactory>();
            return factory.GetEntityContext<TEntity>(x, serviceContext.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> AddEntity<TEntity, TKey>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TEntity : class
    {
        var builder = new FluxRestEntityBuilder<TEntity, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceContext = serviceBuilder.ServiceFactory;

        serviceContext.AddEntity<TEntity, TKey>(builder.EntityOptions);

        services.AddScoped(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetEntityContext<TEntity, TKey>(x, serviceContext.ServiceName);
        });

        services.AddScoped<IFluxEntityContext<TEntity>>(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetEntityContext<TEntity, TKey>(x, serviceContext.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }
}
