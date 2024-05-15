using BitzArt.Blazor.MVVM;
using BitzArt.Pagination;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksPageViewModel : ViewModel
{
    public BooksProvider BooksProvider;
    public PaginationState PaginationState;

    public BooksPageViewModel(IFluxSetContext<Book> booksSet)
    {
        PaginationState = new() { ItemsPerPage = 5 };
        BooksProvider = new(this, booksSet, PaginationState);
    }
    public string? AuthorId { get; set; }
    public string? RandomText { get; set; }
}

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

    protected override async Task<object[]> ConfigureParametersAsync(FluxSortingInfo sort, GridItemsProviderRequest<Book> request)
    {
        if (_viewModel.AuthorId == "2") await PaginationState.SetCurrentPageIndexAsync(0);

        var query = NewQuery();

        if (!string.IsNullOrWhiteSpace(_viewModel.AuthorId))
            query.Add("authorId", _viewModel.AuthorId!);
        if (!string.IsNullOrWhiteSpace(sort.OrderBy))
            query.Add("sort", sort.OrderBy);
        if (sort.Direction.HasValue)
            query.Add("desc", sort.Direction.Value == OrderDirection.Descending ? "true" : "false");

        var queryString = query.ToQueryString();

        return [queryString];
    }
}