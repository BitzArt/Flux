using BitzArt.Pagination;

namespace BitzArt.Flux;

public interface IFluxModelContext { }

public interface IFluxModelContext<TModel> : IFluxModelContext
    where TModel : class
{
    public Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters);
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters);
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters);
    public Task<TModel> GetAsync(object? id, params object[]? parameters);
}

public interface IFluxModelContext<TModel, TKey> : IFluxModelContext<TModel>
    where TModel : class
{
    public Task<TModel> GetAsync(TKey? id, params object[]? parameters);
}