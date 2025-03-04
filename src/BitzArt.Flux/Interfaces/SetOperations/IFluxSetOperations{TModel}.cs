using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <inheritdoc cref="IFluxSetOperations{TModel,TKey}"/>
public interface IFluxSetOperations<TModel>
{
    // ============================== GetAsync ==============================

    /*/// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.GetAsync{TInputParameters}(TKey, TInputParameters, CancellationToken)"/>
    public Task<TModel> GetAsync(object id, CancellationToken cancellationToken = default);*/

    // ============================== GetAllAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.GetAllAsync{TInputParameters}(TInputParameters,CancellationToken)"/>/>
    public Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.GetPageAsync{TInputParameters}(PageRequest, TInputParameters, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, CancellationToken cancellationToken = default)
        => GetPageAsync(new PageRequest(offset, limit), cancellationToken);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.GetPageAsync{TInputParameters}(PageRequest, TInputParameters, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, CancellationToken cancellationToken = default);

    // ============================== AddAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.AddAsync{TInputParameters,TResponse}(TModel, TInputParameters, CancellationToken)"/>
    public Task<TModel> AddAsync(TModel value, CancellationToken cancellationToken = default)
        => AddAsync<TModel>(value, cancellationToken);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel}.AddAsync{TInputParameters,TResponse}(TModel, TInputParameters, CancellationToken)"/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, CancellationToken cancellationToken = default);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync(TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TModel> UpdateAsync(object id, TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IFluxSetParameterizedOperations{TModel, TKey}.UpdateAsync{TInputParameters, TResponse}(TKey, TModel, TInputParameters, bool, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(object id, TModel model, bool partial = false, CancellationToken cancellationToken = default);
}
