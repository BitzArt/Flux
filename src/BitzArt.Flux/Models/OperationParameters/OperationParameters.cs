namespace BitzArt.Flux;

/// <summary>
/// Can be used as a simple way of passing a simple list of parameters to a Flux operation.
/// <para>
/// Such collection will be resolved by the Flux engine by passing them to the operation in their respective order.
/// </para>
/// <para>
/// Consider using <see cref="NamedOperationParameters"/> if you wish to pass named parameters.
/// </para>
/// </summary>
public class OperationParameters : IOperationParameterCollection
{
    private readonly List<object> _parameters;

    IEnumerable<object> IOperationParameterCollection.Values => _parameters;

    internal PrimaryOperationValues PrimaryValues { get; private set; }

    /// <inheritdoc cref="OperationParameters(IEnumerable{object})"/>"
    public OperationParameters(params object[] parameters) : this((IEnumerable<object>)parameters) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationParameters"/> class.
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public OperationParameters(IEnumerable<object> parameters)
    {
        _parameters = [.. parameters];

        PrimaryValues = new();
    }
}
