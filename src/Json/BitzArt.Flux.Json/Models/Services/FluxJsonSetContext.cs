using BitzArt.Flux.Sets;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux.Json;

internal class FluxJsonSetContext<TModel, TKey> : FluxSetContext<TModel, TKey, FluxJsonSetConfiguration>
    where TModel : class
    where TKey : notnull
{

    public FluxJsonSetContext(FluxJsonSetConfiguration configuration, IServiceProvider serviceProvider, ILogger logger)
        : base(configuration, serviceProvider, logger) { }

    public override Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
