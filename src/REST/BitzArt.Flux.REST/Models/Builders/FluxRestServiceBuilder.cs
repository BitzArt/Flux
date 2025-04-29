namespace BitzArt.Flux.REST;

internal class FluxRestServiceBuilder : IFluxRestServiceBuilder
{
    public IFluxServiceBuilder SourceBuilder { get; set; }
    public FluxRestServiceOptions ServiceOptions { get; set; }

    public FluxRestServiceBuilder(IFluxServiceBuilder sourceBuilder, string? baseUrl)
    {
        SourceBuilder = sourceBuilder;
        ServiceOptions = new(baseUrl);
    }
}
