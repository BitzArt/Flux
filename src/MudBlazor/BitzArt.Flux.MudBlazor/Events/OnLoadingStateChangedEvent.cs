namespace BitzArt.Flux.MudBlazor;

/// <summary>
/// Event that is triggered when the loading state of a data provider changes.
/// </summary>
/// <param name="args"></param>
/// <returns></returns>
public delegate Task OnLoadingStateChanged<TRequest, TModel>(OnLoadingStateChangedEventArgs<TRequest, TModel> args)
    where TModel : class;

/// <summary>
/// Event arguments for the OnLoadingStateChanged event.
/// </summary>
public class OnLoadingStateChangedEventArgs<TRequest, TModel> : EventArgs
    where TModel : class
{
    /// <summary>
    /// Data provider that triggered the event.
    /// </summary>
    public IFluxSetDataProvider<TRequest, TModel> Sender { get; set; }

    /// <summary>
    /// New loading state.
    /// </summary>
    public bool IsLoading { get; set; }

    internal OnLoadingStateChangedEventArgs(FluxSetDataProvider<TRequest, TModel> sender)
    {
        Sender = sender;
        IsLoading = sender.IsLoading;
    }
}
