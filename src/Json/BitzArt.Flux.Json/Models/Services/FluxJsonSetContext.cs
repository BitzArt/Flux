using BitzArt.Flux.Sets;

namespace BitzArt.Flux.Json;

internal class FluxJsonSetContext<TModel, TKey> : FluxSetContext<TModel, TKey, FluxJsonSetConfiguration>
    where TModel : class
    where TKey : notnull
{

    public FluxJsonSetContext(FluxJsonSetConfiguration configuration, IServiceProvider serviceProvider)
        : base(configuration, serviceProvider) { }

    public override Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
