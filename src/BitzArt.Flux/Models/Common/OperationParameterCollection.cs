using System.Diagnostics;

namespace BitzArt.Flux;

/// <summary>
/// A collection of operation parameters.
/// <para>
/// Behaves either as
/// <see cref="IOperationParameterCollection"/>
/// or as
/// <see cref="INamedOperationParameterCollection"/>
/// based on the type of the parameters passed to it.
/// </para>
/// </summary>
public class OperationParameterCollection
{
    private readonly SimpleParameters? _simpleParameters;
    private readonly NamedParameters? _namedParameters;

    private enum ParametersType
    {
        Simple,
        Named
    }

    private readonly ParametersType _parametersType;

    internal IOperationParameterCollection Parameters => _parametersType switch
    {
        ParametersType.Simple => _simpleParameters!,
        ParametersType.Named => _namedParameters!,
        _ => throw new UnreachableException($"Unexpected ParametersType value: '{_parametersType}'")
    };

    // ==================== Simple ====================

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationParameterCollection"/> class
    /// as a simple list of parameters.
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public OperationParameterCollection(IEnumerable<object> parameters)
    {
        _simpleParameters = new SimpleParameters(parameters);
        _parametersType = ParametersType.Simple;
    }

    private class SimpleParameters : IOperationParameterCollection
    {
        private protected virtual IEnumerable<object> Parameters { get; set; }

        IEnumerable<object> IOperationParameterCollection.Values => Parameters;

        public SimpleParameters(IEnumerable<object> parameters)
        {
            Parameters = [.. parameters];
        }
    }

    // ==================== Named ====================

    /// <inheritdoc cref="OperationParameterCollection(IDictionary{string, object})"/>
    public OperationParameterCollection(params (string, object)[] parameters)
        : this(parameters.ToDictionary(p => p.Item1, p => p.Item2)) { }

    /// <inheritdoc cref="OperationParameterCollection(IEnumerable{KeyValuePair{string, object}})"/>
    public OperationParameterCollection(params KeyValuePair<string, object>[] parameters)
        : this((IEnumerable<KeyValuePair<string, object>>)parameters) { }

    /// <inheritdoc cref="OperationParameterCollection(IEnumerable{KeyValuePair{string, object}})"/>
    public OperationParameterCollection(IEnumerable<(string, object)> parameters)
        : this(parameters.Select(x => new KeyValuePair<string, object>(x.Item1, x.Item2))) { }

    /// <inheritdoc cref="OperationParameterCollection(IDictionary{string, object})"/>
    public OperationParameterCollection(IEnumerable<KeyValuePair<string, object>> parameters)
        : this(new Dictionary<string, object>(parameters)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationParameterCollection"/> class
    /// as a named collection of parameters.
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public OperationParameterCollection(IDictionary<string, object> parameters)
    {
        _namedParameters = new NamedParameters(parameters);
        _parametersType = ParametersType.Named;
    }

    private class NamedParameters : INamedOperationParameterCollection
    {
        private readonly Dictionary<string, object> _parameters;

        IDictionary<string, object> INamedOperationParameterCollection.Values => _parameters;

        IEnumerable<object> IOperationParameterCollection.Values
            => throw new InvalidOperationException(
                "Named parameters collection should not be used as a simple list of parameters. Use named values instead.");

        public NamedParameters(IDictionary<string, object> parameters)
        {
            _parameters = new(parameters);
        }
    }
}
