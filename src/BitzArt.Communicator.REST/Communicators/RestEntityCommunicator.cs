using BitzArt.Pagination;

namespace BitzArt.Communicator;

internal class RestEntityCommunicator<TEntity, TKey> : EntityCommunicatorBase<TEntity, TKey>
    where TEntity : class
{
    public override Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest)
    {
        throw new NotImplementedException();
    }

    public override Task<TEntity> GetAsync(TKey id)
    {
        throw new NotImplementedException();
    }
}
