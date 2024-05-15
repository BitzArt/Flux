using System.Text.Json.Serialization;

namespace BitzArt.Flux;

public class FluxSortingInfo
{
    [JsonPropertyName("orderBy")]
    public string? OrderBy { get; set; } = null;

    [JsonPropertyName("direction")]
    public OrderDirection? Direction { get; set; } = null;

    public FluxSortingInfo() { }

    public FluxSortingInfo(string? orderBy, OrderDirection? direction)
    {
        OrderBy = orderBy;
        Direction = direction;
    }

    public bool Compare(FluxSortingInfo other)
    {
        return OrderBy == other.OrderBy && Direction == other.Direction;
    }
}