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
    
    public virtual async Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<TModel> GetAsync(object? id, params object[]? parameters)
    {
        throw new NotImplementedException();
    }
}

internal class FluxJsonSetContext<TModel, TKey> : FluxJsonSetContext<TModel>, IFluxSetContext<TModel, TKey>
    where TModel : class
{
    internal new FluxJsonSetOptions<TModel, TKey> SetOptions
    {
        get => (FluxJsonSetOptions<TModel, TKey>)_setOptions;
        set
        {
            _setOptions = value;
        }
    }

    public FluxJsonSetContext(FluxJsonServiceOptions serviceOptions, FluxJsonSetOptions<TModel, TKey> setOptions)
        : base(serviceOptions, setOptions)
    {
        SetOptions = setOptions;
    }

    public override Task<TModel> GetAsync(object? id, params object[]? parameters) => GetAsync((TKey?)id, parameters);

    public async Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        throw new NotImplementedException();
    }
}