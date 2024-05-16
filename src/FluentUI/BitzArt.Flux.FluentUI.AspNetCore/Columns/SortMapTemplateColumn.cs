using Microsoft.AspNetCore.Components;

namespace Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// PropertyColumn with sorting bindings for Flux.
/// </summary>
public class SortMapTemplateColumn<TGridItem, TSortValue> : TemplateColumn<TGridItem>, ISortMapColumn<TSortValue>
{
    /// <summary>
    /// The value to be used for sorting.
    /// </summary>
    [Parameter, EditorRequired] public required TSortValue SortValue { get; set; }
}
