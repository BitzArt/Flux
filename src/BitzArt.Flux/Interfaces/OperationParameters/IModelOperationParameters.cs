namespace BitzArt.Flux;

/// <summary>
/// Flux parameters for an operation that requires a model (e.g. HTTP POST, PUT, PATCH).
/// </summary>
public interface IModelOperationParameters
{
    /// <summary>
    /// The model to be used in the operation.
    /// </summary>
    public object Model { get; }
}
