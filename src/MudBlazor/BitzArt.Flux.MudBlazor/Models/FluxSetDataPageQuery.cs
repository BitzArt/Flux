using MudBlazor;

namespace BitzArt.Flux.MudBlazor;

internal record FluxSetDataPageQuery<TModel>
    where TModel : class
{
    public TableState TableState { get; set; } = null!;

    public object[]? Parameters { get; set; } = null!;

    public TableData<TModel> Result { get; set; } = null!;
}
