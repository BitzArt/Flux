using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

/// <summary>
/// Flux REST service builder.
/// </summary>
public interface IFluxRestServiceBuilder : IFluxServiceBuilder
{
    internal IFluxServiceBuilder SourceBuilder { get; }

    internal ServiceOptions ServiceOptions { get; }

    IServiceCollection IFluxBuilder.ServiceCollection => SourceBuilder.ServiceCollection;
    IFluxBuilder IFluxServiceBuilder.FluxBuilder => SourceBuilder.FluxBuilder;
    string IFluxServiceBuilder.ServiceName => SourceBuilder.ServiceName;
}
