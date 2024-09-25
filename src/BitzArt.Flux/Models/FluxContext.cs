namespace BitzArt.Flux;

internal class FluxContext(IFluxFactory factory, IServiceProvider serviceProvider)
    : IFluxContext
{
    public IFluxServiceContext Service(string serviceName)
        => new FluxServiceContext(factory.GetServiceProvider(serviceName), serviceProvider);

    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? service = null, string? set = null)
        where TModel : class
        => factory.GetSetContext<TModel, TKey>(serviceProvider, service, set);

    public IFluxSetContext<TModel> Set<TModel>(string? service = null, string? set = null)
        where TModel : class
        => factory.GetSetContext<TModel>(serviceProvider, service, set);
}
