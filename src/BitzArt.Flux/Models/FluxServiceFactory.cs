namespace BitzArt.Flux;

internal class FluxServiceFactory : IFluxServiceFactory
{
    public ICollection<IFluxServiceContext> ServiceContexts { get; private set; }

    public FluxServiceFactory()
    {
        ServiceContexts = new HashSet<IFluxServiceContext>();
    }

    public IFluxEntityContext<TEntity> GetEntityContext<TEntity>(
        IServiceProvider services,
        string? serviceName = null)
        where TEntity : class
    {
        IFluxServiceContext? serviceContext;
        var q = ServiceContexts.AsQueryable();

        if (serviceName is not null)
        {
            serviceContext = q.FirstOrDefault(x => x.ServiceName == serviceName);
            if (serviceContext is null) throw new FluxServiceContextNotFoundException();
        }
        else
        {
            var serviceContexts = q.Where(x => x.ContainsSignature<TEntity>()).ToList();
            if (!serviceContexts.Any()) throw new FluxServiceContextNotFoundException();
            if (serviceContexts.Count > 1) throw new MultipleFluxServiceContextFoundException();
            serviceContext = serviceContexts.First();
        }

        return serviceContext.CreateEntityContext<TEntity>(services);
    }

    public IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(
        IServiceProvider services,
        string? serviceName = null)
        where TEntity : class
    {
        IFluxServiceContext? serviceContext;
        var q = ServiceContexts.AsQueryable();

        if (serviceName is not null)
        {
            serviceContext = q.FirstOrDefault(x => x.ServiceName == serviceName);
            if (serviceContext is null) throw new FluxServiceContextNotFoundException();
        }
        else
        {
            var serviceContexts = q.Where(x => x.ContainsSignature<TEntity>()).ToList();
            if (!serviceContexts.Any()) throw new FluxServiceContextNotFoundException();
            if (serviceContexts.Count > 1) throw new MultipleFluxServiceContextFoundException();
            serviceContext = serviceContexts.First();
        }

        return serviceContext.CreateEntityContext<TEntity, TKey>(services);
    }
}

file class FluxServiceContextNotFoundException : Exception
{
    public FluxServiceContextNotFoundException()
        : base("Requested Service Context was not found.")
        { }
}

file class MultipleFluxServiceContextFoundException : Exception
{
    public MultipleFluxServiceContextFoundException()
        : base("Multiple matching Service Contexts were found.")
    { }
}