namespace BitzArt.Flux;

internal class FluxContext : IFluxContext
{
    private readonly IFluxFactory _factory;
    private readonly IServiceProvider _serviceProvider;

    public FluxContext(IFluxFactory factory, IServiceProvider serviceProvider)
    {
        _factory = factory;
        _serviceProvider = serviceProvider;
    }

    public IFluxServiceContext Service(string serviceName)
        => new FluxServiceContext(_factory.GetServiceProvider(serviceName), _serviceProvider);

    public IFluxModelContext<TModel, TKey> Model<TModel, TKey>(string? serviceName = null)
        where TModel : class
        => _factory.GetModelContext<TModel, TKey>(_serviceProvider, serviceName);

    public IFluxModelContext<TModel> Model<TModel>(string? serviceName = null)
        where TModel : class
        => _factory.GetModelContext<TModel>(_serviceProvider, serviceName);
}
