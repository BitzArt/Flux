using System.Collections;
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

    internal class SimpleParameters : IOperationParameterCollection
    {
        public IEnumerable<object> Values { get; set; }

        IEnumerable<object> IOperationParameterCollection.Values => Values;

        public SimpleParameters(IEnumerable<object> values)
        {
            Values = [.. values];
        }

        public SimpleParameters()
        {
            Values = null!;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not IOperationParameterCollection other)
            {
                return false;
            }

            if (obj is INamedOperationParameterCollection)
            {
                return false;
            }

            for (int i = 0; i < Values.Count(); i++)
            {
                var value = Values.ElementAt(i);
                var otherValue = other.Values.ElementAt(i);

                if (value is IEnumerable enumerable && otherValue is IEnumerable otherEnumerable)
                {
                    if (!enumerable.Cast<object>().SequenceEqual(otherEnumerable.Cast<object>()))
                    {
                        return false;
                    }
                }
                else if (value != otherValue)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Values
                .Aggregate(0, (hash, value) => HashCode.Combine(hash, value.GetHashCode()));
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
    /// <param name="values">Parameters to be used in the operation.</param>
    public OperationParameterCollection(IEnumerable<KeyValuePair<string, object>> values)
    {
        _namedParameters = new NamedParameters(values);
        _parametersType = ParametersType.Named;
    }

    internal class NamedParameters : INamedOperationParameterCollection
    {
        public IEnumerable<KeyValuePair<string, object>> Values { get; set; }

        IEnumerable<KeyValuePair<string, object>> INamedOperationParameterCollection.Values => Values;

        public NamedParameters(IEnumerable<KeyValuePair<string, object>> values)
        {
            Values = values;
        }

        public NamedParameters()
        {
            Values = null!;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not IOperationParameterCollection)
            {
                return false;
            }

            if (obj is not INamedOperationParameterCollection other)
            {
                return false;
            }

            for (int i = 0; i < Values.Count(); i++)
            {
                var kvp = Values.ElementAt(i);
                var otherKvp = other.Values.ElementAt(i);

                if (kvp.Key != otherKvp.Key)
                {
                    return false;
                }

                if (kvp.Value is IEnumerable enumerable && otherKvp.Value is IEnumerable otherEnumerable)
                {
                    if (!enumerable.Cast<object>().SequenceEqual(otherEnumerable.Cast<object>()))
                    {
                        return false;
                    }
                }
                else if (kvp.Value != otherKvp.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Values
                .Aggregate(0, (hash, kvp) => HashCode.Combine(hash, kvp.Key.GetHashCode(), kvp.Value.GetHashCode()));
        }
    }
}
