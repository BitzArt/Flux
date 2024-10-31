using BitzArt.Flux;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace MudBlazor;

public class MudFluxSetAutoComplete<T> : MudAutocomplete<T> where T : class
{
    [Parameter]
    public IFluxSetContext<T> Context { get; set; } = null!;

    [Parameter]
    public Func<string, CancellationToken, Task<object[]>>? GetParameters { get; set; }

    [Inject]
    public IServiceProvider ServiceProvider { get; set; }

    [Parameter]
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

    public MudFluxSetAutoComplete()
    {
        base.SearchFunc = SearchAsync;
    }

    protected override void OnInitialized()
    {
        if (Context is null)
            Context = ServiceProvider.GetRequiredService<IFluxSetContext<T>>();
    }

    private async Task<IEnumerable<T>> SearchAsync(string searchText, CancellationToken cancellationToken)
    {
        var parameters = await GetParametersAsync(searchText, cancellationToken);
        var page = await Context.GetPageAsync(0, 10, parameters);

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
