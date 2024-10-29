using Microsoft.AspNetCore.Components;

namespace MudBlazor;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class MudTableSortSelector<T>
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public IEnumerable<MudTableSortLabel<T>>? SortLabels { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter, EditorRequired]
    public required RenderFragment ChildContent { get; set; }


    public MudTableSortLabel<T>? CurrentSortLabel 
    {
        get => _currentSortLabel; 
        set => _currentSortLabel = value;
    }
    
    private MudTableSortLabel<T>? _currentSortLabel;



    private SortDirection _currentSortDirection;



    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string AscendingSortIcon { get; set; } = Icons.Material.Filled.ArrowUpward;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string DescendingSortIcon { get; set; } = Icons.Material.Filled.ArrowDownward;

    private string _currentSortIcon => _currentSortDirection switch
    {
        SortDirection.Ascending => AscendingSortIcon,
        SortDirection.Descending => DescendingSortIcon,
        SortDirection.None => AscendingSortIcon, // TODO: Handle this case. Consider that default sorting may be descending.
        _ => throw new InvalidOperationException($"Invalid sort direction: {_currentSortDirection}.")
    };

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Variant Variant { get; set; } = Variant.Text;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Margin Margin { get; set; } = Margin.Normal;


    private void OnSortingChanged(MudTableSortLabel<T>? sortLabel, SortDirection? sortDirection)
    {
        _currentSortLabel = sortLabel;
    }




    //[Parameter]
    //public RenderFragment? ChildContent { get; set; }

    //private SortDirection? _currentSortDirection
    //{
    //    get
    //    {
    //        if (_currentSortLabel is null || _currentSortLabel.SortDirection == SortDirection.None) 
    //            return null;

    //        return _currentSortLabel.SortDirection;
    //    }
    //}

    //[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
    //private async Task ChangeSortByAsync(MudTableSortLabel<T>? sortLabel)
    //{
    //    if (_currentSortLabel == sortLabel) return;

    //    if (Table is null) throw new InvalidOperationException(
    //       "Table component must be forwarded to MudTableSortSelector for it to be able to set sorting.");

    //    foreach (var label in Table.Context.SortLabels)
    //    {
    //        label.SortDirection = SortDirection.None;
    //    }

    //    if (sortLabel is null)
    //    {
    //        Table.Context.CurrentSortLabel!.SortDirection = SortDirection.None;
    //        await Table.Context.SetSortFunc(Table.Context.CurrentSortLabel).IgnoreCancellation();
    //    }
    //    else
    //    {
    //        sortLabel.SortDirection = _currentSortDirection.HasValue ? _currentSortDirection.Value : SortDirection.Ascending;
    //        await Table.Context.SetSortFunc(sortLabel).IgnoreCancellation();
    //    }
    //}

    //[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
    //private async Task ChangeSortDirtectionAsync(SortDirection? sortDirection)
    //{
    //    if (_currentSortDirection == sortDirection) return;

    //    if (Table is null) throw new InvalidOperationException(
    //       "Table component must be forwarded to MudTableSortSelector for it to be able to set sorting.");

    //    foreach (var sortLabel in Table.Context.SortLabels)
    //    {
    //        sortLabel.SortDirection = SortDirection.None;
    //    }

    //    if (Table.Context.CurrentSortLabel is not null)
    //    {
    //        Table.Context.CurrentSortLabel.SortDirection = sortDirection.HasValue ? sortDirection.Value : SortDirection.None;
    //        await Table.Context.SetSortFunc(Table.Context.CurrentSortLabel).IgnoreCancellation();
    //    }
    //}
}