namespace BitzArt.Flux;

/// <summary>
/// Internal Flux factory. Stores factories for configured services.
/// </summary>
public interface IFluxFactory
{
    public ICollection<IFluxServiceFactory> ServiceContexts { get; }

    public IFluxServiceFactory GetServiceProvider(string name);

    public IFluxSetContext<TModel> GetSetContext<TModel>(IServiceProvider services, string? serviceName = null, string? setName = null)
        where TModel : class;

    public IFluxSetContext<TModel, TKey> GetSetContext<TModel, TKey>(IServiceProvider services, string? serviceName = null, string? setName = null)
        where TModel : class;
}