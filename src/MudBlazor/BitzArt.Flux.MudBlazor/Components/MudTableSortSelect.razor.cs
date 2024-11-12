using Microsoft.AspNetCore.Components;

namespace MudBlazor;

/// <summary>
/// <see cref="MudTable{T}"/> sort selection component.
/// </summary>
public partial class MudTableSortSelect<T>
{
    /// <summary>
    /// <see cref="MudTable{T}"/> associated with this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    [Parameter]
    public MudTable<T>? Table { get; set; }

    /// <summary>
    /// The content to display within this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// The text displayed in the input if no <see cref="Item"/> is specified.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Show the clear button in the input.
    /// </summary>
    [Parameter]
    public bool Clearable { get; set; } = true;

    /// <summary>
    /// The apperiance varation of the input.
    /// </summary>
    [Parameter]
    public Variant InputVariant { get; set; } = Variant.Text;

    /// <summary>
    /// The amount of vertical spacing for the input.
    /// </summary>
    [Parameter]
    public Margin InputMargin { get; set; } = Margin.None;

    /// <summary>
    /// CSS classes applied to the input.
    /// </summary>
    [Parameter]
    public string? InputClass { get; set; }

    /// <summary>
    /// Anchor origin point to determen where the popover will open from.
    /// </summary>
    [Parameter]
    public Origin AnchorOrigin { get; set; } = Origin.TopCenter;

    /// <summary>
    /// Transform origin point for the popover.
    /// </summary>
    [Parameter]
    public Origin TransformOrigin { get; set; } = Origin.TopCenter;

    /// <summary>
    /// The apperiance varation of the sort direction button.
    /// </summary>
    [Parameter]
    public Variant ButtonVariant { get; set; } = Variant.Text;

    /// <summary>
    /// The size of the sort direction button. 
    /// </summary>
    [Parameter]
    public Size ButtonSize { get; set; } = Size.Medium;

    /// <summary>
    /// CSS classes applied to the sort direction button.
    /// </summary>
    [Parameter]
    public string? ButtonClass { get; set; }

    /// <summary>
    /// Hide the sort direction button.
    /// </summary>
    [Parameter]
    public bool HideSortButton 
    { 
        get => _hideSortButton; 
        set
        {
            _hideSortButton = value;
            _rememberSortDirection = !_hideSortButton;
        }
    }

    private bool _hideSortButton = false;

    /// <summary>
    /// Occurs when sorting is changed.
    /// </summary>
    [Parameter]
    public EventCallback<MudTableSortLabel<T>> ValueChanged { get; set; }

    /// <summary>
    /// Occurs when <see cref="Item"/> is changed.
    /// </summary>
    [Parameter]
    public EventCallback<MudTableSortSelectItem<T>> ItemChanged { get; set; }

    /// <summary>
    /// Currently selected item of this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    public MudTableSortSelectItem<T>? Item { get; private set; }

    /// <summary>
    /// Current sort label of this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    public MudTableSortLabel<T>? Value { get; private set; }

    /// <summary>
    /// Current sort direction of this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    public SortDirection? SortDirection { get; private set; }

    private bool _rememberSortDirection;

    private Dictionary<ItemSignature, MudTableSortSelectItem<T>?> _itemSignatureMap { get; set; } = [];
    private ItemSignature? _previousItemSignature;

