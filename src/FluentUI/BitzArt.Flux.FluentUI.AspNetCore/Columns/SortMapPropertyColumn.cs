using Microsoft.AspNetCore.Components;

namespace Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// PropertyColumn with sorting bindings for Flux.
/// </summary>
public class SortMapPropertyColumn<TGridItem, TProp, TSortValue> : PropertyColumn<TGridItem, TProp>, ISortMapColumn<TSortValue>
{
    /// <summary>
    /// The value to be used for sorting.
    /// </summary>
    [Parameter, EditorRequired] public required TSortValue SortValue { get; set; }
}
