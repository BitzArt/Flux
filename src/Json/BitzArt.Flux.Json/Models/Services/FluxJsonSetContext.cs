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
    private Func<IQueryable<TModel>, OperationDescriptor, IQueryable<TModel>>? EnrichQuery => Configuration.EnrichQuery;

    public override Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var operationName = descriptor.GetFriendlyOperationName();

        Logger.LogInformation("[{type}] {operationName}", typeof(TModel).Name, operationName);

        var data = ResolveData(descriptor);

        return Task.FromResult(data);
    }

    private object? ResolveData(OperationDescriptor descriptor) => descriptor switch
    {
        GetAllOperationDescriptor => Items.AsQueryable(q => EnrichQuery?.Invoke(q, descriptor) ?? q).ToList(),

        GetPageOperationDescriptor getPageOperation => Items.AsQueryable(q => EnrichQuery?.Invoke(q, descriptor) ?? q).ToPage(getPageOperation.PageRequest),

        GetOperationDescriptor getOperation => Items.Get(EnrichQuery is not null ? q => EnrichQuery.Invoke(q, descriptor) : null, getOperation.Id),

        AddOperationDescriptor addOperation
            => Items.Add((TModel)addOperation.Value!),

        UpdateOperationDescriptor updateOperation when updateOperation.Partial == false
            => Items.Update(updateOperation.Id, (TModel)updateOperation.Value!),

        UpdateOperationDescriptor updateOperation when updateOperation.Partial == true
            => throw new NotSupportedException("Partial updates are not supported in JSON set context."),

        RemoveOperationDescriptor removeOperation
            => Items.Remove(removeOperation.Id) == false 
            ? throw new InvalidOperationException($"No matching item of type {typeof(TModel).Name} was found.") : null,

        _ => throw new NotSupportedException($"Operation type '{descriptor.GetType().Name}' is not supported in JSON set context.")
    };
}
