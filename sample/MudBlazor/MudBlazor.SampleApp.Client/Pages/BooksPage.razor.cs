using BitzArt.Flux;
using BitzArt.Flux.MudBlazor;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace MudBlazor.SampleApp.Client.Pages;

public partial class BooksPage : ComponentBase
{
    private IEnumerable<Author> _authors = null!;
    private Author? _selectedAuthor;

    private string? _search;

    [Inject] private IFluxSetContext<Author> Authors { get; set; } = null!;
    [Inject] private IFluxSetDataProvider<Book> BooksDataProvider { get; set; } = null!;

    private bool _initialized = false;

    protected override async Task OnInitializedAsync()
    {
        BooksDataProvider.GetParameters = GetBooksParameters;

        await base.OnInitializedAsync();
        _authors = await Authors.GetAllAsync();
        _initialized = true;

        await InvokeAsync(StateHasChanged);
    }

    private object[] GetBooksParameters(TableState state)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (_selectedAuthor is not null) query["authorId"] = _selectedAuthor.Id.ToString();
        if (!string.IsNullOrWhiteSpace(state.SortLabel) && state.SortDirection != SortDirection.None) 
            query["order"] = state.SortLabel;
        if (state.SortDirection == SortDirection.Descending) query["desc"] = "true";
        if (!string.IsNullOrWhiteSpace(_search)) query["search"] = _search;

        var queryString = query.Count > 0 ? $"?{query}" : string.Empty;

        return [queryString];
    }

    private async Task OnAuthorSelectedAsync(Author author)
    {
        _selectedAuthor = author;
        await BooksDataProvider.ResetAndReloadAsync();
    }

    private async Task OnSearchAsync(string value)
    {
        _search = value;
        await BooksDataProvider.ResetPageAndReloadAsync();
    }
}
