using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddEntityExtension
{
    public static IFluxRestEntityBuilder<TEntity> AddEntity<TEntity>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TEntity : class
    {
        var builder = new FluxRestEntityBuilder<TEntity>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceContext = builder.ServiceContext;

        serviceContext.AddEntity<TEntity>(builder.EntityOptions);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxProvider>();
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
        var serviceContext = serviceBuilder.ServiceContext;

        serviceContext.AddEntity<TEntity, TKey>(builder.EntityOptions);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxProvider>();
            return factory.GetEntityContext<TEntity, TKey>(x, serviceContext.ServiceName);
        });

        services.AddScoped<IFluxEntityContext<TEntity>>(x =>
        {
            var factory = x.GetRequiredService<IFluxProvider>();
            return factory.GetEntityContext<TEntity, TKey>(x, serviceContext.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }
}
