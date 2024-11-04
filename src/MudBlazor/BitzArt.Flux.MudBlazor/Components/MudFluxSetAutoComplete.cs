﻿using BitzArt.Flux;
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
    /// The data context used to retrieve and manage data items displayed in the autocomplete suggestions.
    /// </summary>
    [Parameter]
    public IFluxSetContext<T> Context { get; set; } = null!;

    /// <summary>
    /// A function that retrieves additional parameters for querying the data context based on the user's search text.
    /// </summary>
    [Parameter]
    public Func<string, CancellationToken, Task<object[]>>? GetParameters { get; set; }

    /// <inheritdoc cref="MudAutocomplete{T}.SearchFunc"/>
    public new Func<string, CancellationToken, Task<IEnumerable<T>>> SearchFunc
    {
        get
        {
            return base.SearchFunc;
        }
        set
        {
            throw new InvalidOperationException($"{nameof(MudFluxSetAutoComplete<T>)} does not allow configuring SearchFunc. Use {nameof(GetParameters)} instead");
        }
    }

    [Inject]
    private IServiceProvider ServiceProvider { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="MudFluxSetAutoComplete{T}"/> class and sets up the default search function.
    /// </summary>
    public MudFluxSetAutoComplete()
    {
        base.SearchFunc = SearchAsync;
    }

    /// <summary>
    /// Called when the component is initialized; ensures that the required data context is available, injecting it if necessary.
    /// </summary>
    protected override void OnInitialized()
    {
        Context ??= ServiceProvider.GetRequiredService<IFluxSetContext<T>>();
    }

    private async Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var parameters = await GetParametersAsync(searchText, cancellationToken);
        var page = await Context.GetPageAsync(0, MaxItems ?? 10, parameters);

        return page.Data!;
    }

    private async Task<object[]?> GetParametersAsync(string searchText, CancellationToken cancellationToken)
    {
        if (GetParameters is null)
        {
            return null;
        }

        return await GetParameters.Invoke(searchText, cancellationToken);
    }
}