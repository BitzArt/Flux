using BitzArt.Pagination;

namespace BitzArt.Flux;

public interface IFluxSetContext { }

public interface IFluxSetContext<TModel> : IFluxSetContext
    where TModel : class
{
    public Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters);
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters);
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters);
    public Task<TModel> GetAsync(object? id, params object[]? parameters);
}

public interface IFluxSetContext<TModel, TKey> : IFluxSetContext<TModel>
    where TModel : class
{
    public Task<TModel> GetAsync(TKey? id, params object[]? parameters);
}