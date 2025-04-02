using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <summary>
/// Base class for set context implementations.
/// </summary>
public abstract class FluxSetContext<TModel, TKey> : IFluxSetContext<TModel, TKey>
    where TModel : class
    where TKey : notnull
{
    // ============================== General ==============================

    private static TKey CastId(object value)
    {
        if (value is not TKey valueCasted)
            throw new InvalidOperationException($"Invalid key type. Expected '{typeof(TKey).Name}' but got '{value.GetType().Name}'.");

        return valueCasted;
    }

    // ============================== GetAsync ==============================

    /// <inheritdoc/>
    public Task<TModel> GetAsync(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TResponse>(new GetOperationDescriptor(id: null, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(id, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TResponse>(CastId(id), parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(id, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TResponse>(new GetOperationDescriptor(id: id, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetAllAsync ==============================

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> GetAllAsync(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAllAsync<IEnumerable<TModel>>(parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAllAsync<TResponse>(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAllAsync<TResponse>(new GetAllOperationDescriptor(parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> GetAllAsync(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => GetAllAsync<IEnumerable<TModel>>(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAllAsync<TResponse>(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<PageResult<TModel>>(offset, limit, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetPageAsync<TResponse>(int offset, int limit, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<TResponse>(new PageRequest(offset, limit), parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<PageResult<TModel>>(pageRequest, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetPageAsync<TResponse>(PageRequest pageRequest, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<TResponse>(new GetPageOperationDescriptor(pageRequest: pageRequest, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => GetPageAsync<PageResult<TModel>>(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetPageAsync<TResponse>(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== AddAsync ==============================

    /// <inheritdoc/>
    public Task AddAsync(TModel value, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync(new AddOperationDescriptor(id: null, value: value, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync<TResponse>(new AddOperationDescriptor(id: null, value: value, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task AddAsync(TModel value, object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync(value, CastId(id), parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync<TResponse>(value, CastId(id), parameters, cancellationToken);

    /// <inheritdoc/>
    public Task AddAsync(TModel value, TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync(new AddOperationDescriptor(id: id, value: value, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync<TResponse>(new AddOperationDescriptor(id: id, value: value, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task AddAsync(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> AddAsync<TResponse>(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc/>
    public Task UpdateAsync(TModel value, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync(new UpdateOperationDescriptor(id: null, value: value, partial: partial, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync<TResponse>(new UpdateOperationDescriptor(id: null, value: value, partial: partial, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task UpdateAsync(TModel value, object id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync(value, CastId(id), partial, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, object id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync<TResponse>(value, CastId(id), partial, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task UpdateAsync(TModel value, TKey id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync(new UpdateOperationDescriptor(id: id, value: value, partial: partial, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, TKey id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync<TResponse>(new UpdateOperationDescriptor(id: id, value: value, partial: partial, parameters: parameters), cancellationToken);

    /// <inheritdoc/>
    public Task UpdateAsync(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== ExecuteAsync ==============================

    /// <inheritdoc/>
    public Task<TResponse> ExecuteAsync<TResponse>(OperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ((Task<TResponse>)ExecuteAsync(descriptor, typeof(TResponse), cancellationToken));

    /// <inheritdoc/>
    public Task ExecuteAsync(OperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync(descriptor, null, cancellationToken);

    /// <inheritdoc/>
    public abstract Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default);
}
