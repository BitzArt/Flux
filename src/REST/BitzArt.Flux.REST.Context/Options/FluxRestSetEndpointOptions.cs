using System.Diagnostics;

namespace BitzArt.Flux.REST;

internal class FluxRestSetEndpointOptions<TModel, TKey, TInputParameters>(
    IFluxRestSetOptions<TModel> setOptions,
    string? path = null,
    Func<TInputParameters, IRestRequestParameters>? transformParametersFunc = null)
    : IFluxRestSetEndpointOptions<TModel, TInputParameters>
    where TModel : class
    where TInputParameters : notnull, IRequestParameters
{
    public IFluxRestSetOptions<TModel> SetOptions { get; set; } = setOptions;

    /// <inheritdoc/>
    public string? Path { get; set; } = path;

    /// <inheritdoc/>
    public Func<TInputParameters, IRestRequestParameters>? TransformParametersFunc { get; set; } = transformParametersFunc;

    public HttpRequestMessage PrepareRequest(IRequestPreparationParameters parameters)
    {
        var path = BuildRequestPath(parameters);
        var requestMessage = parameters.InitialCreateRequestMessageFunc(path);

        return requestMessage;
    }

    private protected virtual string BuildRequestPath(IRequestPreparationParameters parameters)
    {
        var path = GetInitialPath();
        var outputParameters = HandleInputParameters(parameters);

        var result = RequestParameterParsingUtility.ParseRequestUrl(path, outputParameters);
        return result;
    }

    private protected virtual string GetInitialPath()
        => CombinePath(SetOptions.ServiceOptions.BaseUrl, SetOptions.Path, Path);

    private protected IRestRequestParameters? HandleInputParameters(IRequestPreparationParameters parameters)
    {
        if (TransformParametersFunc is not null)
        {
            if (parameters.RequestParameters is not TInputParameters inputParameters)
                throw new UnreachableException();

            return TransformParametersFunc.Invoke(inputParameters);
        }

        if (parameters.RequestParameters is null)
            return null;

        if (parameters.RequestParameters is not IRestRequestParameters restRequestParameters)
            throw new InvalidOperationException(
                $"Unable to resolve {nameof(IRestRequestParameters)} for this request.");

        return restRequestParameters;
    }

    // TODO: Explicitly specify segments (service base url, set path, endpoint path, id (optional)).
    // Handle leading slash in set path and endpoint path. Leading slash means absolute path relative to domain root.
    private protected static string CombinePath(params string?[] segments)
        => string.Join('/', segments.Where(x => !string.IsNullOrEmpty(x)).Select(x => x!.TrimEnd('/')));
}
