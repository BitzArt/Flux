namespace MudBlazor;

/// <summary>
/// Represents a type of <see cref="MudTableSortSelectItem{T}"/> value.
/// </summary>
internal class MudTableSortSelectItemValue<T>(MudTableSortLabel<T> sortLabel, SortDirection? sortDirection, MudTableSortSelect<T> selector)
{
    /// <summary>
    /// Sort label of this <see cref="MudTableSortSelectItemValue{T}"/>.
    /// </summary>
    public MudTableSortLabel<T> SortLabel { get; set; } = sortLabel;

    /// <summary>
    /// Sort direction of this <see cref="MudTableSortSelectItemValue{T}"/>.
    /// </summary>
    public SortDirection? SortDirection { get; set; } = sortDirection;

    private MudTableSortSelect<T> _selector { get; set; } = selector;

    /// <summary>
    /// Returns <see cref="MudTableSortLabel{T}"/> with the current sort direction.
    /// </summary>
    public MudTableSortLabel<T> GetSortLabel()
    {
        SortLabel.SortDirection = GetSortDirection();
        return SortLabel;
    }

    private SortDirection GetSortDirection()
    {
        if (SortDirection.HasValue)
            return SortDirection.Value;

        if (_selector.SortDirection.HasValue)
            return _selector.SortDirection.Value;

        return MudBlazor.SortDirection.Ascending;
    }
}
