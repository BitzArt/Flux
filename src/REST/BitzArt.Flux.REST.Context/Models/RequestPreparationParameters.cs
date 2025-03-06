using BitzArt.Pagination;

namespace BitzArt.Flux.REST;

internal class RequestPreparationParameters<TInputParameters, TKey> : IRequestPreparationParameters
    where TInputParameters : IOperationParameterCollection?
{
    public EndpointType EndpointType { get; set; }

    public TInputParameters? RequestParameters { get; set; }

    object? IRequestPreparationParameters.RequestParameters => RequestParameters;

    // TODO: handle non-nullable types
    public TKey? Id { get; set; }

    object? IRequestPreparationParameters.Id => Id;

    public PageRequest? PageRequest { get; set; }

    public Func<string, HttpRequestMessage> InitialCreateRequestMessageFunc { get; set; }

    public RequestPreparationParameters(
        EndpointType endpointType,
        TKey id,
        Func<string, HttpRequestMessage> initialCreateRequestMessageFunc)
        : this(endpointType, initialCreateRequestMessageFunc)
    {
        Id = id;
    }

    public RequestPreparationParameters(
        EndpointType endpointType,
        TKey id,
        TInputParameters? requestParameters,
        Func<string, HttpRequestMessage> initialCreateRequestMessageFunc)
        : this(endpointType, requestParameters, initialCreateRequestMessageFunc)
    {
        Id = id;
    }

    public RequestPreparationParameters(
        EndpointType endpointType,
        PageRequest? pageRequest,
        Func<string, HttpRequestMessage> initialCreateRequestMessageFunc)
        : this(endpointType, initialCreateRequestMessageFunc)
    {
        PageRequest = pageRequest;
    }

    public RequestPreparationParameters(
        EndpointType endpointType,
        PageRequest? pageRequest,
        TInputParameters? requestParameters,
        Func<string, HttpRequestMessage> initialCreateRequestMessageFunc)
        : this(endpointType, requestParameters, initialCreateRequestMessageFunc)
    {
        PageRequest = pageRequest;
    }

    public RequestPreparationParameters(
        EndpointType endpointType,
        TInputParameters? requestParameters,
        Func<string, HttpRequestMessage> initialCreateRequestMessageFunc)
        : this(endpointType, initialCreateRequestMessageFunc)
    {
        RequestParameters = requestParameters;
    }

    public RequestPreparationParameters(
        EndpointType endpointType,
        Func<string, HttpRequestMessage> initialCreateRequestMessageFunc)
    {
        EndpointType = endpointType;
        InitialCreateRequestMessageFunc = initialCreateRequestMessageFunc;
    }
}
