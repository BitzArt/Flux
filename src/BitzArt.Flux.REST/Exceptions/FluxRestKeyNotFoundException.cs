namespace BitzArt.Flux;

/// <summary>
/// Exception thrown when a key is not found for a given model type.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class FluxRestKeyNotFoundException<TModel> : Exception
{
    private static readonly string Msg = $"Unable to find TKey for type '{typeof(TModel).Name}'. Consider specifying a key when registering the set.";
    
    /// <summary>
    /// Initializes a new instance of the <see cref="FluxRestKeyNotFoundException{TModel}"/> class.
    /// </summary>
    public FluxRestKeyNotFoundException() : base(Msg) { }
}
