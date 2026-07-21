using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxServiceContext(IServiceProvider serviceProvider, string name) : IFluxServiceContext
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly string _serviceName = name;

    string IFluxServiceContext.ServiceName => _serviceName;

    IFluxSetContext<TModel, TKey> IFluxServiceContext.Set<TModel, TKey>(object? setKey)
        => _serviceProvider.GetRequiredKeyedService<IFluxSetContext<TModel, TKey>>(
            new FluxSetSignature(
                serviceName: _serviceName,
                setKey: setKey));

    IFluxSetContext<TModel> IFluxServiceContext.Set<TModel>(object? setKey)
        => _serviceProvider.GetRequiredKeyedService<IFluxSetContext<TModel>>(
            new FluxSetSignature(_serviceName, setKey));
}
