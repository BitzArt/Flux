namespace BitzArt.Flux;

internal class FluxFactory : IFluxFactory
{
    public ICollection<IFluxServiceRegistration> ServiceRegistrations { get; private set; }

    public FluxFactory()
    {
        ServiceRegistrations = [];
    }

    public IFluxServiceRegistration GetServiceRegistration(string name)
    {
        var serviceRegistration = ServiceRegistrations.AsQueryable().FirstOrDefault(x => x.ServiceName == name);

        return serviceRegistration is null
            ? throw new ServiceContextNotFoundException()
            : serviceRegistration;
    }

    public IFluxSetContext<TModel> GetSetContext<TModel>(
        IServiceProvider services,
        string? serviceName = null,
        string? setName = null)
        where TModel : class
    {
        IFluxServiceRegistration? serviceRegistration;
        var q = ServiceRegistrations.AsQueryable();

        if (serviceName is not null)
        {
            serviceRegistration = q.FirstOrDefault(x => x.ServiceName == serviceName);
            if (serviceRegistration is null) throw new ServiceContextNotFoundException();
        }
        else
        {
            var serviceRegistrations = q.Where(x => x.ContainsSignature<TModel>(setName)).ToList();
            if (serviceRegistrations.Count == 0) throw new ServiceContextNotFoundException();
            if (serviceRegistrations.Count > 1) throw new MultipleServiceContextsFoundException();
            serviceRegistration = serviceRegistrations.First();
        }

        return serviceRegistration.CreateSetContext<TModel>(services, setName);
    }

    public IFluxSetContext<TModel, TKey> GetSetContext<TModel, TKey>(
        IServiceProvider services,
        string? serviceName = null,
        string? setName = null)
        where TModel : class
        where TKey : notnull
    {
        IFluxServiceRegistration? serviceRegistration;
        var q = ServiceRegistrations.AsQueryable();

        if (serviceName is not null)
        {
            serviceRegistration = q.FirstOrDefault(x => x.ServiceName == serviceName);
            if (serviceRegistration is null) throw new ServiceContextNotFoundException();
        }
        else
        {
            var serviceRegistrations = q.Where(x => x.ContainsSignature<TModel>(setName)).ToList();
            if (serviceRegistrations.Count == 0) throw new ServiceContextNotFoundException();
            if (serviceRegistrations.Count > 1) throw new MultipleServiceContextsFoundException();
            serviceRegistration = serviceRegistrations.First();
        }

        return serviceRegistration.CreateSetContext<TModel, TKey>(services, setName);
    }
}
