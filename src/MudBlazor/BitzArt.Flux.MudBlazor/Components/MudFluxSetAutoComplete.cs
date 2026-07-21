using BitzArt.Flux;
using BitzArt.Pagination;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace MudBlazor;

/// <summary>
/// An autocomplete component that integrates with a FluxSet context to dynamically load and filter data items based on user input.
/// </summary>
/// <typeparam name="T">The type of the data item.</typeparam>
public class MudFluxSetAutoComplete<T> : MudAutocomplete<T> where T : class
{
    /// <summary>
    /// Name of the Flux service to resolve set context for.
    /// </summary>
    [Parameter]
    public string? ServiceName { get; set; }

    /// <summary>
    /// Name of the Flux set to resolve context for.
    /// </summary>
    [Parameter]
    public string? SetName { get; set; }

    /// <summary>
    /// Handle function that processes a search request and returns the result. <br/>
    /// If not provided, the default search function will be used.
    /// </summary>
    [Parameter]
    public Func<Task<IEnumerable<T>>, Task<IEnumerable<T>>>? SearchHandler { get; set; }

    /// <summary>
    /// A function that retrieves search request parameters.
    /// </summary>
    [Parameter]
    public Func<string, IOperationParameterCollection>? GetParameters { get; set; }

    /// <summary>
    /// A function that returns a task that retrieves search request parameters.
    /// </summary>
    [Parameter]
    public Func<string, CancellationToken, Task<IOperationParameterCollection>>? GetParametersAsync { get; set; }

    /// <inheritdoc cref="MudAutocomplete{T}.SearchFunc"/>
    public new Func<string?, CancellationToken, Task<IEnumerable<T>>?>? SearchFunc
    {
        get
        {
            return base.SearchFunc;
        }
        set
        {
            throw new InvalidOperationException($"{nameof(MudFluxSetAutoComplete<T>)} does not allow configuring SearchFunc. Use {nameof(GetParameters)} or {nameof(GetParametersAsync)} instead");
        }
    }

    [Inject]
    private IServiceProvider ServiceProvider { get; set; } = null!;

    private IFluxSetContext<T>? _context;
    private IFluxSetContext<T> Context
    {
        get
        {
            if (_context is not null) return _context;

            var flux = ServiceProvider.GetRequiredService<IFluxContext>();

            _context = flux.Set<T>(ServiceName, SetName);

            return _context;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MudFluxSetAutoComplete{T}"/> class and sets up the default search function.
    /// </summary>
    public MudFluxSetAutoComplete()
    {
        base.SearchFunc = HandleSearchAsync;
    }

    private Task<IEnumerable<T>> HandleSearchAsync(string? searchText, CancellationToken cancellationToken)
    {
        searchText ??= string.Empty;

        if (SearchHandler is null)
            return SearchAsync(searchText, cancellationToken);

        return SearchHandler.Invoke(SearchAsync(searchText, cancellationToken));
    }

    private async Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var parameters = await RetrieveParametersAsync(searchText, cancellationToken);
        var descriptor = new GetPageOperationDescriptor(new PageRequest(0, MaxItems ?? 10), parameters);
        var page = await Context.GetPageAsync(descriptor, cancellationToken);

        return page.Items!;
    }

    private async Task<IOperationParameterCollection?> RetrieveParametersAsync(string searchText, CancellationToken cancellationToken)
    {
        var parameters = GetParameters is not null
            ? GetParameters.Invoke(searchText)
            : GetParametersAsync is not null
                ? await GetParametersAsync.Invoke(searchText, cancellationToken)
                : null;

        return parameters;
    }
}
