using System.Linq.Expressions;

namespace BitzArt.Flux.Json;

internal interface IFluxJsonSetOptions<TModel>
    where TModel : class
{
    public ICollection<TModel>? Items { get; set; }

    internal Expression<Func<TModel, object>>? KeyPropertyExpression { get; set; }
}
