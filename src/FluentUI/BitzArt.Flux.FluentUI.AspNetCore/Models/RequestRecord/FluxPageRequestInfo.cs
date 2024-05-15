using BitzArt.Pagination;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

public class FluxPageRequestInfo : FluxRequestInfo
{
    [JsonPropertyName("pageRequest")]
    public PageRequest PageRequest { get; set; } = null!;

    [JsonPropertyName("sorting")]
    public FluxSortingInfo Sorting { get; set; } = null!;

    public FluxPageRequestInfo() { }

    public FluxPageRequestInfo(PageRequest pageRequest, FluxSortingInfo sorting, object[] parameters) : base(parameters)
    {
        PageRequest = pageRequest;
        Sorting = sorting;
    }

    public FluxPageRequestInfo(PageRequest pageRequest, FluxSortingInfo sorting, IEnumerable<string> parameters) : base(parameters)
    {
        PageRequest = pageRequest;
        Sorting = sorting;
    }

    public override bool Compare(FluxRequestInfo other)
    {
        if (other is not FluxPageRequestInfo otherPageRequest) return false;

        return base.Compare(other) && PageRequest.Compare(otherPageRequest.PageRequest);
    }
}

internal static class PageRequestExtensions
{
    public static bool Compare(this PageRequest pageRequest, PageRequest other)
    {
        return pageRequest.Offset == other.Offset && pageRequest.Limit == other.Limit;
    }
}
