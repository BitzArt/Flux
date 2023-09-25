using BitzArt.Pagination;

namespace BitzArt.Flux;

public interface IFluxEntityContext { }

public interface IFluxEntityContext<TEntity> : IFluxEntityContext
    where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync(params object[]? parameters);
    public Task<PageResult<TEntity>> GetPageAsync(int offset, int limit, params object[]? parameters);
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, params object[]? parameters);
    public Task<TEntity> GetAsync(object? id, params object[]? parameters);
}

public interface IFluxEntityContext<TEntity, TKey> : IFluxEntityContext<TEntity>
    where TEntity : class
{
    public Task<TEntity> GetAsync(TKey? id, params object[]? parameters);
}