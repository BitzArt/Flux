using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxServiceContext(IServiceProvider serviceProvider, string name) : IFluxServiceContext
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly string _serviceName = name;

    string IFluxServiceContext.ServiceName => _serviceName;

    IFluxSetContext<TModel, TKey> IFluxServiceContext.Set<TModel, TKey>(string? setName)
        => _serviceProvider.GetRequiredKeyedService<IFluxSetContext<TModel, TKey>>(
            new FluxSetSignature(
                serviceName: _serviceName,
                setName: setName));

    IFluxSetContext<TModel> IFluxServiceContext.Set<TModel>(string? setName)
        => _serviceProvider.GetRequiredKeyedService<IFluxSetContext<TModel>>(
            new FluxSetSignature(_serviceName, setName));
}
