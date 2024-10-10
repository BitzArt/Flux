namespace BitzArt.Flux;

/// <summary>
/// A context for a Flux service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FluxServiceContext"/> class.
/// </remarks>
public class FluxServiceContext(IFluxServiceFactory provider, IServiceProvider serviceProvider)
    : IFluxServiceContext
{
    internal readonly IFluxServiceFactory Provider = provider;

    /// <summary>
    /// Resolves a context for a specific preconfigured Flux Set within the service.
    /// </summary>
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? name = null)
        where TModel : class
        => Provider.CreateSetContext<TModel, TKey>(serviceProvider, name);

    /// <inheritdoc cref="Set{TModel, TKey}(string?)"/>"
    public IFluxSetContext<TModel> Set<TModel>(string? name = null)
        where TModel : class
        => Provider.CreateSetContext<TModel>(serviceProvider, name);
}
