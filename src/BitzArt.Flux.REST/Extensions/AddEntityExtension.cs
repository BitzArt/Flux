using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddEntityExtension
{
    public static IFluxRestEntityBuilder<TEntity> AddEntity<TEntity>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TEntity : class
    {
        var builder = new FluxRestEntityBuilder<TEntity>(serviceBuilder);

        var services = builder.Services;
        var provider = builder.Provider;

        provider.AddSignature(new(typeof(TEntity)));

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxServiceFactory>();
            return factory.GetEntityContext<TEntity>(x, builder.EntityOptions);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> AddEntity<TEntity, TKey>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TEntity : class
    {
        var builder = new FluxRestEntityBuilder<TEntity, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var provider = serviceBuilder.Provider;

        provider.AddSignature(new(typeof(TEntity), typeof(TKey)));

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxServiceFactory>();
            return factory.GetEntityContext<TEntity, TKey>(x, builder.EntityOptions);
        });

        services.AddScoped<IFluxEntityContext<TEntity>>(x =>
        {
            var factory = x.GetRequiredService<IFluxServiceFactory>();
            return factory.GetEntityContext<TEntity, TKey>(x, builder.EntityOptions);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }
}
