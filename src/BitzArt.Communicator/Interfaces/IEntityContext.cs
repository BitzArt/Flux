using BitzArt.Pagination;

namespace BitzArt.Communicator;

public interface IEntityContext { }

public interface IEntityContext<TEntity> : IEntityContext
    where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest);
    public Task<TEntity> GetAsync(object id);
}

public interface IEntityContext<TEntity, TKey> : IEntityContext<TEntity>
    where TEntity : class
{
    public Task<TEntity> GetAsync(TKey id);
}
