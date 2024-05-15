using BitzArt.Blazor.MVVM;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class BooksPageViewModel : ViewModel<BooksPageState>
{
    private readonly RenderingEnvironment _renderingEnvironment;
    public BooksProvider BooksProvider;
    public PaginationState PaginationState;

    public async Task AuthorIdChanged(string? authorId)
    {
        State.AuthorId = authorId;
        await PaginationState.SetCurrentPageIndexAsync(0);
    }

    public BooksPageViewModel(IFluxSetContext<Book> booksSet, RenderingEnvironment renderingEnvironment)
    {
        PaginationState = new() { ItemsPerPage = 5 };
        BooksProvider = new(this, booksSet, PaginationState);
        _renderingEnvironment = renderingEnvironment;

        // Whenever another request is made, save it to ViewModel's state
        BooksProvider.OnAfterRequest += (request) =>
        {
            State.LastRequest = request;
            StateHasChanged(); // Update ComponentStateContainer
        };
    }

    public override void OnStateRestored()
    {
        base.OnStateRestored();

        // Whenever state is restored, supply the last request to the provider
        BooksProvider.RestoreLastRequest(State.LastRequest);
    }
}

public class BooksPageState
{
    public string? AuthorId { get; set; }
    public string? RandomText { get; set; }
    public FluxPageRequestRecord<Book>? LastRequest { get; set; }
}