using System.Text.Json.Serialization;

namespace BitzArt.Flux;

/// <summary>
/// Represents sorting information for a Flux request.
/// </summary>
public class FluxSortingInfo
{
    /// <summary>
    /// The field to order by.
    /// </summary>
    [JsonPropertyName("orderBy")]
    public object? OrderBy { get; set; } = null;

    /// <summary>
    /// The order direction.
    /// </summary>
    [JsonPropertyName("direction")]
    public OrderDirection? Direction { get; set; } = null;

    /// <summary>
    /// Creates a new instance of <see cref="FluxSortingInfo"/>.
    /// </summary>
    public FluxSortingInfo() { }

    /// <summary>
    /// Creates a new instance of <see cref="FluxSortingInfo"/>.
    /// </summary>
    /// <param name="orderBy"></param>
    /// <param name="direction"></param>
    public FluxSortingInfo(object? orderBy, OrderDirection? direction)
    {
        OrderBy = orderBy;
        Direction = direction;
    }

    /// <summary>
    /// Compares the two instances of <see cref="FluxSortingInfo"/>.
    /// </summary>
    /// <param name="other">The instance to compare with.</param>
    /// <returns>True if the instances are equal, otherwise false.</returns>
    public bool Compare(FluxSortingInfo other)
    {
        return OrderBy == other.OrderBy && Direction == other.Direction;
    }
}