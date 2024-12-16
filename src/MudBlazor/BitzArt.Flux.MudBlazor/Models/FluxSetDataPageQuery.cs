using BitzArt.Json;
using BitzArt.Pagination;
using MudBlazor;
using System.Text.Json.Serialization;

namespace BitzArt.Flux.MudBlazor;

/// <summary>
/// Represents a page query for a data set.
/// </summary>
public record FluxSetDataPageQuery<TModel>
    where TModel : class
{
    /// <summary>
    /// Table state at the time of request.
    /// </summary>
    public TableState TableState { get; set; } = null!;

    /// <summary>
    /// Parameters for the request.
    /// </summary>
    [JsonConverter(typeof(ItemConverter<TypedObjectJsonConverter<object>>))]
    public object[]? Parameters { get; set; } = null!;

    /// <summary>
    /// Data returned by the request.
    /// </summary>
    public PageResult<TModel>? Data { get; set; }

    /// <summary>
    /// Result of the request.
    /// </summary>
    public TableData<TModel> GetTableData()
    {
        if (Data is null) throw new InvalidOperationException("Data is null.");

        return new()
        {
            Items = Data!.Items,
            TotalItems = Data!.Total!.Value
        };
    }
}
