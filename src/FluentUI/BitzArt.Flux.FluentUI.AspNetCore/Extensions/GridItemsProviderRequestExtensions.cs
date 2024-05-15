using Microsoft.FluentUI.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace BitzArt.Flux;

public static class GridItemsProviderRequestExtensions
{
    public static ValueTuple<Expression, bool>? GetSorting<TModel>(this GridItemsProviderRequest<TModel> request)
    {
        var sortBy = request.SortByColumn?.SortBy;
        if (sortBy is null) return null;

        var type = sortBy.GetType();
        var fieldInfo = type.GetField("_firstExpression", BindingFlags.NonPublic | BindingFlags.Instance);
        var sorting = fieldInfo!.GetValue(sortBy) as ValueTuple<LambdaExpression, bool>?;

        if (sorting is null) return null;

        var sortingExpression = sorting.Value.Item1!.Body;
        var isAscending = sorting.Value.Item2;

        return (sortingExpression, isAscending);
    }

    public static Expression? GetSortingExpression<TModel>(this GridItemsProviderRequest<TModel> request)
        => request.GetSorting()?.Item1;

    public static LambdaExpression? GetSortingColumnPropertyExpression<TModel>(this GridItemsProviderRequest<TModel> request)
    {
        var sortingColumn = request.SortByColumn;
        if (sortingColumn is null) return null;

        var type = sortingColumn.GetType();
        var fieldInfo = type.GetField("Property", BindingFlags.Public | BindingFlags.Instance)!;
        var sorting = fieldInfo.GetValue(sortingColumn) as LambdaExpression;

        return sorting;
    }
}
