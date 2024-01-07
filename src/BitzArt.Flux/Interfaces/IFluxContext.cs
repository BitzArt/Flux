namespace BitzArt.Flux;

/// <summary>
/// Allows access to configured Services and their Sets. <br/>
/// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information.
/// </summary>
public interface IFluxContext
{
    /// <summary>
    /// Returns a context for a specific Service. <br/>
    /// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information.
    /// </summary>
    public IFluxServiceContext Service(string serviceName);

    /// <summary>
    /// Returns a context for a specific preconfigured Flux Set with the Service resolved implicitly. <br/>
    /// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information.
    /// </summary>
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? service = null, string? set = null) where TModel : class;
    
    public IFluxSetContext<TModel> Set<TModel>(string? service = null, string? set = null) where TModel : class;
}