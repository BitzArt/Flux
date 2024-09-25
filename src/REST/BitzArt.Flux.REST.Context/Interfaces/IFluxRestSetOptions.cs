namespace BitzArt.Flux.REST;

internal interface IFluxRestSetOptions<TModel>
    where TModel : class
{
    public IFluxRestSetEndpointOptions<TModel> EndpointOptions { get; }

    public IFluxRestSetEndpointOptions<TModel> PageEndpointOptions { get; }

    public IFluxRestSetIdEndpointOptions<TModel> IdEndpointOptions { get; }

    internal Type? KeyType { get; }
}
