using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <summary>
/// Operation descriptor for paged operations.
/// </summary>
public sealed class GetPageOperationDescriptor : OperationDescriptor
{
    /// <summary>
    /// Page request parameters.
    /// </summary>
    public PageRequest PageRequest { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPageOperationDescriptor"/> class.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public GetPageOperationDescriptor(PageRequest pageRequest, IOperationParameterCollection? parameters)
        : base(parameters)
    {
        PageRequest = pageRequest;
    }
}
