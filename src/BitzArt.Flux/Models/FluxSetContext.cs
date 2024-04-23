using BitzArt.Pagination;
using System.Collections;
using System.Linq.Expressions;

namespace BitzArt.Flux;

/// <summary>
/// Flux Set Context base class.
/// </summary>
public abstract class FluxSetContext<TModel> : IFluxSetContext<TModel>
    where TModel : class
{
    public virtual Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters) => throw new NotImplementedException();
    public virtual Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters) => throw new NotImplementedException();
    public virtual Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters) => throw new NotImplementedException();
    public virtual Task<TModel> GetAsync(object? id = null, params object[]? parameters) => throw new NotImplementedException();

    public virtual Task<TModel> AddAsync(TModel model, params object[]? parameters) => throw new NotImplementedException();
    public virtual Task<TResponse> AddAsync<TResponse>(TModel model, params object[]? parameters) => throw new NotImplementedException();

    public virtual Task<TModel> UpdateAsync(object? id, TModel model, bool partial = false, params object[]? parameters) => throw new NotImplementedException();
    public virtual Task<TModel> UpdateAsync(TModel model, bool partial = false, params object[]? parameters) => throw new NotImplementedException();

    public virtual Task<TResponse> UpdateAsync<TResponse>(object? id, TModel model, bool partial = false, params object[]? parameters) => throw new NotImplementedException();
    public virtual Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, params object[]? parameters) => throw new NotImplementedException();

    // ============== IEnumerable implementation ==============

    public virtual IEnumerator<TModel> GetEnumerator() => throw new NotImplementedException();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // ============== IQueryable implementation ==============

    public virtual Type ElementType => throw new NotImplementedException();
    public virtual Expression Expression => throw new NotImplementedException();
    public virtual IQueryProvider Provider => throw new NotImplementedException();

    public virtual Task<TModel> FirstOrDefaultAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
}

public abstract class FluxSetContext<TModel, TKey> : FluxSetContext<TModel>, IFluxSetContext<TModel, TKey>
    where TModel : class
{
    public virtual Task<TModel> GetAsync(TKey? id, params object[]? parameters) => throw new NotImplementedException();

    public virtual Task<TModel> UpdateAsync(TKey? id, TModel model, bool partial = false, params object[]? parameters) => throw new NotImplementedException();
    public virtual Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters) => throw new NotImplementedException();
}