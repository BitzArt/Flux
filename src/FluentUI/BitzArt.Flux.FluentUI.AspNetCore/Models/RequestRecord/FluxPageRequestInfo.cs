using BitzArt.Pagination;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

/// <summary>
/// Represents a page request for a Flux request.
/// </summary>
public class FluxPageRequestInfo : FluxRequestInfo
{
    /// <summary>
    /// The request's pagination information.
    /// </summary>
    [JsonPropertyName("pageRequest")]
    public PageRequest PageRequest { get; set; } = null!;

    /// <summary>
    /// The request's sorting information.
    /// </summary>
    [JsonPropertyName("sorting")]
    public FluxSortingInfo Sorting { get; set; } = null!;

    /// <summary>
    /// Creates a new instance of <see cref="FluxPageRequestInfo"/>.
    /// </summary>
    public FluxPageRequestInfo() { }

    /// <summary>
    /// Creates a new instance of <see cref="FluxPageRequestInfo"/>.
    /// </summary>
    /// <param name="pageRequest"></param>
    /// <param name="sorting"></param>
    /// <param name="parameters"></param>
    public FluxPageRequestInfo(PageRequest pageRequest, FluxSortingInfo sorting, object[] parameters) : base(parameters)
    {
        PageRequest = pageRequest;
        Sorting = sorting;
    }

    /// <summary>
    /// Creates a new instance of <see cref="FluxPageRequestInfo"/>.
    /// </summary>
    /// <param name="pageRequest"></param>
    /// <param name="sorting"></param>
    /// <param name="parameters"></param>
    public FluxPageRequestInfo(PageRequest pageRequest, FluxSortingInfo sorting, IEnumerable<string> parameters) : base(parameters)
    {
        PageRequest = pageRequest;
        Sorting = sorting;
    }

    /// <summary>
    /// Compares the two instances of <see cref="FluxPageRequestInfo"/>.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
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
