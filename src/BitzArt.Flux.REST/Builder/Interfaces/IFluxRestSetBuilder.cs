namespace BitzArt.Flux;

public interface IFluxRestSetBuilder<TModel> : IFluxModelBuilder, IFluxRestServiceBuilder
    where TModel : class
{
    public FluxRestSetOptions<TModel> SetOptions { get; }
}

public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestSetBuilder<TModel>
    where TModel : class
{
    public new FluxRestSetOptions<TModel, TKey> SetOptions { get; }
}
