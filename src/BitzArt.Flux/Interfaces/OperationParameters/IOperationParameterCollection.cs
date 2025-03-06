namespace BitzArt.Flux;

/// <summary>
/// A collection of parameters to be used in a Flux operation. <br/>
/// Such collection will be resolved by the Flux engine by passing them to the operation in their respective order.
/// </summary>
public interface IOperationParameterCollection
{
    /// <summary>
    /// Parameter values.
    /// </summary>
    public IEnumerable<object> Values { get; }
}
