namespace BitzArt.Flux.Json;

internal class FluxJsonSetBuilder<TModel, TKey> : IFluxJsonSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxJsonServiceBuilder ServiceBuilder { get; set; }
    public FluxJsonSetConfiguration<TModel, TKey> SetConfiguration { get; set; }

    public string? BaseFilePath => ServiceBuilder.ServiceConfiguration.BaseFilePath;

    public FluxJsonSetBuilder(IFluxJsonServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        SetConfiguration = new(ServiceBuilder.ServiceConfiguration);
    }
}
