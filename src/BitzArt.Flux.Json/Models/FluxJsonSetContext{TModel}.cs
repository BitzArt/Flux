using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxJsonSetContext<TModel> : IFluxSetContext<TModel>
    where TModel : class
{
    // ================ Flux internal wiring ================

    internal readonly FluxJsonServiceOptions ServiceOptions;
    internal readonly ILogger _logger;
    
    protected FluxJsonSetOptions<TModel> _setOptions;
    internal virtual FluxJsonSetOptions<TModel> SetOptions
    {
        get => _setOptions;
        set => _setOptions = value;
    }

    // ==================== Constructor ====================

    public FluxJsonSetContext(FluxJsonServiceOptions serviceOptions, ILogger logger, FluxJsonSetOptions<TModel> setOptions)
    {
        ServiceOptions = serviceOptions;
        _logger = logger;
        _setOptions = setOptions;
    }

    // ================ Linking parsed data ================

    public ICollection<TModel> Items => SetOptions.Items ?? throw new FluxJsonMissingDataException();
    private IQueryable<TModel> Query => Items.AsQueryable();

    // ============== IEnumerable implementation ==============

    public IEnumerator<TModel> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // ============== IQueryable implementation ==============

    public Type ElementType => typeof(TModel);

    public Expression Expression => Query.Expression;

    public IQueryProvider Provider => Query.Provider;

    // ============== Data methods implementation ==============

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
            return itemId.Equals(id);
        }) ?? throw new FluxItemNotFoundException<TModel>(id);

        return Task.FromResult(existingItem);
    }

    public Task<TModel> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }
}
