namespace BitzArt.Flux.REST;

internal class FluxRestSetOptions<TModel, TKey> : IFluxRestSetOptions<TModel>
    where TModel : class
{
    public FluxRestServiceOptions ServiceOptions { get; }

    public FluxRestSetEndpointCollection<TModel, TKey> EndpointCollection { get; }

    IFluxRestSetEndpointCollection<TModel> IFluxRestSetOptions<TModel>.EndpointCollection => EndpointCollection;

    public Type? KeyType => typeof(TKey);

    public string? Path { get; }

    public FluxRestSetOptions(FluxRestServiceOptions serviceOptions, string? path = null)
    {
        ServiceOptions = serviceOptions;
        EndpointCollection = new(this);
        Path = path;
    }
}
