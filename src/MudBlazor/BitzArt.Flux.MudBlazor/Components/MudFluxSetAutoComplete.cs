using BitzArt.Flux;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

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
    public Func<string, CancellationToken, object>? GetParametersFunc { get; set; }

    /// <inheritdoc cref="MudAutocomplete{T}.SearchFunc"/>
    public new Func<string, CancellationToken, Task<IEnumerable<T>>> SearchFunc
    {
        get
        {
            return base.SearchFunc;
        }
        set
        {
            throw new InvalidOperationException($"{nameof(MudFluxSetAutoComplete<T>)} does not allow configuring SearchFunc. Use {nameof(GetParametersFunc)} instead");
        }
    }

    [Inject]
    private IServiceProvider _serviceProvider { get; set; } = null!;

    private IFluxSetContext<T>? _context;
    private IFluxSetContext<T> Context
    {
        get
        {
            if (_context is not null) return _context;

            var flux = _serviceProvider.GetRequiredService<IFluxContext>();

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

    private Task<IEnumerable<T>> HandleSearchAsync(string searchText, CancellationToken cancellationToken)
    {
        if (SearchHandler is null)
            return SearchAsync(searchText, cancellationToken);

        return SearchHandler.Invoke(SearchAsync(searchText, cancellationToken));
    }

    private async Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var parameters = await GetParametersAsync(searchText, cancellationToken);
        var page = await Context.GetPageAsync(0, MaxItems ?? 10, parameters);

        return page.Items!;
    }

    private async Task<object[]?> GetParametersAsync(string searchText, CancellationToken cancellationToken)
    {
        if (GetParametersFunc is null)
        {
            return null;
        }

        var funcResult = GetParametersFunc.Invoke(searchText, cancellationToken);

        return funcResult switch
        {
            IEnumerable<object> parameters => parameters.ToArray(),
            Task<object[]> task => await task,
            Task<IEnumerable<object>> task => (await task).ToArray(),
            _ => throw new InvalidOperationException($"The result of GetParameters function should either be {nameof(IEnumerable<object>)}, {nameof(Task<object[]>)} or {nameof(Task<IEnumerable<object>>)}.")
        };
    }
}
