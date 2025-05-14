namespace BitzArt.Flux;

/// <summary>
/// 'Update' operation descriptor.
/// </summary>
public sealed class UpdateOperationDescriptor : ModelOperationDescriptor
{
    /// <summary>
    /// Whether to perform a partial update (e.g. PATCH in REST).
    /// </summary>
    public bool Partial { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateOperationDescriptor"/> class.
    /// </summary>
    /// <param name="id">Identifier of the object to update (if applicable).</param>
    /// <param name="value">Model to be used in the operation.</param>
    /// <param name="partial">Whether to perform a partial update (e.g. PATCH in REST).</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public UpdateOperationDescriptor(object? id, object value, bool partial, IOperationParameterCollection? parameters)
        : base(id, value, parameters)
    {
        Partial = partial;
    }
}
