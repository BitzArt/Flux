namespace BitzArt.Flux;

/// <summary>
/// Flux context for a specific configured Service.
/// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information.
/// </summary>
public interface IFluxServiceContext
{
    /// <summary>
    /// Name of the service this context is for.
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// Resolves a context for a specific preconfigured Flux Set.
    /// </summary>
    /// <typeparam name="TModel">Type of the model.</typeparam>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <param name="setKey">Key of the set to resolve.</param>
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(object? setKey = null)
        where TModel : class
        where TKey : notnull;

    /// <inheritdoc cref="Set{TModel, TKey}(object?)"/>
    public IFluxSetContext<TModel> Set<TModel>(object? setKey = null) where TModel : class;
}