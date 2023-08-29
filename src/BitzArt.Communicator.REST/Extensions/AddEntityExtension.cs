using BitzArt.Communicator;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BitzArt;

public static class AddEntityExtension
{
    public static ICommunicatorRestEntityBuilder<TEntity> AddEntity<TEntity>(this ICommunicatorRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TEntity : class
    {
        var builder = new CommunicatorRestEntityBuilder<TEntity>(serviceBuilder);

        var services = builder.Services;
        var provider = builder.Provider;

        provider.AddSignature(new(typeof(TEntity)));

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<ICommunicatorServiceFactory>();
            return factory.GetEntityCommunicator<TEntity>(x, builder.EntityOptions);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }

    public static ICommunicatorRestServiceBuilder AddEntity<TEntity, TKey>(this ICommunicatorRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TEntity : class
    {
        var builder = new CommunicatorRestEntityBuilder<TEntity, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var provider = serviceBuilder.Provider;

        provider.AddSignature(new(typeof(TEntity), typeof(TKey)));

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<ICommunicatorServiceFactory>();
            return factory.GetEntityCommunicator<TEntity, TKey>(x, builder.EntityOptions);
        });

        services.AddScoped<ICommunicationContext<TEntity>>(x =>
        {
            var factory = x.GetRequiredService<ICommunicatorServiceFactory>();
            return factory.GetEntityCommunicator<TEntity, TKey>(x, builder.EntityOptions);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }
}
