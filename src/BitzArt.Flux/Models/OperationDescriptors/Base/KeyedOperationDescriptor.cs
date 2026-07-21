namespace BitzArt.Flux;

/// <summary>
/// Keyed operation descriptor.
/// </summary>
public abstract class KeyedOperationDescriptor : OperationDescriptor
{
    /// <summary>
    /// Unique identifier of the object to fetch (if applicable).
    /// </summary>
    public object? Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyedOperationDescriptor"/> class.
    /// </summary>
    /// <param name="id">Identifier of the object to fetch (if applicable).</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public KeyedOperationDescriptor(object? id, IOperationParameterCollection? parameters) : base(parameters)
    {
        Id = id;
    }
}
