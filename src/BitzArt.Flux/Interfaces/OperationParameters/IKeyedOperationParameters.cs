namespace BitzArt.Flux;

/// <summary>
/// Flux parameters for a keyed operation.
/// </summary>
public interface IKeyedOperationParameters
{
    /// <summary>
    /// Entity Id.
    /// </summary>
    public object Id { get; set; }
}
