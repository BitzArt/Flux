using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <inheritdoc/>
public sealed class SetOperationValues<TInputParameters> : SetOperationValues
    where TInputParameters : notnull, IOperationParameterCollection
{
    /// <summary>
    /// Input parameters to be used in the operation.
    /// </summary>
    public TInputParameters Parameters { get; internal set; } = default!;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetOperationValues"/> class with input parameters of type <typeparamref name="TInputParameters"/>.
    /// </summary>
    /// <param name="id">Unique identifier of the object to fetch (if any).</param>
    /// <param name="pageRequest">Page request parameters (if any).</param>
    /// <param name="model">Model to be used in the operation (if any).</param>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    internal SetOperationValues(TInputParameters parameters, object? id = null, PageRequest? pageRequest = null, object? model = null)
        : base(id, pageRequest, model)
    {
        Parameters = parameters;
    }
}

/// <summary>
/// <para>
/// Values to be used by a Flux operation.
/// </para>
/// <para>
/// Even though it is public, this class should not be used directly.
/// </para>
/// <para>
/// This class is an internal implementation detail of the
/// <see href="https://github.com/BitzArt/Flux">Flux library</see>
/// and should not be used by the library consumers. <br />
/// It is only exposed to allow for the creation of custom
/// <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux implementations</see>.
/// </para>
/// </summary>
public class SetOperationValues
{
    /// <summary>
    /// Unique identifier of the object to fetch (if any).
    /// </summary>
    public object? Id { get; internal set; }

    /// <summary>
    /// Page request parameters (if any).
    /// </summary>
    public PageRequest? PageRequest { get; internal set; }

    /// <summary>
    /// Model to be used in the operation (if any).
    /// </summary>
    public object? Model { get; internal set; }

    /// <summary>
    /// Response type (if any). <br />
    /// Can be used to specify the type of the response
    /// when the operation is expected to return a different type than the set model. <br />
    /// </summary>
    public Type? ResponseType { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetOperationValues"/> class.
    /// </summary>
    /// <param name="id">Unique identifier of the object to fetch (if any).</param>
    /// <param name="pageRequest">Page request parameters (if any).</param>
    /// <param name="model">Model to be used in the operation (if any).</param>
    internal SetOperationValues(object? id = null, PageRequest? pageRequest = null, object? model = null)
    {
        Id = id;
        PageRequest = pageRequest;
        Model = model;
    }
}
