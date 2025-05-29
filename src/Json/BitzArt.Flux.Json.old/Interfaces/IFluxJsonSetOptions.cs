using System.Linq.Expressions;

namespace BitzArt.Flux;

internal interface IFluxJsonSetOptions<TModel>
{
    public ICollection<TModel>? Items { get; internal set; }

    public Expression<Func<TModel, object>>? KeyPropertyExpression { get; internal set; }
}
