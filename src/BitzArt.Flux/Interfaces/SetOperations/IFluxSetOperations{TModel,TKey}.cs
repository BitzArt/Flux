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

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel,TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync(TKey id, CancellationToken cancellationToken = default);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel,TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync(TKey id, TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel,TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(TKey id, TModel model, bool partial = false, CancellationToken cancellationToken = default);
}
