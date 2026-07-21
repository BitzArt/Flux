namespace BitzArt.Flux;

/// <summary>
/// <para>
/// This interface allows for custom implementations of parameter collections. <br />
/// For a default implementation, see <see cref="OperationParameterCollection"/>.
/// </para>
/// <para>
/// A collection of parameters to be used in a Flux operation. <br/>
/// Such collection will be resolved by the Flux engine by passing them to the operation in their respective order.
/// </para>
/// <para>
/// For a collection of named parameters, see <see cref="INamedOperationParameterCollection"/>.
/// </para>
/// </summary>
public interface IOperationParameterCollection
{
    /// <summary>
    /// Parameter values.
    /// </summary>
    public IEnumerable<object> Values { get; }
}
