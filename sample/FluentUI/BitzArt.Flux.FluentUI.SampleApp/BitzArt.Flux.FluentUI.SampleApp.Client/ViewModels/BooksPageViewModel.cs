using BitzArt.Blazor.MVVM;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Linq.Expressions;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksPageViewModel : ViewModel
{
    public BooksProvider BooksProvider;
    public PaginationState PaginationState = new() { ItemsPerPage = 5 };

    public BooksPageViewModel(IFluxSetContext<Book> booksSet)
    {
        BooksProvider = new(this, booksSet);
    }
    public string? AuthorId { get; set; }

    public string? RandomText { get; set; }
}

public class BooksProvider(
    BooksPageViewModel viewModel,
    IFluxSetContext<Book> booksSet)
    : FluxItemsProvider<Book>(booksSet)
{
    protected override object[] ConfigureParameters(GridItemsProviderRequest<Book> request)
    {
        var sortingExpression = request.GetSortingColumnPropertyExpression();

        var authorId = viewModel.AuthorId;
        var query = string.IsNullOrWhiteSpace(authorId) ? string.Empty : $"?authorId={authorId}";
        return [query];
    }
}