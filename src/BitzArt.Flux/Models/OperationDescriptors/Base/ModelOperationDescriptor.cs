namespace BitzArt.Flux;

/// <summary>
/// An operation descriptor that utilizes a model and also, optionally, it's identifier.
/// </summary>
public abstract class ModelOperationDescriptor : KeyedOperationDescriptor
{
    /// <summary>
    /// Model to be used in the operation.
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelOperationDescriptor"/> class.
    /// </summary>
    /// <param name="id">Identifier of the object to update (if applicable).</param>
    /// <param name="value">Model to be used in the operation.</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public ModelOperationDescriptor(object? id, object value, IOperationParameterCollection? parameters)
        : base(id, parameters)
    {
        Value = value;
    }
}
