using BitzArt;
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
    /// The text displayed in the input if no <see cref="Value"/> is specified.
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
    /// /// Fills the full width of the parent container.
    /// </summary>
    [Parameter]
    public bool InputFullWidth { get; set; } = true;

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
    public bool HideSortButton { get; set; } = false;

    /// <summary>
    /// The content to display within this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Occurs when sorting is changed.
    /// </summary>
    [Parameter]
    public EventCallback<MudTableSortLabel<T>> SortChanged { get; set; }

    /// <summary>
    /// Current sort direction of this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    public SortDirection? SortDirection { get; set; }

    internal MudTableSortSelectItemValue<T>? Value { get; set; }

    private ICollection<MudTableSortSelectItem<T>> _items { get; set; } = [];

    private Dictionary<ValueSignature, MudTableSortSelectItemValue<T>> _signatureMap { get; set; } = [];

    private ValueSignature? _previousValueSignature;

    private bool _rememberSortDirection;

    private MudSelect<MudTableSortSelectItemValue<T>> _select = null!;

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
    /// Register the <paramref name="item"/> in this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    internal void AddItem(MudTableSortSelectItem<T> item)
    {
        _items.Add(item);

        var signature = new ValueSignature(item.SortLabel, item.SortDirection);
        _signatureMap[signature] = item.Value;
    }

    /// <summary>
    /// Unregister the <paramref name="item"/> from this <see cref="MudTableSortSelect{T}"/>.
    /// </summary>
    internal void RemoveItem(MudTableSortSelectItem<T> item)
    {
        _items.Remove(item);

        var signature = new ValueSignature(item.SortLabel, item.SortDirection);
        _signatureMap.Remove(signature);

        if (_previousValueSignature == signature)
        {
            _previousValueSignature = null;
            _ = OnValueChangedAsync(GetSortLabel(null));
        }
    }

    private async Task OnValueChangedAsync(MudTableSortSelectItemValue<T>? value)
    {
        Value = value;
        var sortLabel = GetSortLabel(value);
        await OnValueChangedAsync(sortLabel);
    }

    private async Task OnValueChangedAsync(MudTableSortLabel<T> sortLabel)
    {
        if (SortChanged.HasDelegate)
            await SortChanged.InvokeAsync(sortLabel);

        if (Table is not null)
            await Table.Context.SetSortFunc(sortLabel).IgnoreCancellation();

        await InvokeAsync(StateHasChanged);
    }

    private MudTableSortLabel<T> GetSortLabel(MudTableSortSelectItemValue<T>? value)
    {
        if (value is null)
        {
            return new MudTableSortLabel<T>
            {
                SortLabel = null,
                SortDirection = SortDirection!.Value
            };
        }

        if (value.SortDirection.HasValue && _rememberSortDirection)
            SortDirection = value.SortDirection;

        return value.GetSortLabel();
    }

    private async Task ToggleSortDirectionAsync()
    {
        SortDirection = SortDirection!.Value.Invert();

        TryInvertValue();

        await OnValueChangedAsync(Value);
    }

    private void TryInvertValue()
    {
        // If no value is selected, toggling the sort direction should not cause any value to be selected.
        if (Value is null) return;

        var targetSignature = new ValueSignature(Value.SortLabel.SortLabel, SortDirection);
        var oppositeFound = _signatureMap.TryGetValue(targetSignature, out var oppositeValue);

        if (!oppositeFound) return;

        Value = oppositeValue;
    }

    private void UpdateValue()
    {
        if (Table is null) return;

        var currentSortLabel = Table.Context.CurrentSortLabel;

        if (currentSortLabel is null || currentSortLabel.SortDirection == MudBlazor.SortDirection.None)
        {
            SortDirection = MudBlazor.SortDirection.Ascending;
            if (Value is not null) Value = null;
            return;
        }

        if (currentSortLabel.SortLabel is null)
        {
            SortDirection = currentSortLabel.SortDirection == MudBlazor.SortDirection.Descending
                ? MudBlazor.SortDirection.Descending
                : MudBlazor.SortDirection.Ascending;

            if (Value is not null) Value = null;
            return;
        }

        if (_previousValueSignature is not null
            && _previousValueSignature.SortLabel == currentSortLabel.SortLabel
            && _previousValueSignature.SortDirection == currentSortLabel.SortDirection)
            return;

        var fullMatchSignature = new ValueSignature(currentSortLabel.SortLabel, currentSortLabel.SortDirection);
        _previousValueSignature = fullMatchSignature;
        var fullMatchFound = _signatureMap.TryGetValue(fullMatchSignature, out var fullMatchValue);

        if (fullMatchFound)
        {
            Value = fullMatchValue;
            return;
        }

        var sortLabelMatchSignature = new ValueSignature(currentSortLabel.SortLabel, null);
        var sortLabelMatchFound = _signatureMap.TryGetValue(sortLabelMatchSignature, out var sortLabelMatchValue);

        if (sortLabelMatchFound)
        {
            SortDirection = currentSortLabel.SortDirection;
            Value = sortLabelMatchValue;
            return;
        }

        if (Value is not null) Value = null;
    }

    private record ValueSignature(string? SortLabel, SortDirection? SortDirection);
}
