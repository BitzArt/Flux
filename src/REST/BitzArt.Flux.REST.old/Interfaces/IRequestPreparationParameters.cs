using BitzArt.Pagination;

namespace BitzArt.Flux.REST;

internal interface IRequestPreparationParameters
{
    public EndpointType EndpointType { get; }

    public object? RequestParameters { get; }

    public object? Id { get; }

    public PageRequest? PageRequest { get; }

    public Func<string, HttpRequestMessage> InitialCreateRequestMessageFunc { get; }
}
