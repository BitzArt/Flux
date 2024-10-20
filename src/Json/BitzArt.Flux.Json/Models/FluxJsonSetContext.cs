using BitzArt.Pagination;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxJsonSetContext<TModel, TKey>(
    FluxJsonServiceOptions serviceOptions,
    ILogger logger,
    IFluxJsonSetOptions<TModel> setOptions
    ) : FluxSetContext<TModel, TKey>
    where TModel : class
{
    // ================ Flux internal wiring ================
    internal readonly FluxJsonServiceOptions ServiceOptions = serviceOptions;
    internal readonly ILogger _logger = logger;

    internal IFluxJsonSetOptions<TModel> SetOptions { get; set; } = setOptions;

    // ============== Methods implementation ==============

    public override Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters)
    {
        _logger.LogDebug("GetAll {type}", typeof(TModel).Name);

        return Task.FromResult<IEnumerable<TModel>>(SetOptions.Items!);
    }

    public override Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        _logger.LogDebug("GetPage {type}", typeof(TModel).Name);

        return Task.FromResult(SetOptions.Items!.ToPage(pageRequest));
    }

    public override Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        _logger.LogDebug("Get {type}[{id}]", typeof(TModel).Name, id is not null ? id.ToString() : "_");

        var existingItem = SetOptions.Items!.FirstOrDefault(item =>
        {
            if (SetOptions.KeyPropertyExpression is null) throw new FluxKeyPropertyExpressionMissingException<TModel>();

            var itemId = SetOptions.KeyPropertyExpression.Compile().Invoke(item);
            return itemId is not null && itemId.Equals(id);
        }) ?? throw new FluxItemNotFoundException<TModel>(id);

        return Task.FromResult(existingItem);
    }

    public override Task<TResponse> AddAsync<TResponse>(TModel model, params object[]? parameters)
    {
        throw new NotSupportedException();
    }

    public override Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }
}