namespace BitzArt.Flux.REST;

internal interface IFluxRestSetIdEndpointContext<TModel, TInputParameters>
    : IFluxRestSetEndpointContext<TModel, TInputParameters>, IFluxRestSetIdEndpointContext<TModel>
    where TModel : class
    where TInputParameters : IOperationParameterCollection?
{
}

internal interface IFluxRestSetIdEndpointContext<TModel>
    : IFluxRestSetEndpointContext<TModel>
    where TModel : class
{
}
