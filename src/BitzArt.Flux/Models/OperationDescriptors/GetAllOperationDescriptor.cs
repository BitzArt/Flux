namespace BitzArt.Flux;

/// <summary>
/// 'Get All' operation descriptor.
/// </summary>
public sealed class GetAllOperationDescriptor : OperationDescriptor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllOperationDescriptor"/> class.
    /// </summary>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public GetAllOperationDescriptor(IOperationParameterCollection? parameters) : base(parameters)
    {
    }
}