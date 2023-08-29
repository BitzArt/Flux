using BitzArt.Communicator;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt;

public static class AddEntityExtension
{
    public static ICommunicatorRestEntityBuilder AddEntity<TEntity>(this ICommunicatorRestServiceBuilder builder, string? endpoint = null)
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

        return new CommunicatorRestEntityBuilder(builder);
    }

    public static ICommunicatorRestServiceBuilder AddEntity<TEntity, TKey>(this ICommunicatorRestServiceBuilder builder, string? endpoint = null)
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

        services.AddScoped<IEntityContext<TEntity>>(x =>
        {
            var factory = x.GetRequiredService<ICommunicatorServiceFactory>();
            return factory.GetEntityCommunicator<TEntity, TKey>(x, endpoint);
        });

        return builder;
    }
}
