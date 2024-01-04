using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class UsingJsonExtension
{
    public static IFluxJsonServiceBuilder UsingJson(this IFluxServicePreBuilder prebuilder, string? basePath = null)
    {
        var builder = new FluxJsonServiceBuilder(prebuilder, basePath);

        var fluxServiceProvider = builder.ServiceFactory;
        builder.Factory.ServiceContexts.Add(fluxServiceProvider);

        builder.Services.AddScoped<IFluxServiceContext>(x =>
        {
            return new FluxServiceContext(fluxServiceProvider, x);
        });
        
        return builder;
    }
}