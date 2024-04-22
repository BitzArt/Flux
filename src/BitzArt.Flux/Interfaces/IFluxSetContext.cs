using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <summary>
/// Flux context for a preconfigured data Set.<br/>
/// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see>
/// for more information on how to use it.
/// </summary>
public interface IFluxSetContext<TModel> : IFluxQueryable<TModel>
    where TModel : class
{
    public Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters);
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters);
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters);
    public Task<TModel> GetAsync(object? id = null, params object[]? parameters);

    public Task<TModel> AddAsync(TModel model, params object[]? parameters);
    public Task<TResponse> AddAsync<TResponse>(TModel model, params object[]? parameters);

    public Task<TModel> UpdateAsync(object? id, TModel model, bool partial = false, params object[]? parameters);
    public Task<TResponse> UpdateAsync<TResponse>(object? id, TModel model, bool partial = false, params object[]? parameters);
}

public interface IFluxSetContext<TModel, TKey> : IFluxSetContext<TModel>, IFluxQueryable<TModel, TKey>
    where TModel : class
{
    public Task<TModel> GetAsync(TKey? id, params object[]? parameters);
    public Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters);
}