namespace BitzArt.Flux.REST;

internal interface IFluxRestSetEndpointContext<TModel, TInputParameters> : IFluxRestSetEndpointContext<TModel>
    where TModel : class
    where TInputParameters : IOperationParameterCollection?
{
    public Func<TInputParameters, IFluxRestOperationParameters>? TransformParametersFunc { get; internal set; }
}

internal interface IFluxRestSetEndpointContext<TModel>
    where TModel : class
{
    /// <summary>
    /// A path to the endpoint.
    /// Can be null if other approach to building the endpoint path is used.
    /// </summary>
    public string? Path { get; internal set; }

    public HttpRequestMessage PrepareRequest(IRequestPreparationParameters parameters);
}
