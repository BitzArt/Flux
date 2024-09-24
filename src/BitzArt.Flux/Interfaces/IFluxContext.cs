namespace BitzArt.Flux;

/// <summary>
/// Allows access to configured Services and their Sets. <br/>
/// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information.
/// </summary>
public interface IFluxContext
{
    /// <summary>
    /// Resolves a context for a specific Flux Service. <br/>
    /// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information
    /// on how to use Flux Services.
    /// </summary>
    public IFluxServiceContext Service(string serviceName);

    /// <summary>
    /// Resolves a context for a specific preconfigured Flux Set. <br/>
    /// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information
    /// on how to use Flux Sets.
    /// </summary>
    public IFluxSetContext<TModel> Set<TModel>(string? service = null, string? set = null) where TModel : class;

    /// <summary>
    /// Resolves a context for a specific preconfigured Flux Set. <br/>
    /// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information
    /// on how to use Flux Sets.
    /// </summary>
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? service = null, string? set = null) where TModel : class;
}