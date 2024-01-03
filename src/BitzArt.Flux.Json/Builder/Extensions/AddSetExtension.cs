using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddSetExtension
{
    public static IFluxJsonSetBuilder<TModel> AddSet<TModel>(this IFluxJsonServiceBuilder serviceBuilder, string filePath, string? name = null)
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

        builder.SetOptions.FilePath = filePath;

        return builder;
    }
}