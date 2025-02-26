using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <inheritdoc cref="IFluxSetParameterizedOperations{TModel,TKey}"/>
public interface IFluxSetParameterizedOperations<TModel>
{
    // ============================== GetAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync<TInputParameters>(object id, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters;

    // ============================== GetAllAsync ==============================

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <typeparam name="TInputParameters">Input parameters type.</typeparam>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<IEnumerable<TModel>> GetAllAsync<TInputParameters>(TInputParameters parameters, CancellationToken cancellationToken = default)
         where TInputParameters : notnull, IRequestParameters;

    // ============================== GetPageAsync ==============================

    /// <inheritdoc cref="GetPageAsync{TInputParameters}(PageRequest, TInputParameters, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync<TInputParameters>(int offset, int limit, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters
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
        where TInputParameters : notnull, IRequestParameters;

    // ============================== AddAsync ==============================

    /// <inheritdoc cref="AddAsync{TInputParameters,TResponse}(TModel, TInputParameters, CancellationToken)"/>
    public Task<TModel> AddAsync<TInputParameters>(TModel value, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters
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
        where TInputParameters : notnull, IRequestParameters;

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync<TInputParameters>(object id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters
        => UpdateAsync<TInputParameters, TModel>(id, model, parameters, partial, cancellationToken);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TInputParameters, TResponse>(object id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters;

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync<TInputParameters>(TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters
        => UpdateAsync<TInputParameters, TModel>(model, parameters, partial, cancellationToken);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TInputParameters, TResponse>(TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters;
}

/// <summary>
/// Parametrized operations for <see href="https://bitzart.github.io/Flux/">Flux</see> Sets.
/// </summary>
/// <typeparam name="TModel">Model type of the set.</typeparam>
/// <typeparam name="TKey">Key type of the set.</typeparam>
public interface IFluxSetParameterizedOperations<TModel, TKey>
{
    // ============================== GetAsync ==============================

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <typeparam name="TInputParameters">Input parameters type.</typeparam>
    /// <param name="id">Unique identifier of the object to fetch.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TModel> GetAsync<TInputParameters>(TKey id, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters;

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync<TInputParameters>(TKey id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters
        => UpdateAsync<TInputParameters, TModel>(id, model, parameters, partial, cancellationToken);

    /// <summary>
    /// Updates an existing object in the set.
    /// </summary>
    /// <typeparam name="TInputParameters">Input parameters type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="id">Unique identifier of the object to update.</param>
    /// <param name="value">The value to update the object with.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="partial">Whether to perform a partial update (e.g. PATCH in REST).</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> UpdateAsync<TInputParameters, TResponse>(TKey id, TModel value, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IRequestParameters;
}
