namespace BitzArt.Flux.Json;

internal class FluxJsonSetBuilder<TModel, TKey> : IFluxJsonSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxJsonServiceBuilder ServiceBuilder { get; set; }
    public FluxJsonSetConfiguration SetConfiguration { get; set; }

    public string? BaseFilePath => ServiceBuilder.ServiceConfiguration.BaseFilePath;
    public FluxJsonSetOptions<TModel, TKey> SetOptions { get; set; } = new FluxJsonSetOptions<TModel, TKey>();
    IFluxJsonSetOptions<TModel> IFluxJsonSetBuilder<TModel, TKey>.SetOptions => SetOptions;

    public FluxJsonSetBuilder(IFluxJsonServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        SetConfiguration = new(ServiceBuilder.ServiceConfiguration);
    }
}
