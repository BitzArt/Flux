using BitzArt.Pagination;

namespace BitzArt.Communicator;

public interface ICommunicationContext
{
    ICommunicationContext<TEntity, TKey> Entity<TEntity, TKey>() where TEntity : class;
    ICommunicationContext<TEntity> Entity<TEntity>() where TEntity : class;
}

public interface ICommunicationContext<TEntity>
    where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<PageResult<TEntity>> GetPageAsync(int offset, int limit);
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest);
    public Task<TEntity> GetAsync(object id);
}

public interface ICommunicationContext<TEntity, TKey> : ICommunicationContext<TEntity>
    where TEntity : class
{
    public Task<TEntity> GetAsync(TKey id);
}