namespace BitzArt.Flux;

/// <summary>
/// 'Remove' operation descriptor.
/// </summary>
public sealed class RemoveOperationDescriptor : KeyedOperationDescriptor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveOperationDescriptor"/> class.
    /// </summary>
    /// <param name="id">Identifier of the object to remove (if applicable).</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public RemoveOperationDescriptor(object? id, IOperationParameterCollection? parameters)
        : base(id, parameters) { }
}
