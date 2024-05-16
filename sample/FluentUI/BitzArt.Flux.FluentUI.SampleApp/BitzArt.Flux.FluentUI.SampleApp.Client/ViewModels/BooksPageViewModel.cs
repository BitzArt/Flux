using BitzArt.Blazor.MVVM;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksPageViewModel : ViewModel<BooksPageState>
{
    public BooksProvider BooksProvider;

    public async Task AuthorIdChanged(string? authorId)
    {
        State.AuthorId = authorId;
        BooksProvider.AuthorId = authorId;
        await BooksProvider.SetPageAsync(1);
    }

    public BooksPageViewModel(BooksProvider booksProvider)
    {
        BooksProvider = booksProvider;

        // Whenever another request is made, save it to ViewModel's state
        BooksProvider.OnAfterRequest += (request) =>
        {
            State.LastRequest = request;
            StateHasChanged(); // Notify ComponentStateContainer
        };
    }

    public override void OnStateRestored()
    {
        base.OnStateRestored();

        // Whenever state is restored, supply the last request to the provider
        BooksProvider.RestoreLastRequest(State.LastRequest);
        BooksProvider.AuthorId = State.AuthorId;
    }
}

public class BooksPageState
{
    public string? AuthorId { get; set; }
    public string? RandomText { get; set; }
    public FluxPageRequestRecord<Book>? LastRequest { get; set; }
}