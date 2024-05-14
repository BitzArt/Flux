using BitzArt.Pagination;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux;

public class FluxItemsProvider<TModel> where TModel : class
{
    private IFluxSetContext<TModel>? _fluxSet;

    public FluxItemsProvider(IFluxSetContext<TModel> fluxSet)
    {
        _fluxSet = fluxSet;
    }

    public GridItemsProvider<TModel> GetItems => new(GetItemsAsync);

    private async ValueTask<GridItemsProviderResult<TModel>> GetItemsAsync(GridItemsProviderRequest<TModel> request)
    {
        var pageRequest = new PageRequest(request.StartIndex, request.Count);

        var page = await _fluxSet!.GetPageAsync(pageRequest, request.CancellationToken);

        return GridItemsProviderResult.From(
                items: page.Data!.ToList(),
                totalItemCount: page.Total!.Value);
    }
}
