namespace BitzArt.Flux.REST;

internal interface IFluxRestSetIdEndpointOptions<TModel> : IFluxRestSetEndpointOptions<TModel>
    where TModel : class
{
    public Func<object?, object[]?, string>? GetPathFunc { get; set; }
}
