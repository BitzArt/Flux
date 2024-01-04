using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxJsonServiceBuilder : IFluxJsonServiceBuilder
{
    public IServiceCollection Services { get; private set; }
    public IFluxServiceFactory ServiceFactory { get; set; }
    public IFluxFactory Factory { get; init; }
    public FluxJsonServiceOptions ServiceOptions { get; init; }
    public string? BasePath { get; }
    
    public FluxJsonServiceBuilder(IFluxServicePreBuilder prebuilder, string? basePath = null)
    {
        Services = prebuilder.Services;
        Factory = prebuilder.Factory;
        ServiceOptions = new FluxJsonServiceOptions();
        BasePath = basePath;

        if (prebuilder.Name is null) throw new Exception("Missing Name in Flux Service configuration. Consider using .WithName() when configuring external services.");
       
        ServiceFactory = new FluxJsonServiceFactory(ServiceOptions, prebuilder.Name);
    }
}