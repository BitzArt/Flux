using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST.Endpoints;

internal sealed class FluxRestPathEndpointConfiguration : FluxRestEndpointConfiguration
{
    private readonly string? _path;
    private readonly List<Type> _operationTypes;
    private readonly bool _pathComplete;
    private readonly bool _queryComplete;

    public FluxRestPathEndpointConfiguration(
        FluxRestSetConfiguration setConfiguration,
        HttpMethods httpMethods,
        string? path,
        bool pathComplete,
        bool queryComplete)
        : base(setConfiguration, httpMethods)
    {
        _path = path?.TrimEnd('/');

        _pathComplete = pathComplete;
        _queryComplete = queryComplete;

        _operationTypes = GetOperationTypes(httpMethods);
    }

    public override IEnumerable<Type> OperationTypes => _operationTypes.AsReadOnly();

    public override HttpRequestMessage Resolve(OperationDescriptor descriptor, IServiceProvider serviceProvider)
    {
        var resolver = serviceProvider.GetRequiredService<IHttpRequestMessageResolver>();
        return resolver.Resolve(SetConfiguration, _path, descriptor, _pathComplete, _queryComplete);
    }

    private static List<Type> GetOperationTypes(HttpMethods methods)
    {
        // Currently, there are 6 possible operation types,
        // hence the maximum capacity.
        var results = new List<Type>(6);

        if (methods.HasFlag(HttpMethods.Get))
        {
            results.Add(typeof(GetOperationDescriptor));
            results.Add(typeof(GetAllOperationDescriptor));
            results.Add(typeof(GetPageOperationDescriptor));
        }

        if (methods.HasFlag(HttpMethods.Post) || methods.HasFlag(HttpMethods.Put))
        {
            results.Add(typeof(AddOperationDescriptor));
        }

        if (methods.HasFlag(HttpMethods.Put) || methods.HasFlag(HttpMethods.Patch))
        {
            results.Add(typeof(UpdateOperationDescriptor));
        }

        if (methods.HasFlag(HttpMethods.Delete))
        {
            results.Add(typeof(RemoveOperationDescriptor));
        }

        return results;
    }
}
