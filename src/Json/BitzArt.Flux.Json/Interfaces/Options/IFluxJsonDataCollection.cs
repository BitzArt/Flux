using System.Linq.Expressions;

namespace BitzArt.Flux.Json;

internal interface IFluxJsonDataCollection<TModel>
    where TModel : class
{
    public ICollection<TModel>? Items { get; set; }

    internal Func<TModel, object>? KeyPropertySelector { get; set; }
}
