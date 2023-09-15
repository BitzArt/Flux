namespace BitzArt.Communicator;

public interface ICommunicatorRestEntityBuilder<TEntity> : ICommunicatorEntityBuilder, ICommunicatorRestServiceBuilder
    where TEntity : class
{
    public CommunicatorRestEntityOptions<TEntity> EntityOptions { get; }
}

public interface ICommunicatorRestEntityBuilder<TEntity, TKey> : ICommunicatorRestEntityBuilder<TEntity>
    where TEntity : class
{
    public new CommunicatorRestEntityOptions<TEntity, TKey> EntityOptions { get; }
}
