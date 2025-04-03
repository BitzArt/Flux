namespace BitzArt.Flux;

/// <summary>
/// Holds registration of a specific Flux service.
/// Can create new instances of service context based on the configuration.
/// </summary>
public interface IFluxServiceRegistration
{
    /// <summary>
    /// Name of the service.
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// Create a new Flux context for a preconfigured data Set.
    /// </summary>
    public IFluxSetContext<TModel> CreateSetContext<TModel>(IServiceProvider services, string? name = null) where TModel : class;

    /// <inheritdoc cref="CreateSetContext{TModel}(IServiceProvider, string?)"/>
    public IFluxSetContext<TModel, TKey> CreateSetContext<TModel, TKey>(IServiceProvider services, string? name = null)
        where TModel : class
        where TKey : notnull;

    /// <summary>
    /// Register a new data Set.
    /// </summary>
    public void AddSet<TModel>(object options, string? name) where TModel : class;

    /// <inheritdoc cref="AddSet{TModel}(object, string?)"/>
    public void AddSet<TModel, TKey>(object options, string? name) where TModel : class;

    /// <summary>
    /// Check if a specific data Set is registered.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the data Set is registered; otherwise, <c>false</c>.
    /// </returns>
    public bool ContainsSignature<TModel>(string? setName);

    /// <inheritdoc cref="ContainsSignature{TModel}(string?)"/>/>
    public bool ContainsSignature<TModel, TKey>(string? setName);
}