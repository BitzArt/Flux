using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksProvider(IFluxContext flux)
    : FluxItemsProvider<Book>(flux)
{
    protected override int DefaultPageSize => 10;

    public int? SelectedAuthorId { get; set; }

    protected override Task<object[]> ConfigureParametersAsync(FluxSortingInfo sort, GridItemsProviderRequest<Book> request)
    {
        var query = QueryString.New();

        if (SelectedAuthorId.HasValue)
            query.Add("authorId", SelectedAuthorId!.Value.ToString());
        if (sort.OrderBy is string orderByString && !string.IsNullOrWhiteSpace(orderByString))
            query.Add("sort", orderByString);
        if (sort.Direction.HasValue && sort.Direction.Value == OrderDirection.Descending)
            query.Add("desc", "true");

        var queryString = query.ToQueryString();

        return Task.FromResult(new object[] { queryString });
    }
}