using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <inheritdoc cref="IFluxSetOperations{TModel,TKey}"/>
public interface IFluxSetOperations<TModel>
{
    // ============================== GetAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TModel> GetAsync(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== GetAllAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.GetAllAsync{TInputParameters}(TInputParameters,CancellationToken)"/>/>
    public Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<IEnumerable<TModel>> GetAllAsync(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.GetPageAsync{TInputParameters}(PageRequest, TInputParameters, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, CancellationToken cancellationToken = default)
        => GetPageAsync(new PageRequest(offset, limit), cancellationToken);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.GetPageAsync{TInputParameters}(PageRequest, TInputParameters, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<PageResult<TModel>> GetPageAsync(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== AddAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.AddAsync{TInputParameters,TResponse}(TModel, TInputParameters, CancellationToken)"/>
    public Task<TModel> AddAsync(TModel value, CancellationToken cancellationToken = default)
        => AddAsync<TModel>(value, cancellationToken);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.AddAsync{TInputParameters,TResponse}(TModel, TInputParameters, CancellationToken)"/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new object to the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> AddAsync<TResponse>(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync(TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync(object id, TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(object id, TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing object in the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> UpdateAsync<TResponse>(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default);
}
