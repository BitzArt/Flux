namespace BitzArt.Flux;

/// <summary>
/// Internal Flux factory. Stores factories for configured services.
/// </summary>
public interface IFluxFactory
{
    internal ICollection<IFluxServiceFactory> ServiceContexts { get; }

    internal IFluxServiceFactory GetServiceProvider(string name);

    internal IFluxSetContext<TModel> GetSetContext<TModel>(IServiceProvider services, string? serviceName = null, string? setName = null)
        where TModel : class;

    internal IFluxSetContext<TModel, TKey> GetSetContext<TModel, TKey>(IServiceProvider services, string? serviceName = null, string? setName = null)
        where TModel : class;
}