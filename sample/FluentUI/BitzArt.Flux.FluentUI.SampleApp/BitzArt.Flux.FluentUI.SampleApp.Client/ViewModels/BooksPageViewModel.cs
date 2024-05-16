using BitzArt.Blazor.MVVM;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksPageViewModel : ViewModel<BooksPageState>
{
    public BooksProvider BooksProvider;
    private readonly IFluxSetContext<Author> _authors;

    public async Task OnAuthorSelectedAsync(Author author)
    {
        State.SelectedAuthorId = author.Id;
        BooksProvider.SelectedAuthorId = author.Id;
        await BooksProvider.ResetPaginationAsync();
    }

    public BooksPageViewModel(BooksProvider booksProvider, IFluxSetContext<Author> authors)
    {
        BooksProvider = booksProvider;
        _authors = authors;

        // Whenever another request is made, save it to ViewModel's state
        BooksProvider.OnAfterRequest += (request) =>
        {
            State.LastRequest = request;
            StateHasChanged(); // Notify ComponentStateContainer
        };
    }

    public override async Task InitializeStateAsync()
    {
        await base.InitializeStateAsync();
        await LoadAuthorsAsync();

        return;
    }

    private async Task LoadAuthorsAsync()
    {
        var authors = (await _authors.GetAllAsync()).ToList();
        authors.Insert(0, new Author { Id = null, Name = "All Authors" });
        State.Authors = authors;
    }

    public override void OnStateRestored()
    {
        base.OnStateRestored();

        // Whenever state is restored, supply the last request to the provider
        BooksProvider.RestoreLastRequest(State.LastRequest);
        BooksProvider.SelectedAuthorId = State.SelectedAuthorId;
    }
}

public class BooksPageState
{
    public IEnumerable<Author> Authors { get; set; } = [];
    public int? SelectedAuthorId { get; set; } = null;

    public string? RandomText { get; set; }
    public FluxPageRequestRecord<Book>? LastRequest { get; set; }
}