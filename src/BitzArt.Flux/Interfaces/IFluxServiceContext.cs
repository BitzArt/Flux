namespace BitzArt.Flux;

/// <summary>
/// Flux context for a specific configured Service.
/// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information.
/// </summary>
public interface IFluxServiceContext
{
    /// <summary>
    /// Resolves a context for a specific preconfigured Flux Set.
    /// </summary>
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? name = null) where TModel : class;

    /// <inheritdoc cref="Set{TModel, TKey}(string?)"/>
    public IFluxSetContext<TModel> Set<TModel>(string? name = null) where TModel : class;
}