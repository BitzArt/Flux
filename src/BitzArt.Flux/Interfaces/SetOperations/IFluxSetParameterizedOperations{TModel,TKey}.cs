namespace BitzArt.Flux;

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
        where TInputParameters : notnull, IFluxOperationParameters;

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync<TInputParameters>(TKey id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IFluxOperationParameters
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
        where TInputParameters : notnull, IFluxOperationParameters;
}
