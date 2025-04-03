namespace BitzArt.Flux;

/// <summary>
/// A context for a Flux service.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FluxServiceContext"/> class.
/// </remarks>
public class FluxServiceContext(IFluxServiceRegistration serviceRegistration, IServiceProvider serviceProvider)
    : IFluxServiceContext
{
    internal readonly IFluxServiceRegistration ServiceRegistration = serviceRegistration;

    /// <summary>
    /// Resolves a context for a specific preconfigured set within a service.
    /// </summary>
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? name = null)
        where TModel : class
        where TKey : notnull
        => ServiceRegistration.CreateSetContext<TModel, TKey>(serviceProvider, name);

    /// <inheritdoc cref="Set{TModel, TKey}(string?)"/>"
    public IFluxSetContext<TModel> Set<TModel>(string? name = null)
        where TModel : class
        => ServiceRegistration.CreateSetContext<TModel>(serviceProvider, name);
}
