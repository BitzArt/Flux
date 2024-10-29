using Microsoft.AspNetCore.Components;

namespace MudBlazor;

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
    private MudTableSortSelector<T>? ParentSelector { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        ParentSelector?.AddItem(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public MudTableSortLabel<T> GetSortLabel()
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

        if (ParentSelector is not null && ParentSelector.SortDirection.HasValue)
            return ParentSelector.SortDirection.Value;

        return MudBlazor.SortDirection.None;
    }
}
