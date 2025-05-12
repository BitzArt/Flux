using BitzArt.Flux.Sets;

namespace BitzArt.Flux.REST;

internal class FluxRestSetContext<TModel, TKey> : FluxSetContext<TModel, TKey, FluxRestSetConfiguration>
    where TModel : class
    where TKey : notnull
{
    public FluxRestSetContext(FluxRestSetConfiguration configuration) : base(configuration) { }

    public override Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var endpoint = Configuration.ResolveEndpoint(descriptor);

        throw new NotImplementedException();
    }
}
