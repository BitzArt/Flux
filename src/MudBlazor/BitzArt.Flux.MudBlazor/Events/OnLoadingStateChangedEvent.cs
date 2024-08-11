namespace BitzArt.Flux.MudBlazor;

/// <summary>
/// Event that is triggered when the loading state of a data provider changes.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <param name="args"></param>
/// <returns></returns>
public delegate Task OnLoadingStateChanged<TModel>(OnLoadingStateChangedEventArgs<TModel> args)
    where TModel : class;

/// <summary>
/// Event arguments for the OnLoadingStateChanged event.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class OnLoadingStateChangedEventArgs<TModel> : EventArgs
    where TModel : class
{
    /// <summary>
    /// Data provider that triggered the event.
    /// </summary>
    public IFluxSetDataProvider<TModel> Sender { get; set; }

    /// <summary>
    /// New loading state.
    /// </summary>
    public bool IsLoading { get; set; }

    internal OnLoadingStateChangedEventArgs(FluxSetDataProvider<TModel> sender)
    {
        Sender = sender;
        IsLoading = sender.IsLoading;
    }
}
