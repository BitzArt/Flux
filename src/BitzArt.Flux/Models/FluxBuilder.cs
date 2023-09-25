using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxBuilder : IFluxBuilder
{
	public IServiceCollection Services { get; private set; }
	public IFluxProvider Provider { get; init; }

    public FluxBuilder(IServiceCollection services)
	{
		Services = services;
		Provider = new FluxProvider();
	}
}
