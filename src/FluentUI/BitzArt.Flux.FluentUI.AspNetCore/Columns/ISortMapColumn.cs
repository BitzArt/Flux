namespace Microsoft.FluentUI.AspNetCore.Components;

internal interface ISortMapColumn<TSortValue>
{
    public TSortValue SortValue { get; set; }
}