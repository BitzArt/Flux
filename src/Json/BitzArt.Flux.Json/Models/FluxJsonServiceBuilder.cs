using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxJsonServiceBuilder : IFluxJsonServiceBuilder
{
    public IServiceCollection ServiceCollection { get; private set; }
    public IFluxServiceRegistration Registration { get; set; }
    public IFluxFactory ServiceRegistration { get; init; }
    public FluxJsonServiceOptions ServiceOptions { get; init; }

    public FluxJsonServiceBuilder(IFluxServiceBuilder prebuilder)
    {
        ServiceCollection = prebuilder.ServiceCollection;
        ServiceRegistration = prebuilder.Factory;
        ServiceOptions = new FluxJsonServiceOptions();

        if (prebuilder.Name is null) throw new Exception("Missing Name in Flux Service configuration. Specify service names when configuring external services.");

        Registration = new FluxJsonServiceFactory(ServiceOptions, prebuilder.Name);
    }
}