using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux;

/// <summary>
/// Represents a Flux Items Provider for a specified model.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IFluxItemsProvider<TModel> : IFluxItemsProvider
    where TModel : class
{
    /// <summary>
    /// The items provider delegate. <br />
    /// </summary>
    public GridItemsProvider<TModel> GetItems { get; }

    /// <summary>
    /// Call this method to manually configure the set to be used by the provider.
    /// </summary>
    /// <param name="set"></param>
    public void ConfigureSet(IFluxSetContext<TModel> set);

    /// <summary>
    /// Restores the last request. <br />
    /// This method allows restoring the last request from a component's state. <br />
    /// Use this when you are restoring components' states.
    /// </summary>
    public void RestoreLastRequest(FluxPageRequestRecord<TModel>? request);

    /// <summary>
    /// Event that is raised after an items request has been made.
    /// </summary>
    /// <param name="request"></param>
    public delegate void OnAfterRequestHandler(FluxPageRequestRecord<TModel> request);

    /// <summary>
    /// Event that is raised after an items request has been made.
    /// </summary>
    public event OnAfterRequestHandler? OnAfterRequest;
}
