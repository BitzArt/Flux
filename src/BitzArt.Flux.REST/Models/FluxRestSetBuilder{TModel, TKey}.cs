using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestSetBuilder<TModel, TKey> : FluxRestSetBuilder<TModel>, IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public new FluxRestSetOptions<TModel, TKey> SetOptions { get; set; }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder) : base(serviceBuilder)
    {
        SetOptions = new();
    }
}
