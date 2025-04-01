using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <inheritdoc cref="IFluxSetParameterizedOperations{TModel,TKey}"/>
public interface IFluxSetParameterizedOperations<TModel>
{
    // ============================== GetAsync ==============================

    /// <inheritdoc cref="GetAsync{TInputParameters}(TInputParameters, CancellationToken)"/>
    //public Task<TModel> GetAsync(IEnumerable<KeyValuePair<string, object>> namedParameters, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAsync{TInputParameters}(TInputParameters, CancellationToken)"/>
    //public Task<TModel> GetAsync(IEnumerable<object> parameters, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync<TInputParameters>(TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync<TInputParameters>(object id, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <param name="values">Values to be used by an operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TModel> GetAsync(SetOperationValues values, CancellationToken cancellationToken = default);

    // ============================== GetAllAsync ==============================

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <typeparam name="TInputParameters">Input parameters type.</typeparam>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<IEnumerable<TModel>> GetAllAsync<TInputParameters>(TInputParameters parameters, CancellationToken cancellationToken = default)
         where TInputParameters : notnull, IOperationParameterCollection;

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <param name="values">Values to be used by an operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<IEnumerable<TModel>> GetAllAsync(SetOperationValues values, CancellationToken cancellationToken = default);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc cref="GetPageAsync{TInputParameters}(PageRequest, TInputParameters, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync<TInputParameters>(int offset, int limit, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => GetPageAsync(new PageRequest(offset, limit), parameters, cancellationToken);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <typeparam name="TInputParameters">Input parameters type.</typeparam>
    /// <param name="pageRequest">A <see cref="PageRequest"/> containing page request parameters.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<PageResult<TModel>> GetPageAsync<TInputParameters>(PageRequest pageRequest, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <param name="values">Values to be used by an operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<PageResult<TModel>> GetPageAsync(SetOperationValues values, CancellationToken cancellationToken = default);

    // ============================== AddAsync ==============================

    /// <inheritdoc cref="AddAsync{TInputParameters,TResponse}(TModel, TInputParameters, CancellationToken)"/>
    public Task<TModel> AddAsync<TInputParameters>(TModel value, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => AddAsync<TInputParameters, TModel>(value, parameters, cancellationToken);

    /// <summary>
    /// Adds a new object to the set.
    /// </summary>
    /// <typeparam name="TInputParameters">Input parameters type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="value">The value to add to the set.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> AddAsync<TInputParameters, TResponse>(TModel value, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync<TInputParameters>(TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => UpdateAsync<TInputParameters, TModel>(model, parameters, partial, cancellationToken);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TInputParameters, TResponse>(TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync<TInputParameters>(object id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TInputParameters, TResponse>(object id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;
}
