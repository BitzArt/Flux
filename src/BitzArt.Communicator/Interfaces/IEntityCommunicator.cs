using BitzArt.Pagination;

namespace BitzArt.Communicator;

public interface IEntityCommunicator { }

public interface IEntityCommunicator<TEntity> : IEntityCommunicator
    where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAll();
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest);
    public Task<TEntity> GetAsync(object id);
}

public interface IEntityCommunicator<TEntity, TKey> : IEntityCommunicator<TEntity>
    where TEntity : class
{
    public Task<TEntity> GetAsync(TKey id);
}
