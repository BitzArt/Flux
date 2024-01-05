using BitzArt.Pagination;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxJsonSetContext<TModel> : IFluxSetContext<TModel> 
    where TModel : class
{
    internal readonly FluxJsonServiceOptions ServiceOptions;
    internal readonly ILogger _logger;
    
    protected FluxJsonSetOptions<TModel> _setOptions;
    internal virtual FluxJsonSetOptions<TModel> SetOptions
    {
        get => _setOptions;
        set => _setOptions = value;
    }

    public FluxJsonSetContext(FluxJsonServiceOptions serviceOptions, ILogger logger, FluxJsonSetOptions<TModel> setOptions)
    {
        ServiceOptions = serviceOptions;
        _logger = logger;
        _setOptions = setOptions;
    }
    
    public virtual Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters)
    {
        _logger.LogInformation("GetAll {type}", typeof(TModel).Name);

        return Task.FromResult<IEnumerable<TModel>>(SetOptions.Items!);
    }

    public virtual Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters)
    {
        return Task.FromResult(SetOptions.Items!.ToPage(offset, limit));
    }

    public virtual Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        _logger.LogInformation("GetPage {type}", typeof(TModel).Name);
        
       return Task.FromResult(SetOptions.Items!.ToPage(pageRequest.Offset!.Value, pageRequest.Limit!.Value));
    }

    public virtual Task<TModel> GetAsync(object? id, params object[]? parameters)
    {
        _logger.LogInformation("Get {type}[{id}]", typeof(TModel).Name, id is not null ? id.ToString() : "_");
        
        var existingItem = SetOptions.Items!.FirstOrDefault(item =>
        {
            if (SetOptions.KeyPropertyExpression is null) throw new FluxKeyPropertyExpressionMissingException<TModel>();
            
            var itemId = SetOptions.KeyPropertyExpression.Compile().Invoke(item);
            return Equals(itemId, id);
        }) ?? throw new FluxItemNotFoundException<TModel>(id);

        return Task.FromResult(existingItem);
    }
}

internal class FluxJsonSetContext<TModel, TKey> : FluxJsonSetContext<TModel>, IFluxSetContext<TModel, TKey>
    where TModel : class
{
    internal new FluxJsonSetOptions<TModel, TKey> SetOptions
    {
        get => (FluxJsonSetOptions<TModel, TKey>)_setOptions;
        set => _setOptions = value;
    }

    public FluxJsonSetContext(FluxJsonServiceOptions serviceOptions, ILogger logger, FluxJsonSetOptions<TModel, TKey> setOptions)
        : base(serviceOptions, logger, setOptions)
    {
        SetOptions = setOptions;
    }

    public override Task<TModel> GetAsync(object? id, params object[]? parameters) => GetAsync((TKey?)id, parameters);

    public Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        _logger.LogInformation("Get {type}[{id}]", typeof(TModel).Name, id is not null ? id.ToString() : "_");

        var existingItem = SetOptions.Items!.FirstOrDefault(item =>
        {
            if (SetOptions.KeyPropertyExpression is null) throw new FluxKeyPropertyExpressionMissingException<TModel>();
            
            var itemId = SetOptions.KeyPropertyExpression.Compile().Invoke(item);
            return Equals(itemId, id);
        }) ?? throw new FluxItemNotFoundException<TModel>(id);

        return Task.FromResult(existingItem);
    }
}

internal class FluxItemNotFoundException<TModel> : Exception
{
    public FluxItemNotFoundException(object? id) : base($"{typeof(TModel).Name} with key {id} was not found")
    { }
}

internal class FluxKeyPropertyExpressionMissingException<TModel> : Exception
{
    public FluxKeyPropertyExpressionMissingException() : base($"KeyPropertyExpression is required for {typeof(TModel).Name}. Consider using .WithKey() when configuring a Set.")
    {
    }
}