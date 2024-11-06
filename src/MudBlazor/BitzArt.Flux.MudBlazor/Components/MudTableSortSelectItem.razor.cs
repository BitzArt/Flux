using Microsoft.AspNetCore.Components;

namespace MudBlazor;

/// <summary>
/// Represents an option component for <see cref="MudTableSortSelect{T}"/>.
/// </summary>
public partial class MudTableSortSelectItem<T> : IDisposable
{
    /// <summary>
    /// The content to display within this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Sort label of this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    [Parameter]
    public string? SortLabel { get; set; }

    /// <summary>
    /// Sort direction of this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    [Parameter]
    public SortDirection? SortDirection { get; set; }

    [CascadingParameter]
    private MudTableSortSelect<T>? _parentSelector { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        if (_parentSelector is null)
            throw new InvalidOperationException($"{nameof(MudTableSortSelectItem<T>)} requires a parent {nameof(MudTableSortSelect<T>)} component.");

        _parentSelector.AddItem(this);
    }

    /// <summary>
    /// Returns <see cref="MudTableSortLabel{T}"/> for this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    internal MudTableSortLabel<T> GetSortLabel()
    {
        if (_parentSelector!.Table is null)
            return CreateNewSortLabel();

        var sortLabel = _parentSelector!.Table.Context.SortLabels.FirstOrDefault(x => x.SortLabel == SortLabel);
        if (sortLabel is null)
            return CreateNewSortLabel();

        sortLabel.SortDirection = GetSortDirection();
        return sortLabel;
    }

    private MudTableSortLabel<T> CreateNewSortLabel()
    {
        return new MudTableSortLabel<T>
        {
            SortLabel = SortLabel,
            SortDirection = GetSortDirection()
        };
    }

    private SortDirection GetSortDirection()
    {
        if (SortDirection.HasValue)
            return SortDirection.Value;

        if (_parentSelector!.CurrentSortDirection.HasValue)
            return _parentSelector.CurrentSortDirection.Value;

        return MudBlazor.SortDirection.Ascending;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        _parentSelector!.RemoveItem(this);
    }
}
