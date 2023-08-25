using BitzArt.Pagination;

namespace BitzArt.Communicator;

public abstract class EntityCommunicatorBase<TEntity, TKey> : IEntityCommunicator<TEntity, TKey>
    where TEntity : class
{
    public abstract Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest);
    public abstract Task<TEntity> GetAsync(TKey id);

    public Task<TEntity> GetAsync(object id) => GetAsync((TKey)id);
}
