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
    private MudTableSortSelect<T>? ParentSelector
    {
        get => _parentSelector;
        set
        {
            if (value is null) return;
            if (_parentSelector is not null) return;

            _parentSelector = value;

            if (_parentSelector is null)
                throw new InvalidOperationException($"{nameof(MudTableSortSelectItem<T>)} requires a parent {nameof(MudTableSortSelect<T>)} component.");

            _parentSelector.AddItem(this);
        }
    }

    private MudTableSortSelect<T>? _parentSelector;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        if (_parentSelector is null) return;
        _parentSelector!.RemoveItem(this);
    }
}
