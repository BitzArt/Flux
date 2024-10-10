namespace BitzArt.Flux.REST;

internal class FluxRestSetOptions<TModel, TKey> : IFluxRestSetOptions<TModel>
    where TModel : class
{
    public FluxRestSetEndpointOptions<TModel, TKey> EndpointOptions { get; private set; }

    IFluxRestSetEndpointOptions<TModel> IFluxRestSetOptions<TModel>.EndpointOptions => EndpointOptions;

    public FluxRestSetEndpointOptions<TModel, TKey> PageEndpointOptions { get; private set; }

    IFluxRestSetEndpointOptions<TModel> IFluxRestSetOptions<TModel>.PageEndpointOptions => PageEndpointOptions;

    public FluxRestSetIdEndpointOptions<TModel, TKey> IdEndpointOptions { get; private set; }

    IFluxRestSetIdEndpointOptions<TModel> IFluxRestSetOptions<TModel>.IdEndpointOptions => IdEndpointOptions;

    public Type? KeyType => typeof(TKey);

    public FluxRestSetOptions()
    {
        EndpointOptions = new();
        PageEndpointOptions = new();
        IdEndpointOptions = new();
    }
}