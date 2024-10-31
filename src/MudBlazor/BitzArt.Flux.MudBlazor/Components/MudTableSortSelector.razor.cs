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
    /// The text displayed in the input if no <see cref="Value"/> is specified. <br/>
    /// Default is "Sort by".
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "Sort by";

    /// <summary>
    /// The apperiance varation of the input.
    /// Default is <see cref="Variant.Text"/>.
    /// </summary>
    [Parameter]
    public Variant SelectVariant { get; set; } = Variant.Text;

    /// <summary>
    /// The amount of vertical spacing for the input.
    /// Default is <see cref="Margin.Normal"/>.
    /// </summary>
    [Parameter]
    public Margin SelectMargin { get; set; } = Margin.Normal;

    /// <summary>
    /// The apperiance varation of the button. 
    /// Default is <see cref="Variant.Text"/>.
    /// </summary>
    [Parameter]
    public Variant ButtonVariant { get; set; } = Variant.Text;

    /// <summary>
    /// The size of the button. 
    /// Default is <see cref="Size.Medium"/>.
    /// </summary>
    [Parameter]
    public Size ButtonSize { get; set; } = Size.Medium;

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
        _isQuiescent = true;
        _select.ChildContent = _quiescentChildContent;
        StateHasChanged();
    }

    public void AddItem(MudTableSortSelectorItem<T> item)
    {
        _items.Add(item);
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

        await OnValueChangedAsync(Value);
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