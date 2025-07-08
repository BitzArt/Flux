using BitzArt.Flux.Sets;
using BitzArt.Pagination;
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

    public override async Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var operationName = descriptor.GetFriendlyOperationName();

        Logger.LogInformation("[{type}] {operationName}", typeof(TModel).Name, operationName);

        var data = await ResolveDataAsync(descriptor, cancellationToken);

        return data;
    }

    private async Task<object?> ResolveDataAsync(OperationDescriptor descriptor, CancellationToken cancellationToken)
    {
        switch (descriptor)
        {
            case GetOperationDescriptor getOperation:
                return await GetAsync((TKey)getOperation.Id!, getOperation.Parameters);
            case GetAllOperationDescriptor getAllOperation:
                return await GetAllAsync(getAllOperation.Parameters);
            case GetPageOperationDescriptor pageOperation:
                return await GetPageAsync(pageOperation.PageRequest, pageOperation.Parameters);
            case AddOperationDescriptor addOperation:
                return await AddAsync((TModel)addOperation.Value!, addOperation.Parameters);
            case UpdateOperationDescriptor updateOperation:
                if (updateOperation.Id is null)
                    return await UpdateAsync((TModel)updateOperation.Value!, updateOperation.Partial, updateOperation.Parameters);
                return await UpdateAsync((TKey)updateOperation.Id!, (TModel)updateOperation.Value!, updateOperation.Partial, updateOperation.Parameters);
            default:
                Logger.LogError("Unsupported operation type: {operationType}", descriptor.GetType().Name);
                throw new NotSupportedException($"Operation type '{descriptor.GetType().Name}' is not supported in JSON set context.");
        }
    }

    private Task<IEnumerable<TModel>> GetAllAsync(IOperationParameterCollection? parameters = null)
    {
        Logger.LogInformation("GetAll {type}", typeof(TModel).Name);

        return Task.FromResult((IEnumerable<TModel>)Configuration.DataCollection.Items!);
    }

    private Task<PageResult<TModel, PageRequest>> GetPageAsync(PageRequest pageRequest, IOperationParameterCollection? parameters = null)
    {
        Logger.LogInformation("GetPage {type}", typeof(TModel).Name);

        return Task.FromResult(Configuration.DataCollection.Items!.ToPage(pageRequest));
    }

    private Task<TModel> GetAsync(TKey id, IOperationParameterCollection? parameters = null)
    {
        Logger.LogInformation("Get {type}[{id}]", typeof(TModel).Name, id is not null ? id.ToString() : "_");

        if (Configuration.DataCollection.KeyPropertySelector is null) throw new FluxKeyPropertyExpressionMissingException<TModel>();

        if (id is null) throw new ArgumentNullException(nameof(id), "The key cannot be null.");

        if (Configuration.DataCollection.KeyedItems is null || !Configuration.DataCollection.KeyedItems.TryGetValue(id, out var existingItem))
            throw new FluxItemNotFoundException<TModel>(id);

        return Task.FromResult(existingItem);
    }

    private Task<TModel> AddAsync(TModel model, IOperationParameterCollection? parameters = null)
    {
        throw new NotSupportedException();
    }

    private Task<TModel> UpdateAsync(TModel model, bool partial = false, IOperationParameterCollection? parameters = null)
    {
        throw new NotImplementedException();
    }

    private Task<TModel> UpdateAsync(TKey? id, TModel model, bool partial = false, IOperationParameterCollection? parameters = null)
    {
        throw new NotImplementedException();
    }
}
