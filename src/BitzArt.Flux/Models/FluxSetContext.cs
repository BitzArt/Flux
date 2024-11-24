using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <summary>
/// Flux Set Context base class.
/// </summary>
public abstract class FluxSetContext<TModel, TKey> : IFluxSetContext<TModel, TKey>
    where TModel : class
{
    // ============== GET ALL ==============

    /// <inheritdoc/>
    public abstract Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters);

    // ============== GET (Single) ==============

    /// <inheritdoc/>
    public Task<TModel> GetAsync(object? id = null, params object[]? parameters)
    {
        if (id is not TKey idTyped) throw new InvalidOperationException("Invalid key type");
        return GetAsync(idTyped, parameters);
    }

    /// <inheritdoc/>
    public abstract Task<TModel> GetAsync(TKey? id, params object[]? parameters);

    // ============== GET PAGE ==============

    /// <inheritdoc/>
    public virtual Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters)
        => GetPageAsync(new PageRequest(offset, limit), parameters);

    /// <inheritdoc/>
    public abstract Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters);

    // ============== ADD ==============

    /// <inheritdoc/>
    public virtual Task<TModel> AddAsync(TModel model, params object[]? parameters)
        => AddAsync<TModel>(model, parameters);

    /// <inheritdoc/>
    public abstract Task<TResponse> AddAsync<TResponse>(TModel model, params object[]? parameters);

    // ============== UPDATE ==============

    /// <inheritdoc/>
    Task<TModel> IFluxSetContext<TModel>.UpdateAsync(object? id, TModel model, bool partial, params object[]? parameters)
    {
        if (id is not TKey idTyped) throw new InvalidOperationException("Invalid key type");
        return UpdateAsync<TModel>(idTyped, model, partial, parameters);
    }

    /// <inheritdoc/>
    Task<TModel> IFluxSetContext<TModel>.UpdateAsync(TModel model, bool partial, params object[]? parameters)
        => UpdateAsync<TModel>(model, partial, parameters);

    /// <inheritdoc/>
    Task<TResponse> IFluxSetContext<TModel>.UpdateAsync<TResponse>(object? id, TModel model, bool partial, params object[]? parameters)
        => UpdateAsync<TResponse>(Cast<TKey>(id), model, partial, parameters);

    /// <inheritdoc/>
    Task<TModel> IFluxSetContext<TModel, TKey>.UpdateAsync(TKey? id, TModel model, bool partial, params object[]? parameters)
        => UpdateAsync<TModel>(id, model, partial, parameters);

    /// <inheritdoc/>
    public abstract Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, params object[]? parameters);

    /// <inheritdoc/>
    public abstract Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters);

    private static TResult Cast<TResult>(object? value)
    {
        if (value is not TResult result)
            throw new InvalidOperationException($"Invalid key type. Expected {typeof(TResult).Name} but got {value?.GetType().Name ?? "null"}");

        return result;
    }

    /// <inheritdoc />
    public IFluxSetContext<TResult> Cast<TResult>() where TResult : class => this.Cast<TModel, TResult>();
}