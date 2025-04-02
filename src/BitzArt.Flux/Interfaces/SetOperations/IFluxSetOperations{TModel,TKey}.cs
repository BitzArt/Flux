namespace BitzArt.Flux;

/// <summary>
/// Parameterless operations for <see href="https://bitzart.github.io/Flux/">Flux</see> Sets. <br />
/// Parameterized operations are covered by <see cref="IFluxSetParameterizedOperations{TModel,TKey}"/>.
/// </summary>
/// <typeparam name="TModel">Model type of the set.</typeparam>
/// <typeparam name="TKey">Key type of the set.</typeparam>
public interface IFluxSetOperations<TModel, TKey>
{
    // ============================== GetAsync ==============================

    /// <inheritdoc cref="GetAsync{TResponse}(TKey, IOperationParameterCollection?, CancellationToken)"/>
    public Task<TModel> GetAsync(TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="id">Unique identifier of the object to fetch.</param>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAsync<TResponse>(TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    // ============================== AddAsync ==============================

    /// <inheritdoc cref="AddAsync{TResponse}(TModel, TKey, IOperationParameterCollection?, CancellationToken)"/>
    public Task AddAsync(TModel value, TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new object to the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="value">The value to add to the set.</param>
    /// <param name="id">
    /// Unique identifier of the object to add.
    /// <para>
    /// <b>Note:</b> For some <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux implementations</see>,<br/>
    /// the presence of this identifier may result in a keyed operation (e.g. 'PUT' in REST),<br/>
    /// rather than a non-keyed operation (e.g. 'POST' in REST).
    /// </para>
    /// </param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> AddAsync<TResponse>(TModel value, TKey id, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="UpdateAsync{TResponse}(TModel, TKey, bool, IOperationParameterCollection?, CancellationToken)"/>
    public Task UpdateAsync(TModel value, TKey id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing object in the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="value">The value to update the existing object with.</param>
    /// <param name="id">Unique identifier of the object to update.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="partial">Whether to perform a partial update (e.g. PATCH in REST), rather than a full update (e.g. PUT in REST).</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, TKey id, bool partial = false, IOperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);
}
