using BitzArt.Blazor.MVVM;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksPageViewModel(IFluxSetContext<Book> booksSet) : ViewModel<BooksPageState>
{
    public BooksProvider BooksProvider = new(booksSet);
    public PaginationState PaginationState = new() { ItemsPerPage = 10 };
}

public class BooksPageState
{
    public int? AuthorId { get; set; }
}

public class BooksProvider : FluxItemsProvider<Book>
{
    public BooksProvider(IFluxSetContext<Book> booksSet) : base(booksSet)
    {
    }
}