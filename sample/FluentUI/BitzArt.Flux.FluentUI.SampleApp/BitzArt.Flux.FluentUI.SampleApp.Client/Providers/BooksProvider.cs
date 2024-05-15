using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksProvider
    : FluxItemsProvider<Book>
{
    private readonly BooksPageViewModel _viewModel;

    public BooksProvider(
        BooksPageViewModel viewModel,
        IFluxSetContext<Book> booksSet,
        PaginationState paginationState) : base(booksSet, paginationState)
    {
        _viewModel = viewModel;

        SortMap.Add(x => x.Id, "id");
        SortMap.Add(x => x.Title, "title");
        SortMap.Add(x => x.AuthorId, "authorId");
        SortMap.Add(x => x.Rating!.Rating, "rating");
        SortMap.Add(x => x.Rating!.RatingCount, "ratingCount");
    }

    protected override Task<object[]> ConfigureParametersAsync(FluxSortingInfo sort, GridItemsProviderRequest<Book> request)
    {
        var query = NewQuery();

        if (!string.IsNullOrWhiteSpace(_viewModel.State.AuthorId))
            query.Add("authorId", _viewModel.State.AuthorId!);
        if (!string.IsNullOrWhiteSpace(sort.OrderBy))
            query.Add("sort", sort.OrderBy);
        if (sort.Direction.HasValue && sort.Direction.Value == OrderDirection.Descending)
            query.Add("desc", "true");

        var queryString = query.ToQueryString();

        return Task.FromResult(new object[] { queryString });
    }
}