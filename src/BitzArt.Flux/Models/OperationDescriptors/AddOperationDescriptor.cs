namespace BitzArt.Flux;

/// <summary>
/// 'Add' operation descriptor.
/// </summary>
public class AddOperationDescriptor : KeyedOperationDescriptor
{
    /// <summary>
    /// Model to be used in the operation.
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateOperationDescriptor"/> class.
    /// </summary>
    /// <param name="id">Identifier of the object to update (if applicable).</param>
    /// <param name="value">Model to be used in the operation.</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public AddOperationDescriptor(object? id, object value, IOperationParameterCollection? parameters)
        : base(id, parameters)
    {
        Value = value;
    }
}
