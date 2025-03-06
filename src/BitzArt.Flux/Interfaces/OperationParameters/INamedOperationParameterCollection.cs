namespace BitzArt.Flux;

/// <summary>
/// A collection of named parameters to be used in a Flux operation. <br />
/// Such collection will be resolved by the Flux engine by passing them to the operation by their respective names.
/// </summary>
public interface INamedOperationParameterCollection : IOperationParameterCollection
{
    /// <summary>
    /// Named parameter values.
    /// </summary>
    public new IDictionary<string, object> Values { get; }
}
