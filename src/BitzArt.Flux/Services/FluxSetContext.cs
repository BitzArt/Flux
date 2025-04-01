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
    public Task<TModel> GetAsync(CancellationToken cancellationToken = default)
        => GetAsync(new SetOperationValues(), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(object id, CancellationToken cancellationToken = default)
        => GetAsync(CastId(id), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(TKey id, CancellationToken cancellationToken = default)
        => GetAsync(new SetOperationValues(id: id), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync<TInputParameters>(TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => GetAsync(new SetOperationValues<TInputParameters>(parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync<TInputParameters>(object id, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => GetAsync(CastId(id), parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync<TInputParameters>(TKey id, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => GetAsync(new SetOperationValues<TInputParameters>(parameters, id: id), cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TModel> GetAsync(SetOperationValues values, CancellationToken cancellationToken = default);

    // ============================== GetAllAsync ==============================

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        => GetAllAsync(new SetOperationValues(), cancellationToken);

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> GetAllAsync<TInputParameters>(TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => GetAllAsync(new SetOperationValues<TInputParameters>(parameters), cancellationToken);

    /// <inheritdoc/>
    public abstract Task<IEnumerable<TModel>> GetAllAsync(SetOperationValues values, CancellationToken cancellationToken = default);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, CancellationToken cancellationToken = default)
        => GetPageAsync(new PageRequest(offset, limit), cancellationToken);

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, CancellationToken cancellationToken = default)
        => GetPageAsync(new SetOperationValues(pageRequest: pageRequest), cancellationToken);

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync<TInputParameters>(int offset, int limit, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => GetPageAsync(new PageRequest(offset, limit), parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync<TInputParameters>(PageRequest pageRequest, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => GetPageAsync(new SetOperationValues<TInputParameters>(parameters, pageRequest: pageRequest), cancellationToken);

    /// <inheritdoc/>
    public abstract Task<PageResult<TModel>> GetPageAsync(SetOperationValues values, CancellationToken cancellationToken = default);

    // ============================== AddAsync ==============================

    /// <inheritdoc/>
    public Task<TModel> AddAsync(TModel value, CancellationToken cancellationToken = default)
        => AddAsync<TModel>(value, cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResponse> AddAsync<TResponse>(TModel value, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public Task<TModel> AddAsync<TInputParameters>(TModel value, TInputParameters parameters, CancellationToken cancellationToken = default)
    where TInputParameters : notnull, IOperationParameterCollection
        => AddAsync<TInputParameters, TModel>(value, parameters, cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResponse> AddAsync<TInputParameters, TResponse>(TModel value, TInputParameters parameters, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    /// <inheritdoc/>
    //public abstract Task<TResponse> AddAsync<TInputParameters, TResponse>(TModel value, TInputParameters parameters, CancellationToken cancellationToken = default)
    //    where TInputParameters : notnull, IOperationParameterCollection;

    // ============================== UpdateAsync ==============================

    /// <inheritdoc/>
    public Task<TModel> UpdateAsync(TModel model, bool partial = false, CancellationToken cancellationToken = default)
        => UpdateAsync<TModel>(model, partial, cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public Task<TModel> UpdateAsync(object id, TModel model, bool partial = false, CancellationToken cancellationToken = default)
        => UpdateAsync<TModel>(id, model, partial, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(object id, TModel model, bool partial = false, CancellationToken cancellationToken = default)
        => UpdateAsync<TResponse>(CastId(id), model, partial, cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> UpdateAsync(TKey id, TModel model, bool partial = false, CancellationToken cancellationToken = default)
        => UpdateAsync<TModel>(id, model, partial, cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResponse> UpdateAsync<TResponse>(TKey id, TModel model, bool partial = false, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public Task<TModel> UpdateAsync<TInputParameters>(TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
    where TInputParameters : notnull, IOperationParameterCollection
        => UpdateAsync<TInputParameters, TModel>(model, parameters, partial, cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResponse> UpdateAsync<TInputParameters, TResponse>(TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;

    /// <inheritdoc/>
    public Task<TModel> UpdateAsync<TInputParameters>(object id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
    where TInputParameters : notnull, IOperationParameterCollection
        => UpdateAsync<TInputParameters, TModel>(id, model, parameters, partial, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TInputParameters, TResponse>(object id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection
        => UpdateAsync<TInputParameters, TResponse>(CastId(id), model, parameters, partial, cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> UpdateAsync<TInputParameters>(TKey id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
    where TInputParameters : notnull, IOperationParameterCollection
        => UpdateAsync<TInputParameters, TModel>(id, model, parameters, partial, cancellationToken);

    /// <inheritdoc/>
    public abstract Task<TResponse> UpdateAsync<TInputParameters, TResponse>(TKey id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
        where TInputParameters : notnull, IOperationParameterCollection;
}
