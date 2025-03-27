namespace BitzArt.Flux.REST;

internal interface IFluxRestSetPageEndpointContext<TModel, TInputParameters>
    : IFluxRestSetEndpointContext<TModel, TInputParameters>, IFluxRestSetPageEndpointContext<TModel>
    where TModel : class
    where TInputParameters : IOperationParameterCollection?
{
}

internal interface IFluxRestSetPageEndpointContext<TModel>
    : IFluxRestSetEndpointContext<TModel>
    where TModel : class
{
}
