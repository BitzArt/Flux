namespace BitzArt.Flux.Operations;

/// <summary>
/// Describes an operation to be executed by the Flux engine. <br />
/// </summary>
public abstract class OperationDescriptor
{
    /// <summary>
    /// Input parameters to be used in the operation.
    /// </summary>
    public IOperationParameterCollection? Parameters { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationDescriptor"/> class.
    /// </summary>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public OperationDescriptor(IOperationParameterCollection? parameters)
    {
        Parameters = parameters;
    }
}