    private MudSelect<MudTableSortSelectItem<T>> _select = null!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        if (!HideSortButton)
        {
            SortDirection = MudBlazor.SortDirection.Ascending;
            _rememberSortDirection = true;
        }
    }

    /// <summary>
    /// Add the <paramref name="item"/> in this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    internal void AddItem(MudTableSortSelectItem<T> item)
    {
        var signature = new ItemSignature(item.SortLabel, item.SortDirection);
        _itemSignatureMap[signature] = item;

        StateHasChanged();
    }

    /// <summary>
    /// Remove the <paramref name="item"/> from this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    internal void RemoveItem(MudTableSortSelectItem<T> item)
    {
        var signature = new ItemSignature(item.SortLabel, item.SortDirection);
        _itemSignatureMap.Remove(signature);

        StateHasChanged();
    }

    private async Task ToggleSortDirectionAsync()
    {
        SortDirection = SortDirection!.Value.Invert();

        if (Item is not null)
        {
            var invertedSignature = new ItemSignature(Item.SortLabel, SortDirection);
            var invertedFound = _itemSignatureMap.TryGetValue(invertedSignature, out var invertedItem);

            if (invertedFound)
            {
                Item = invertedItem;
                await ItemChanged.InvokeAsync(Item);
            }
        }

        await UpdateValueAsync();
    }

    private async Task OnItemChangedAsync(MudTableSortSelectItem<T>? item)
    {
        if (item is not null)
        {
            // Get the item from the map to ensure the correct reference is used.
            var signature = new ItemSignature(item.SortLabel, item.SortDirection);
            Item = _itemSignatureMap[signature]!;
        }
        else
        {
            Item = null;
        }

        await ItemChanged.InvokeAsync(Item);

        await UpdateValueAsync();
    }

    private async Task UpdateValueAsync()
    {
        Value = GetSortLabel();

        await ValueChanged.InvokeAsync(Value);

        if (Table is not null)
            await Table.Context.SetSortFunc(Value!);
    }

    private MudTableSortLabel<T> GetSortLabel()
    {
        if (Item is null)
        {
            if (!_rememberSortDirection) SortDirection = null;
            return CreateNewSortLabel();
        }

        if (Item.SortDirection.HasValue || !_rememberSortDirection)
            SortDirection = Item.SortDirection;

        var sortLabel = Table?.Context.SortLabels.FirstOrDefault(x => x.SortLabel == Item.SortLabel);
        if (sortLabel is null)
        {
            if (!_rememberSortDirection) SortDirection = null;
            return CreateNewSortLabel(); 
        }

        sortLabel.SortDirection = GetSortDirection(sortLabel);
        return sortLabel;
    }

    private MudTableSortLabel<T> CreateNewSortLabel()
    {
        return new MudTableSortLabel<T>
        {
            SortLabel = Item?.SortLabel,
            SortDirection = SortDirection ?? MudBlazor.SortDirection.None
        };
    }

    private SortDirection GetSortDirection(MudTableSortLabel<T> sortLabel)
    {
        if (SortDirection.HasValue)
            return SortDirection!.Value;

        if (sortLabel.SortDirection != MudBlazor.SortDirection.None)
            return sortLabel.SortDirection;

        return MudBlazor.SortDirection.Ascending;
    }

    private void OnRender()
    {
        var previousitem = Item;
        SyncCurrentItem();

        if (previousitem != Item)
            _ = ItemChanged.InvokeAsync(Item);

        var previousSortLabel = Value?.SortLabel;
        var previousSortDirection = Value?.SortDirection;
        SyncCurrentValue();

        if (previousSortLabel != Value?.SortLabel && previousSortDirection != Value?.SortDirection)
            _ = ValueChanged.InvokeAsync(Value);
    }

    private void SyncCurrentItem()
    {
        if (Table is null) return;

        var tableSortLabel = Table.Context.CurrentSortLabel;
        if (tableSortLabel is null || tableSortLabel.SortDirection == MudBlazor.SortDirection.None)
        {
            if (Item is not null) Item = null;
            return;
        }

        if (tableSortLabel.SortLabel is null)
        {
            if (Item is not null) Item = null;
            return;
        }

        var fullMatchSignature = new ItemSignature(tableSortLabel.SortLabel, tableSortLabel.SortDirection);
        _previousItemSignature = fullMatchSignature;
        var fullMatchFound = _itemSignatureMap.TryGetValue(fullMatchSignature, out var fullMatchValue);

        if (fullMatchFound)
        {
            Item = fullMatchValue;
            return;
        }

        var sortLabelMatchSignature = new ItemSignature(tableSortLabel.SortLabel, null);
        var sortLabelMatchFound = _itemSignatureMap.TryGetValue(sortLabelMatchSignature, out var sortLabelMatchValue);

        if (sortLabelMatchFound)
        {
            Item = sortLabelMatchValue;
            return;
        }

        if (Item is not null) Item = null;
    }

    private void SyncCurrentValue()
    {
        if (Table is null) return;

        Value = Item is null
            ? Table.Context.CurrentSortLabel
            : Table.Context.SortLabels.FirstOrDefault(x => x.SortLabel == Item.SortLabel);

        var unsorted = Value is null || Value.SortDirection == MudBlazor.SortDirection.None;
        SortDirection = unsorted
            ? _rememberSortDirection ? MudBlazor.SortDirection.Ascending : null
            : Value!.SortDirection;
    }

    private record ItemSignature(string? SortLabel, SortDirection? SortDirection);
}
