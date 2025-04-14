namespace BitzArt.Flux.Operations;

/// <summary>
/// 'Get' operation descriptor.
/// </summary>
public sealed class GetOperationDescriptor : KeyedOperationDescriptor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetOperationDescriptor"/> class.
    /// </summary>
    /// <param name="id">Identifier of the object to fetch (if applicable).</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public GetOperationDescriptor(object? id, IOperationParameterCollection? parameters) : base(id, parameters)
    {
    }
}
