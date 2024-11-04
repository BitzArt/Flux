using Microsoft.AspNetCore.Components;

namespace MudBlazor;

/// <summary>
/// Represents an option component for <see cref="MudTableSortSelect{T}"/>.
/// </summary>
public partial class MudTableSortSelectItem<T>
{
    /// <summary>
    /// The content to display within this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// The sort label value of this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    [Parameter]
    public string? SortLabel { get; set; }

    /// <summary>
    /// The sort direction of this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    [Parameter]
    public SortDirection? SortDirection { get; set; }

    /// <summary>
    /// The value of this <see cref="MudTableSortSelectItem{T}"/>.
    /// </summary>
    internal MudTableSortSelectItemValue<T> Value = null!;

    [CascadingParameter]
    private MudTableSortSelect<T>? _parentSelector { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        if (_parentSelector is null)
            throw new InvalidOperationException($"{nameof(MudTableSortSelectItem<T>)} requires a parent {nameof(MudTableSortSelect<T>)} component.");
        
        Value = new(GetSortLabel(), SortDirection, _parentSelector!);

        _parentSelector.AddItem(this);
    }

    private MudTableSortLabel<T> GetSortLabel()
    {
        if (_parentSelector!.Table is null) return CreateNewSortLabel();

        var sortLabel = _parentSelector!.Table.Context.SortLabels.FirstOrDefault(x => x.SortLabel == SortLabel);
        if (sortLabel is null) return CreateNewSortLabel();

        return sortLabel;
    }

    private MudTableSortLabel<T> CreateNewSortLabel()
    {
        return new MudTableSortLabel<T>
        {
            SortLabel = SortLabel,
            SortDirection = SortDirection ?? MudBlazor.SortDirection.None
        };
    }
}
