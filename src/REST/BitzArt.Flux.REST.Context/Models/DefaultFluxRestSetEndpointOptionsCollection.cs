using System.Diagnostics;

namespace BitzArt.Flux.REST;

internal class DefaultFluxRestSetEndpointOptionsCollection<TModel, TKey>()
    where TModel : class
{
    private IFluxRestSetEndpointOptions<TModel>? _defaultEndpointOptions;

    private IFluxRestSetPageEndpointOptions<TModel>? _pageEndpointOptions;

    private IFluxRestSetIdEndpointOptions<TModel>? _idEndpointOptions;

    public IFluxRestSetEndpointOptions<TModel, TInputParameters> GetDefaultInstance<TInputParameters>(IFluxRestSetOptions<TModel> setOptions, EndpointType endpointType)
         where TInputParameters : notnull, IFluxOperationParameters
    {
        var defaultOptions = endpointType switch
        {
            EndpointType.Default => _defaultEndpointOptions ??= new FluxRestSetEndpointOptions<TModel, TKey, TInputParameters>(setOptions),
            EndpointType.Page => _pageEndpointOptions ??= new FluxRestSetPageEndpointOptions<TModel, TKey, TInputParameters>(setOptions),
            EndpointType.Id => _idEndpointOptions ??= new FluxRestSetIdEndpointOptions<TModel, TKey, TInputParameters>(setOptions),
            _ => throw new UnreachableException("Invalid endpoint type.")
        };

        return (IFluxRestSetEndpointOptions<TModel, TInputParameters>)defaultOptions;
    }
}
