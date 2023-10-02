namespace BitzArt.Flux;

internal class FluxServiceContext : IFluxServiceContext
{
    internal readonly IFluxServiceFactory Provider;
    private readonly IServiceProvider _serviceProvider;

    public FluxServiceContext(IFluxServiceFactory provider, IServiceProvider serviceProvider)
    {
        Provider = provider;
        _serviceProvider = serviceProvider;
    }

    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>()
        where TModel : class
        => Provider.CreateSetContext<TModel, TKey>(_serviceProvider);

    public IFluxSetContext<TModel> Set<TModel>()
        where TModel : class
        => Provider.CreateSetContext<TModel>(_serviceProvider);
}
