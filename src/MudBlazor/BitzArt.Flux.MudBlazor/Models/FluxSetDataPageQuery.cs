using MudBlazor;

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
    public object[]? Parameters { get; set; } = null!;

    /// <summary>
    /// Result of the request.
    /// </summary>
    public TableData<TModel> Result { get; set; } = null!;
}
