namespace BitzArt.Flux;

public interface IFluxRestSetEndpointBuilder<TModel> : IFluxRestSetBuilder<TModel>
    where TModel : class
{
}

public interface IFluxRestSetEndpointBuilder<TModel, TKey> : IFluxRestSetEndpointBuilder<TModel>
    where TModel : class
{
}
