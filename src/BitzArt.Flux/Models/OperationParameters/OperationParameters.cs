using System.Collections;

namespace BitzArt.Flux;

/// <summary>
/// Can be used when no parameters are passed to an operation.
/// </summary>
internal class EmptyOperationParameters : OperationParameters
{
    private static readonly EmptyOperationParameters _instance = new();
    public static EmptyOperationParameters Instance => _instance;

    private protected override IEnumerable<object> Parameters
    {
        get => [];
        set => throw new NotSupportedException();
    }
}

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
    /// <summary>
    /// Empty operation parameter collection - can be used when no parameters are passed to an operation.
    /// </summary>
    public static OperationParameters Empty => EmptyOperationParameters.Instance;

    private protected virtual IEnumerable<object> Parameters { get; set; }

    IEnumerable<object> IOperationParameterCollection.Values => Parameters;

    /// <inheritdoc cref="OperationParameters(IEnumerable{object})"/>"
    public OperationParameters(params object[] parameters) : this((IEnumerable<object>)parameters) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationParameters"/> class.
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public OperationParameters(IEnumerable<object> parameters)
    {
        Parameters = [.. parameters];
    }
}
