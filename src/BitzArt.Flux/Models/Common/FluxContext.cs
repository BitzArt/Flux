using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxContext(IServiceProvider serviceProvider) : IFluxContext
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IFluxServiceContext Service(string serviceName)
        => _serviceProvider.GetRequiredKeyedService<IFluxServiceContext>(new FluxServiceSignature(serviceName));

    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? serviceName = null, string? setName = null)
        where TModel : class
        where TKey : notnull
        => _serviceProvider.GetRequiredKeyedService<IFluxSetContext<TModel, TKey>>(new FluxSetSignature(serviceName, setName));

    public IFluxSetContext<TModel> Set<TModel>(string? serviceName = null, string? setName = null)
        where TModel : class
        => _serviceProvider.GetRequiredKeyedService<IFluxSetContext<TModel>>(new FluxSetSignature(serviceName, setName));
}
