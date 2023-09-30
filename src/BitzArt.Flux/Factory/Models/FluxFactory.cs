namespace BitzArt.Flux;

internal class FluxFactory : IFluxFactory
{
    public ICollection<IFluxServiceFactory> ServiceContexts { get; private set; }

    public FluxFactory()
    {
        ServiceContexts = new HashSet<IFluxServiceFactory>();
    }

    public IFluxServiceFactory GetServiceProvider(string name)
    {
        var serviceContext = ServiceContexts.AsQueryable().FirstOrDefault(x => x.ServiceName == name);
        if (serviceContext is null) throw new FluxServiceProviderNotFoundException();
        return serviceContext;
    }

    public IFluxModelContext<TModel> GetModelContext<TModel>(
        IServiceProvider services,
        string? serviceName = null)
        where TModel : class
    {
        IFluxServiceFactory? serviceContext;
        var q = ServiceContexts.AsQueryable();

        if (serviceName is not null)
        {
            serviceContext = q.FirstOrDefault(x => x.ServiceName == serviceName);
            if (serviceContext is null) throw new FluxServiceProviderNotFoundException();
        }
        else
        {
            var serviceContexts = q.Where(x => x.ContainsSignature<TModel>()).ToList();
            if (!serviceContexts.Any()) throw new FluxServiceProviderNotFoundException();
            if (serviceContexts.Count > 1) throw new MultipleFluxServiceProviderFoundException();
            serviceContext = serviceContexts.First();
        }

        return serviceContext.CreateModelContext<TModel>(services);
    }

    public IFluxModelContext<TModel, TKey> GetModelContext<TModel, TKey>(
        IServiceProvider services,
        string? serviceName = null)
        where TModel : class
    {
        IFluxServiceFactory? serviceContext;
        var q = ServiceContexts.AsQueryable();

        if (serviceName is not null)
        {
            serviceContext = q.FirstOrDefault(x => x.ServiceName == serviceName);
            if (serviceContext is null) throw new FluxServiceProviderNotFoundException();
        }
        else
        {
            var serviceContexts = q.Where(x => x.ContainsSignature<TModel>()).ToList();
            if (!serviceContexts.Any()) throw new FluxServiceProviderNotFoundException();
            if (serviceContexts.Count > 1) throw new MultipleFluxServiceProviderFoundException();
            serviceContext = serviceContexts.First();
        }

        return serviceContext.CreateModelContext<TModel, TKey>(services);
    }
}

file class FluxServiceProviderNotFoundException : Exception
{
    public FluxServiceProviderNotFoundException()
        : base("Requested Flux Service Provider was not found.")
    { }
}

file class MultipleFluxServiceProviderFoundException : Exception
{
    public MultipleFluxServiceProviderFoundException()
        : base("Multiple matching Flux Service Providers were found.")
    { }
}