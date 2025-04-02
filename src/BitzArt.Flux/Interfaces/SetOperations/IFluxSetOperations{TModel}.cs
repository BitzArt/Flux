using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <inheritdoc cref="IFluxSetOperations{TModel,TKey}"/>
public interface IFluxSetOperations<TModel>
{
    // ============================== GetAsync ==============================

    /// <inheritdoc cref="IFluxSetOperations{TModel,TKey}.GetAsync{TResponse}(TKey, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TModel> GetAsync(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetOperations{TModel,TKey}.GetAsync{TResponse}(TKey, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> GetAsync<TResponse>(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetOperations{TModel,TKey}.GetAsync{TResponse}(TKey, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TModel> GetAsync(object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetOperations{TModel,TKey}.GetAsync{TResponse}(TKey, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> GetAsync<TResponse>(object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAsync{TResponse}(GetOperationDescriptor, CancellationToken)"/>
    public Task<TModel> GetAsync(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAsync<TResponse>(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== GetAllAsync ==============================

    /// <inheritdoc cref="GetAllAsync{TResponse}(IOperationParameterCollection?, CancellationToken)"/>
    public Task<IEnumerable<TModel>> GetAllAsync(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAllAsync<TResponse>(IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAllAsync{TResponse}(GetAllOperationDescriptor, CancellationToken)"/>
    public Task<IEnumerable<TModel>> GetAllAsync(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAllAsync<TResponse>(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc cref="GetPageAsync{TResponse}(int, int, IOperationParameterCollection?, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="offset">Page offset.</param>
    /// <param name="limit">Page limit.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetPageAsync<TResponse>(int offset, int limit, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetPageAsync{TResponse}(PageRequest, IOperationParameterCollection?, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="pageRequest">A <see cref="PageRequest"/> containing page request parameters.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetPageAsync<TResponse>(PageRequest pageRequest, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetPageAsync{TResponse}(GetPageOperationDescriptor, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetPageAsync<TResponse>(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== AddAsync ==============================

    /// <inheritdoc cref="AddAsync{TResponse}(TModel, object, IOperationParameterCollection?, CancellationToken)"/>
    public Task AddAsync(TModel value, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AddAsync{TResponse}(TModel, object, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AddAsync{TResponse}(TModel, object, IOperationParameterCollection?, CancellationToken)"/>
    public Task AddAsync(TModel value, object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetOperations{TModel, TKey}.AddAsync(TModel, TKey, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, object id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AddAsync{TResponse}(AddOperationDescriptor, CancellationToken)"/>
    public Task AddAsync(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new object to the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> AddAsync<TResponse>(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="UpdateAsync(TModel, object, bool, IOperationParameterCollection?, CancellationToken)"/>
    public Task UpdateAsync(TModel value, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="UpdateAsync{TResponse}(TModel, object, bool, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetOperations{TModel, TKey}.UpdateAsync(TModel, TKey, bool, IOperationParameterCollection?, CancellationToken)"/>
    public Task UpdateAsync(TModel value, object id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetOperations{TModel, TKey}.UpdateAsync{TResponse}(TModel, TKey, bool, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, object id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="UpdateAsync{TResponse}(UpdateOperationDescriptor, CancellationToken)"/>
    public Task UpdateAsync(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing object in the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> UpdateAsync<TResponse>(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== ExecuteAsync ==============================

    /// <summary>
    /// Executes an operation on the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> ExecuteAsync<TResponse>(OperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ((Task<TResponse>)ExecuteAsync(descriptor, typeof(TResponse), cancellationToken));

    /// <inheritdoc cref="ExecuteAsync(OperationDescriptor, Type?, CancellationToken)"/>
    public Task ExecuteAsync(OperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes an operation on the set.
    /// </summary>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="responseType">Type to deserialize the response to (if any).</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default);
}
