namespace BitzArt.Flux.REST;

internal interface IFluxRestSetEndpointOptions<TModel>
    where TModel : class
{
    /// <summary>
    /// A path to the endpoint.
    /// Can be null if other approach to building the endpoint path is used.
    /// </summary>
    string? Path { get; set; }
}
