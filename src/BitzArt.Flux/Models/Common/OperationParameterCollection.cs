using System.Diagnostics;
using System.Text.Json;

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

    /// <summary>
    /// <para>
    /// Collection of parameters to be used in the operation.
    /// </para>
    /// <para>
    /// <b>Note:</b> This value will implement <see cref="INamedOperationParameterCollection"/>
    /// in cases where named parameters are provided. <br />
    /// In such cases, the <see cref="IOperationParameterCollection.Values"/> property
    /// will not be available (and will throw an exception if an attempt is made to access it),
    /// and the <see cref="INamedOperationParameterCollection.Values"/> property should be used instead
    /// (which requires this value to be cast to <see cref="INamedOperationParameterCollection"/> first).
    /// </para>
    /// </summary>
    public IOperationParameterCollection Parameters => _parametersType switch
    {
        ParametersType.Simple => _simpleParameters!,
        ParametersType.Named => _namedParameters!,
        _ => throw new UnreachableException($"Unexpected ParametersType value: '{_parametersType}'")
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationParameterCollection"/> class
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public OperationParameterCollection(IOperationParameterCollection parameters)
    {
        switch (parameters)
        {
            case INamedOperationParameterCollection namedParameters:
                _namedParameters = new NamedParameters(namedParameters.Values);
                _parametersType = ParametersType.Named;
                break;

            default:
                _simpleParameters = new SimpleParameters(parameters.Values);
                _parametersType = ParametersType.Simple;
                break;
        }
    }

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

    /// <inheritdoc cref="OperationParameterCollection(IEnumerable{KeyValuePair{string, object}})"/>
    public OperationParameterCollection(IEnumerable<(string, object)> parameters)
        : this(parameters.Select(x => new KeyValuePair<string, object>(x.Item1, x.Item2))) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationParameterCollection"/> class
    /// as a named collection of parameters.
    /// </summary>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    public OperationParameterCollection(IEnumerable<KeyValuePair<string, object>> parameters)
    {
        _namedParameters = new NamedParameters(parameters);
        _parametersType = ParametersType.Named;
    }

    private class NamedParameters : INamedOperationParameterCollection
    {
        private readonly IEnumerable<KeyValuePair<string, object>> _parameters;

        IEnumerable<KeyValuePair<string, object>> INamedOperationParameterCollection.Values => _parameters;

        public NamedParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            _parameters = parameters;
        }
    }
}
