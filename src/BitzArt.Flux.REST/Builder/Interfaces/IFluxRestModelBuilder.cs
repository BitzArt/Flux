namespace BitzArt.Flux;

public interface IFluxRestModelBuilder<TModel> : IFluxModelBuilder, IFluxRestServiceBuilder
    where TModel : class
{
    public FluxRestModelOptions<TModel> ModelOptions { get; }
}

public interface IFluxRestModelBuilder<TModel, TKey> : IFluxRestModelBuilder<TModel>
    where TModel : class
{
    public new FluxRestModelOptions<TModel, TKey> ModelOptions { get; }
}
