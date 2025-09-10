using BitzArt.Flux.Sets;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux.Json;

internal class FluxJsonSetContext<TModel, TKey> : FluxSetContext<TModel, TKey, FluxJsonSetConfiguration<TModel>>
    where TModel : class
    where TKey : notnull
{
    public FluxJsonSetContext(FluxJsonSetConfiguration<TModel> configuration, IServiceProvider serviceProvider, ILogger logger)
        : base(configuration, serviceProvider, logger)
    {
    }

    private FluxJsonDataCollection<TModel> Items => Configuration.DataCollection;

    public override Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var operationName = descriptor.GetFriendlyOperationName();

        Logger.LogInformation("[{type}] {operationName}", typeof(TModel).Name, operationName);

        var data = ResolveDataAsync(descriptor, cancellationToken);

        return Task.FromResult(data);
    }

    private object? ResolveDataAsync(OperationDescriptor descriptor, CancellationToken cancellationToken) => descriptor switch
    {
        GetAllOperationDescriptor
            => Items.GetAll(),

        GetPageOperationDescriptor getPageOperation
            => Items.GetAll().ToPage(getPageOperation.PageRequest),

        GetOperationDescriptor getOperation
            => Items.Get(getOperation.Id),

        AddOperationDescriptor addOperation
            => Items.Add((TModel)addOperation.Value!),

        UpdateOperationDescriptor updateOperation when updateOperation.Partial == false
            => Items.Update(updateOperation.Id, (TModel)updateOperation.Value!),

        UpdateOperationDescriptor updateOperation when updateOperation.Partial == true
            => throw new NotSupportedException("Partial updates are not supported in JSON set context."),

        _ => throw new NotSupportedException($"Operation type '{descriptor.GetType().Name}' is not supported in JSON set context.")
    };
}
