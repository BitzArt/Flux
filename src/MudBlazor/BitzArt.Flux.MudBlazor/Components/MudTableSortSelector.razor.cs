using Microsoft.AspNetCore.Components;

namespace MudBlazor;

public partial class MudTableSortSelector<T>
{
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public bool HideSortButton { get; set; } = false;

    private bool _isQuiescent = false;
    private RenderFragment _childContent => _isQuiescent ? _quiescentChildContent : ChildContent;

    private RenderFragment _quiescentChildContent => builder =>
    {
        var counter = 0;
        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items.ElementAt(i);

            builder.OpenComponent<MudSelectItem<MudTableSortSelectorItemValue<T>>>(counter++);
            builder.AddAttribute(counter++, "Value", item.Value);
            builder.AddAttribute(counter++, "ChildContent", item.ChildContent);
            builder.CloseComponent();
        }
    };

    [Parameter]
    public EventCallback<MudTableSortLabel<T>> SortChanged { get; set; }

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
    public bool Clearable { get; set; }

    /// <summary>
    /// The apperiance varation of the input.
    /// </summary>
    [Parameter]
    public Variant InputVariant { get; set; }

    /// <summary>
    /// The amount of vertical spacing for the input.
    /// </summary>
    [Parameter]
    public Margin InputMargin { get; set; }

    /// <summary>
    /// CSS classes applied to the input.
    /// </summary>
    [Parameter]
    public string? InputClass { get; set; }

    /// <summary>
    /// The apperiance varation of the sort direction button.
    /// </summary>
    [Parameter]
    public Variant ButtonVariant { get; set; }

    /// <summary>
    /// The size of the sort direction button. 
    /// </summary>
    [Parameter]
    public Size ButtonSize { get; set; }

    /// <summary>
    /// CSS classes applied to the sort direction button.
    /// </summary>
    [Parameter]
    public string? ButtonClass { get; set; }

    private MudSelect<MudTableSortSelectorItemValue<T>> _select;

    public SortDirection? SortDirection { get; set; }

    internal MudTableSortSelectorItemValue<T>? Value
    {
        get => _value;
        set
        {
            _value = value;
            _ = OnValueChangedAsync(value);
        }

    }

    private MudTableSortSelectorItemValue<T>? _value;

    private ICollection<MudTableSortSelectorItem<T>> _items { get; set; } = [];

    private Dictionary<ValueSignature, MudTableSortSelectorItemValue<T>> _signatureMap { get; set; } = [];

    private record ValueSignature(string? SortLabel, SortDirection? SortDirection);

    //private Dictionary<MudTableSortLabel<T>,  MudTableSortSelectorItemValue<T>> _sortLabelMap { get; set; } = [];

    private bool _rememberSortDirection;

    protected override void OnInitialized()
    {
        if (!HideSortButton)
        {
            SortDirection = MudBlazor.SortDirection.Ascending;
            _rememberSortDirection = true;
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;

        _isQuiescent = true;
        _select.ChildContent = _quiescentChildContent;

        StateHasChanged();
    }

    public void AddItem(MudTableSortSelectorItem<T> item)
    {
        _items.Add(item);

        var signature = new ValueSignature(item.SortLabel, item.SortDirection);
        _signatureMap[signature] = item.Value;

        //_sortLabelMap[item.Value.SortLabel] = item.Value;
    }

    private async Task OnValueChangedAsync(MudTableSortSelectorItemValue<T>? value)
    {
        var sortLabel = GetSortLabel(value);
        await OnValueChangedAsync(sortLabel);
    }

    private async Task OnValueChangedAsync(MudTableSortLabel<T> sortLabel)
    {
        if (SortChanged.HasDelegate)
            await SortChanged.InvokeAsync(sortLabel);

        if (Table is not null)
            await Table.Context.SetSortFunc(sortLabel);

        await InvokeAsync(StateHasChanged);
    }

    private MudTableSortLabel<T> GetSortLabel(MudTableSortSelectorItemValue<T>? value)
    {
        //_selectedItem = item;

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
        SortDirection = SortDirection!.Value.Inverse();

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

    private ValueSignature? previousValueSignature;

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

        if (previousValueSignature is not null
            && previousValueSignature.SortLabel == currentSortLabel.SortLabel
            && previousValueSignature.SortDirection == currentSortLabel.SortDirection)
            return;

        var fullMatchSignature = new ValueSignature(currentSortLabel.SortLabel, currentSortLabel.SortDirection);
        previousValueSignature = fullMatchSignature;
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
}


public static class SortDirectionExtensions
{
    public static SortDirection Inverse(this SortDirection direction)
    {
        return direction switch
        {
            SortDirection.Ascending => SortDirection.Descending,
            SortDirection.Descending => SortDirection.Ascending,
            _ => throw new InvalidOperationException($"Sort direction '{direction}' can not be inverted.")
        };
    }
}

public static class Ext
{
    public static bool EqualValues<T>(this MudTableSortLabel<T> oldValue, MudTableSortLabel<T> newValue)
    {
        return oldValue.SortLabel == newValue.SortLabel && oldValue.SortDirection == newValue.SortDirection;
    }
}