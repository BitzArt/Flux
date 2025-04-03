namespace BitzArt.Flux;

/// <summary>
/// <para>
/// This interface allows for custom implementations of parameter collections. <br />
/// For a default implementation, see <see cref="OperationParameterCollection"/>.
/// </para>
/// <para>
/// A collection of named parameters to be used in a Flux operation. <br />
/// Such collection will be resolved by the Flux engine by passing them to the operation by their respective names.
/// </para>
/// <para>
/// This interface overrides the default behavior of <see cref="IOperationParameterCollection"/>
/// by providing a dictionary of named parameters, instead of a simple list of values.
/// </para>
/// <para>
/// When implementing this interface,
/// it is not necessary to implement <see cref="IOperationParameterCollection.Values"/> property -
/// Flux engine will use <see cref="INamedOperationParameterCollection.Values"/> instead.
/// </para>
/// </summary>
public interface INamedOperationParameterCollection : IOperationParameterCollection
{
    /// <summary>
    /// Named parameter key-value pairs.
    /// </summary>
    public new IDictionary<string, object> Values { get; }
}
