using Microsoft.FluentUI.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace BitzArt.Flux;

public static class GridItemsProviderRequestExtensions
{
    public static ValueTuple<LambdaExpression, bool>? GetSorting<TModel>(this GridItemsProviderRequest<TModel> request)
    {
        var sortBy = request.SortByColumn?.SortBy;
        var type = sortBy?.GetType();
        var fieldInfo = type?.GetField("_firstExpression", BindingFlags.NonPublic | BindingFlags.Instance);
        var sorting = fieldInfo?.GetValue(sortBy) as ValueTuple<LambdaExpression, bool>?;

        return sorting;
    }

    public static LambdaExpression? GetSortingExpression<TModel>(this GridItemsProviderRequest<TModel> request)
        => request.GetSorting()?.Item1;
}
