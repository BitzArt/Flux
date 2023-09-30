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

    public IFluxModelContext<TModel, TKey> Model<TModel, TKey>()
        where TModel : class
        => Provider.CreateModelContext<TModel, TKey>(_serviceProvider);

    public IFluxModelContext<TModel> Model<TModel>()
        where TModel : class
        => Provider.CreateModelContext<TModel>(_serviceProvider);
}
