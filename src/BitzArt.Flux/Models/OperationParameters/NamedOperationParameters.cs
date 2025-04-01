using System.Collections;

namespace BitzArt.Flux;

/// <summary>
/// Can be used as a way to pass a collection of named parameters to a Flux operation.
/// </summary>
public class NamedOperationParameters : INamedOperationParameterCollection
{
    private readonly Dictionary<string, object> _parameters;

    IDictionary<string, object> INamedOperationParameterCollection.Values => _parameters;

    IEnumerable<object> IOperationParameterCollection.Values
        => throw new InvalidOperationException(
            "Named parameters collection should not be used as a simple list of parameters. Use named values instead.");

    /// <inheritdoc cref="NamedOperationParameters(IDictionary{string, object})"/>"
    public NamedOperationParameters(params KeyValuePair<string, object>[] parameters)
        : this((IEnumerable<KeyValuePair<string, object>>)parameters) { }

    /// <inheritdoc cref="NamedOperationParameters(IDictionary{string, object})"/>"
    public NamedOperationParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        : this(new Dictionary<string, object>(parameters)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedOperationParameters"/> class.
    /// </summary>
    /// <param name="parameters">Named arameters to be used in the operation.</param>
    public NamedOperationParameters(IDictionary<string, object> parameters)
    {
        _parameters = new(parameters);
    }
}
