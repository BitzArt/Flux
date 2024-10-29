using Microsoft.AspNetCore.Components;

namespace MudBlazor;

public partial class MudTableSortSelector<T>
{
    private ICollection<MudTableSortSelectorItem<T>> _items = [];

    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public EventCallback<MudTableSortSelectorItem<T>> ValueChanged { get; set; }

    [Parameter]
    public EventCallback<MudTableSortLabel<T>> SortChanged { get; set; }

    [Parameter]
    public MudTable<T>? Table { get; set; }

    private MudSelect<MudTableSortSelectorItem<T>> _select;

    public SortDirection? SortDirection { get; set; }

    public MudTableSortSelectorItem<T>? SelectedItem 
    { 
        get => _selectedItem; 
        set
        {
            _selectedItem = value;
            _ = OnValueChangedAsync(value);
        }
    }

    private MudTableSortSelectorItem<T>? _selectedItem;

    public void AddItem(MudTableSortSelectorItem<T> item)
    {
        _items.Add(item);
    }

    private async Task OnValueChangedAsync(MudTableSortSelectorItem<T>? item)
    {
        //_selectedItem = item;

        if (item is null)
            return;

        if (item.SortDirection.HasValue)
            SortDirection = item.SortDirection;

        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(item);

        var sortLabel = item.GetSortLabel();

        if (SortChanged.HasDelegate)
            await SortChanged.InvokeAsync(sortLabel);

        if (Table is not null)
            await Table.Context.SetSortFunc(sortLabel);
    }
}