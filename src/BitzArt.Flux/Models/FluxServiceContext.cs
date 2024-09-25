namespace BitzArt.Flux;

/// <summary>
/// A context for a Flux service.
/// </summary>
public class FluxServiceContext : IFluxServiceContext
{
    internal readonly IFluxServiceFactory Provider;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxServiceContext"/> class.
    /// </summary>
    public FluxServiceContext(IFluxServiceFactory provider, IServiceProvider serviceProvider)
    {
        Provider = provider;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Resolves a context for a specific preconfigured Flux Set within the service.
    /// </summary>
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? name = null)
        where TModel : class
        => Provider.CreateSetContext<TModel, TKey>(_serviceProvider, name);

    /// <inheritdoc cref="Set{TModel, TKey}(string?)"/>"
    public IFluxSetContext<TModel> Set<TModel>(string? name = null)
        where TModel : class
        => Provider.CreateSetContext<TModel>(_serviceProvider, name);
}
