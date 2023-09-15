using BitzArt.Pagination;

namespace Flex;

public interface ICommunicationContext
{
    ICommunicationContext<TEntity, TKey> Entity<TEntity, TKey>() where TEntity : class;
    ICommunicationContext<TEntity> Entity<TEntity>() where TEntity : class;
}

public interface ICommunicationContext<TEntity>
    where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync(params object[]? parameters);
    public Task<PageResult<TEntity>> GetPageAsync(int offset, int limit, params object[]? parameters);
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, params object[]? parameters);
    public Task<TEntity> GetAsync(object? id, params object[]? parameters);
}

public interface ICommunicationContext<TEntity, TKey> : ICommunicationContext<TEntity>
    where TEntity : class
{
    public Task<TEntity> GetAsync(TKey? id, params object[]? parameters);
}