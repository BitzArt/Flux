using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Web;

namespace BitzArt.Flux.MudBlazorSample.Client.Pages;

public partial class BooksPage : ComponentBase
{
    private MudTable<Book> _table = null!;

    private IEnumerable<Author> _authors = null!;
    private Author? _selectedAuthor;

    private string? _search;

    [Inject] private IFluxSetContext<Author> Authors { get; set; } = null!;
    [Inject] private IFluxSetContext<Book> BooksSet { get; set; } = null!;

    private const int _pageSize = 10;

    private bool _initialized = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _authors = await Authors.GetAllAsync();
        _initialized = true;

        await InvokeAsync(StateHasChanged);
    }

    private async Task<TableData<Book>> LoadBooksAsync(TableState state, CancellationToken token)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (_selectedAuthor is not null) query["authorId"] = _selectedAuthor.Id.ToString();
        if (!string.IsNullOrWhiteSpace(state.SortLabel)) query["order"] = state.SortLabel;
        if (state.SortDirection == SortDirection.Descending) query["desc"] = "true";
        if (!string.IsNullOrWhiteSpace(_search)) query["search"] = _search;

        var queryString = query.Count > 0 ? $"?{query}" : string.Empty;

        var page = await BooksSet.GetPageAsync(state.Page * state.PageSize, state.PageSize, queryString);

        return new TableData<Book>
        {
            Items = page.Data,
            TotalItems = page.Total!.Value
        };
    }

    private async Task OnAuthorSelectedAsync(Author author)
    {
        _selectedAuthor = author;
        if (_table is not null)
        {
            _table!.CurrentPage = 0;
            await _table!.ReloadServerData();
        }
    }

    private async Task OnSearchAsync(string value)
    {
        _search = value;
        if (_table is not null)
        {
            _table!.CurrentPage = 0;
            await _table!.ReloadServerData();
        }
    }
}
