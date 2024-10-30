using Microsoft.AspNetCore.Components;

namespace MudBlazor;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
internal class MudTableSortSelectorItemValue<T>
{
    /// <summary>
    /// 
    /// </summary>
    public MudTableSortLabel<T> SortLabel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public SortDirection? SortDirection { get; set; }

    private MudTableSortSelector<T> Selector { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sortLabel"></param>
    /// <param name="sortDirection"></param>
    /// <param name="selector"></param>
    public MudTableSortSelectorItemValue(MudTableSortLabel<T> sortLabel, SortDirection? sortDirection, MudTableSortSelector<T> selector)
    {
        SortLabel = sortLabel;
        SortDirection = sortDirection;
        Selector = selector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public MudTableSortLabel<T> GetSortLabel()
    {
        SortLabel.SortDirection = GetSortDirection();
        return SortLabel;
    }

    private SortDirection GetSortDirection()
    {
        if (SortDirection.HasValue)
            return SortDirection.Value;

        if (Selector.SortDirection.HasValue)
            return Selector.SortDirection.Value;

        return MudBlazor.SortDirection.Ascending;
    }
}
