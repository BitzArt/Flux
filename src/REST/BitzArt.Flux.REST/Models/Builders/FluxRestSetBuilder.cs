
namespace BitzArt.Flux.REST;

internal class FluxRestSetBuilder<TModel, TKey> : IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxRestServiceBuilder ServiceBuilder { get; set; }
    public FluxRestSetOptions SetOptions { get; set; }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder, string? path = null)
    {
        ServiceBuilder = serviceBuilder;
        SetOptions = new(ServiceBuilder.ServiceOptions, path);
    }
}
