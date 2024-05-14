using BitzArt.Pagination;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux;

public class FluxItemsProvider<TModel>(IFluxSetContext<TModel> fluxSet)
    where TModel : class
{
    public GridItemsProvider<TModel> GetItems => new(GetItemsAsync);

    private async ValueTask<GridItemsProviderResult<TModel>> GetItemsAsync(GridItemsProviderRequest<TModel> request)
    {
        var pageRequest = new PageRequest(request.StartIndex, request.Count);

        pageRequest = ConfigurePageRequest(pageRequest);
        var parameters = ConfigureParameters(request);

        var page = await fluxSet!.GetPageAsync(
            pageRequest,
            //request.CancellationToken,
            parameters: parameters);

        return ConfigureResult(page);
    }

    protected virtual PageRequest ConfigurePageRequest(PageRequest pageRequest) => pageRequest;

    protected virtual object[] ConfigureParameters(GridItemsProviderRequest<TModel> request) => [];

    protected virtual GridItemsProviderResult<TModel> ConfigureResult(PageResult<TModel> page)
        => GridItemsProviderResult.From(items: page.Data!.ToList(), totalItemCount: page.Total!.Value);
}
