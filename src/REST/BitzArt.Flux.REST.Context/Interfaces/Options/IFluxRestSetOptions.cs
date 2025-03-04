namespace BitzArt.Flux.REST;

internal interface IFluxRestSetOptions<TModel>
    where TModel : class
{
    public FluxRestServiceOptions ServiceOptions { get; }

    public IFluxRestSetEndpointCollection<TModel> EndpointCollection { get; }
    string? Path { get; }
    internal Type? KeyType { get; }
}
