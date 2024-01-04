using BitzArt.Pagination;

namespace BitzArt.Flux;

internal class FluxJsonSetContext<TModel> : IFluxSetContext<TModel> 
    where TModel : class
{
    internal readonly FluxJsonServiceOptions ServiceOptions;
    
    protected FluxJsonSetOptions<TModel> _setOptions;
    internal virtual FluxJsonSetOptions<TModel> SetOptions
    {
        get => _setOptions;
        set => _setOptions = value;
    }

    public FluxJsonSetContext(FluxJsonServiceOptions serviceOptions, FluxJsonSetOptions<TModel> setOptions)
    {
        ServiceOptions = serviceOptions;
        _setOptions = setOptions;
    }
    
    public virtual Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters)
    {
        return Task.FromResult<IEnumerable<TModel>>(SetOptions.Items!);
    }

    public virtual Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters)
    {
        return Task.FromResult(SetOptions.Items!.ToPage(offset, limit));
    }

    public virtual Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
       return Task.FromResult(SetOptions.Items!.ToPage(pageRequest.Offset!.Value, pageRequest.Limit!.Value));
    }

    public virtual Task<TModel> GetAsync(object? id, params object[]? parameters)
    {
        var existingItem = SetOptions.Items!.FirstOrDefault(item =>
        {
            if (SetOptions.KeyPropertyExpression is null) throw new Exception();
            
            var itemId = SetOptions.KeyPropertyExpression.Compile().Invoke(item);
            return Equals(itemId, id);
        });

        if (existingItem is null)
            throw new Exception("Not found");
        
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

    public FluxJsonSetContext(FluxJsonServiceOptions serviceOptions, FluxJsonSetOptions<TModel, TKey> setOptions)
        : base(serviceOptions, setOptions)
    {
        SetOptions = setOptions;
    }

    public override Task<TModel> GetAsync(object? id, params object[]? parameters) => GetAsync((TKey?)id, parameters);

    public Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        var existingItem = SetOptions.Items!.FirstOrDefault(item =>
        {
            if (SetOptions.KeyPropertyExpression is null) throw new Exception();
            
            var itemId = SetOptions.KeyPropertyExpression.Compile().Invoke(item);
            return Equals(itemId, id);
        });

        if (existingItem is null)
            throw new Exception("Not found");

        return Task.FromResult(existingItem);
    }
}