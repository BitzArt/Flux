using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxJsonSetContext<TModel, TKey> : FluxJsonSetContext<TModel>, IFluxSetContext<TModel, TKey>
    where TModel : class
{
    // ================ Flux internal wiring ================

    internal new FluxJsonSetOptions<TModel, TKey> SetOptions
    {
        get => (FluxJsonSetOptions<TModel, TKey>)_setOptions;
        set => _setOptions = value;
    }

    // ==================== Constructor ====================

    public FluxJsonSetContext(FluxJsonServiceOptions serviceOptions, ILogger logger, FluxJsonSetOptions<TModel, TKey> setOptions)
        : base(serviceOptions, logger, setOptions)
    {
        SetOptions = setOptions;
    }

    // ============== Methods implementation ==============

    public override Task<TModel> GetAsync(object? id, params object[]? parameters) => GetAsync((TKey?)id, parameters);

    public Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        _logger.LogInformation("Get {type}[{id}]", typeof(TModel).Name, id is not null ? id.ToString() : "_");

        var existingItem = SetOptions.Items!.FirstOrDefault(item =>
        {
            if (SetOptions.KeyPropertyExpression is null) throw new FluxKeyPropertyExpressionMissingException<TModel>();
            
            var itemId = SetOptions.KeyPropertyExpression.Compile().Invoke(item);
            return itemId is not null && itemId.Equals(id);
        }) ?? throw new FluxItemNotFoundException<TModel>(id);

        return Task.FromResult(existingItem);
    }

    public Task<TModel> UpdateAsync(TKey? id, TModel model, bool partial = false, params object[]? parameters)
        => UpdateAsync<TModel>(id, model, partial, parameters);

    public Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters)
        => base.UpdateAsync<TResponse>(id, model, partial, parameters);
}