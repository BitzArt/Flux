using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksProvider
    : FluxItemsProvider<Book>
{
    protected override int DefaultPageSize => 5;

    public string? AuthorId { get; set; }

    public BooksProvider(IFluxContext flux) : base(flux)
    {
        SortMap.Add(x => x.Id, "id");
        SortMap.Add(x => x.Title, "title");
        SortMap.Add(x => x.AuthorId, "authorId");
        SortMap.Add(x => x.Rating!.Rating, "rating");
        SortMap.Add(x => x.Rating!.RatingCount, "ratingCount");
    }

    protected override Task<object[]> ConfigureParametersAsync(FluxSortingInfo sort, GridItemsProviderRequest<Book> request)
    {
        var query = QueryString.New();

        if (!string.IsNullOrWhiteSpace(AuthorId))
            query.Add("authorId", AuthorId!);
        if (!string.IsNullOrWhiteSpace(sort.OrderBy))
            query.Add("sort", sort.OrderBy);
        if (sort.Direction.HasValue && sort.Direction.Value == OrderDirection.Descending)
            query.Add("desc", "true");

        var queryString = query.ToQueryString();

        return Task.FromResult(new object[] { queryString });
    }
}