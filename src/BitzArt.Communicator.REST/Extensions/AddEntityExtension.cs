using BitzArt.Communicator;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt;

public static class AddEntityExtension
{
    public static IRestCommunicatorServiceBuilder AddEntity<TEntity>(this IRestCommunicatorServiceBuilder builder, string? endpoint = null)
        where TEntity : class
    {
        var services = builder.Services;
        var provider = builder.Provider;

        provider.AddSignature(new(typeof(TEntity)));

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<ICommunicatorServiceFactory>();
            return factory.GetEntityCommunicator<TEntity>(x, endpoint);
        });

        return builder;
    }

    public static IRestCommunicatorServiceBuilder AddEntity<TEntity, TKey>(this IRestCommunicatorServiceBuilder builder, string? endpoint = null)
        where TEntity : class
    {
        var services = builder.Services;
        var provider = builder.Provider;

        provider.AddSignature(new(typeof(TEntity), typeof(TKey)));

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<ICommunicatorServiceFactory>();
            return factory.GetEntityCommunicator<TEntity, TKey>(x, endpoint);
        });

        services.AddScoped<IEntityCommunicator<TEntity>>(x =>
        {
            var factory = x.GetRequiredService<ICommunicatorServiceFactory>();
            return factory.GetEntityCommunicator<TEntity, TKey>(x, endpoint);
        });

        return builder;
    }
}
