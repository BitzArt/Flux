
namespace BitzArt.Flux.REST;

internal class SetBuilder<TModel, TKey> : IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxRestServiceBuilder ServiceBuilder { get; set; }

    public SetBuilder(IFluxRestServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
    }
}
