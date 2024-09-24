using System.Linq.Expressions;

namespace BitzArt.Flux;

public class FluxJsonSetOptions<TModel>
    where TModel : class
{
    public ICollection<TModel>? Items { get; set; }

    protected Expression<Func<TModel, object>>? _keyPropertyExpression;

    public Expression<Func<TModel, object>>? KeyPropertyExpression
    {
        get => _keyPropertyExpression;
        set => _keyPropertyExpression = value;
    }
}
