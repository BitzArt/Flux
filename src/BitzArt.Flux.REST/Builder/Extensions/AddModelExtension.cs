using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddModelExtension
{
    public static IFluxRestModelBuilder<TModel> AddModel<TModel>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TModel : class
    {
        var builder = new FluxRestModelBuilder<TModel>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceContext = builder.ServiceFactory;

        serviceContext.AddModel<TModel>(builder.ModelOptions);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxFactory>();
            return factory.GetModelContext<TModel>(x, serviceContext.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }

    public static IFluxRestModelBuilder<TModel, TKey> AddModel<TModel, TKey>(this IFluxRestServiceBuilder serviceBuilder, string? endpoint = null)
        where TModel : class
    {
        var builder = new FluxRestModelBuilder<TModel, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceContext = serviceBuilder.ServiceFactory;

        serviceContext.AddModel<TModel, TKey>(builder.ModelOptions);

        services.AddScoped(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetModelContext<TModel, TKey>(x, serviceContext.ServiceName);
        });

        services.AddScoped<IFluxModelContext<TModel>>(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetModelContext<TModel, TKey>(x, serviceContext.ServiceName);
        });

        if (endpoint is not null) return builder.WithEndpoint(endpoint);

        return builder;
    }
}
