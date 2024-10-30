using Microsoft.AspNetCore.Components;

namespace MudBlazor;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class MudTableSortSelectorItem<T>
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? SortLabel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public SortDirection? SortDirection { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [CascadingParameter]
    private MudTableSortSelector<T>? ParentSelector { get; set; }

    internal MudTableSortSelectorItemValue<T> Value = null!;

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        if (ParentSelector is null)
            throw new InvalidOperationException($"{nameof(MudTableSortSelectorItem<T>)} requires a parent {nameof(MudTableSortSelector<T>)} component.");
        
        Value = new(GetSortLabel(), SortDirection, ParentSelector!);

        ParentSelector!.AddItem(this);
    }

    private MudTableSortLabel<T> GetSortLabel()
    {
        if (ParentSelector!.Table is null) return CreateNewSortLabel();

        var sortLabel = ParentSelector!.Table.Context.SortLabels.FirstOrDefault(x => x.SortLabel == SortLabel);

        if (sortLabel is null) return CreateNewSortLabel();

        return sortLabel;
    }

    private MudTableSortLabel<T> CreateNewSortLabel()
    {
        return new MudTableSortLabel<T>
        {
            SortLabel = SortLabel,
            SortDirection = SortDirection.HasValue ? SortDirection.Value : MudBlazor.SortDirection.None
        };
    }
}
