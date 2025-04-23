using System.Diagnostics;

namespace BitzArt.Flux.REST;

internal class DefaultFluxRestSetEndpointOptionsCollection<TModel, TKey>()
    where TModel : class
{
    private IFluxRestSetEndpointContext<TModel>? _defaultEndpointOptions;

    private IFluxRestSetPageEndpointContext<TModel>? _pageEndpointOptions;

    private IFluxRestSetIdEndpointContext<TModel>? _idEndpointOptions;

    public IFluxRestSetEndpointContext<TModel, TInputParameters> GetDefaultInstance<TInputParameters>(IFluxRestSetOptions<TModel> setOptions, EndpointType endpointType)
         where TInputParameters : notnull, IOperationParameterCollection
    {
        var defaultOptions = endpointType switch
        {
            EndpointType.Default => _defaultEndpointOptions ??= new FluxRestSetEndpointContext<TModel, TKey, TInputParameters>(setOptions),
            EndpointType.Page => _pageEndpointOptions ??= new FluxRestSetPageEndpointContext<TModel, TKey, TInputParameters>(setOptions),
            EndpointType.Id => _idEndpointOptions ??= new FluxRestSetIdEndpointContext<TModel, TKey, TInputParameters>(setOptions),
            _ => throw new UnreachableException("Invalid endpoint type.")
        };

        return (IFluxRestSetEndpointContext<TModel, TInputParameters>)defaultOptions;
    }
}
