namespace BitzArt.Flux;

internal class FluxFactory : IFluxFactory
{
    public ICollection<IFluxServiceFactory> ServiceContexts { get; private set; }

    public FluxFactory()
    {
        ServiceContexts = [];
    }

    public IFluxServiceFactory GetServiceProvider(string name)
    {
        var serviceContext = ServiceContexts.AsQueryable().FirstOrDefault(x => x.ServiceName == name);
        if (serviceContext is null) throw new FluxServiceProviderNotFoundException();
        return serviceContext;
    }

    public IFluxSetContext<TModel> GetSetContext<TModel>(
        IServiceProvider services,
        string? serviceName = null,
        string? setName = null)
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
            var serviceContexts = q.Where(x => x.ContainsSignature<TModel>(setName)).ToList();
            if (!serviceContexts.Any()) throw new FluxServiceProviderNotFoundException();
            if (serviceContexts.Count > 1) throw new MultipleFluxServiceProviderFoundException();
            serviceContext = serviceContexts.First();
        }

        return serviceContext.CreateSetContext<TModel>(services, setName);
    }

    public IFluxSetContext<TModel, TKey> GetSetContext<TModel, TKey>(
        IServiceProvider services,
        string? serviceName = null,
        string? setName = null)
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
            var serviceContexts = q.Where(x => x.ContainsSignature<TModel>(setName)).ToList();
            if (!serviceContexts.Any()) throw new FluxServiceProviderNotFoundException();
            if (serviceContexts.Count > 1) throw new MultipleFluxServiceProviderFoundException();
            serviceContext = serviceContexts.First();
        }

        return serviceContext.CreateSetContext<TModel, TKey>(services, setName);
    }
}
