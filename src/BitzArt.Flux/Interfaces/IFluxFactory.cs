namespace BitzArt.Flux;

/// <summary>
/// Internal Flux factory. Stores factories for configured services.
/// </summary>
public interface IFluxFactory
{
    /// <summary>
    /// Registered service contexts.
    /// </summary>
    public ICollection<IFluxServiceFactory> ServiceContexts { get; }

    /// <summary>
    /// Returns a service provider for a specific service name.
    /// </summary>
    public IFluxServiceFactory GetServiceProvider(string name);

    /// <summary>
    /// Resolves a context for a specific Flux Service.
    /// </summary>
    public IFluxSetContext<TModel> GetSetContext<TModel>(IServiceProvider services, string? serviceName = null, string? setName = null)
        where TModel : class;

    /// <summary>
    /// Resolves a context for a specific Flux Service.
    /// </summary>
    public IFluxSetContext<TModel, TKey> GetSetContext<TModel, TKey>(IServiceProvider services, string? serviceName = null, string? setName = null)
        where TModel : class;
}