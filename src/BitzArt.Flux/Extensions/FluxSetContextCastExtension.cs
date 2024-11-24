using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for <see cref="IFluxSetContext{TModel}"/>, allowing for casting of the model type.
/// </summary>
internal static class FluxSetContextCastExtension
{
    internal static IFluxSetContext<TResult> Cast<TSource, TKey, TResult>(this IFluxSetContext<TSource, TKey> sourceContext)
        where TSource : class
        where TResult : class
    {
        if (typeof(TSource).IsAssignableTo(typeof(TResult))) return sourceContext.CastDown<TSource, TKey, TResult>();
        if (typeof(TResult).IsAssignableTo(typeof(TSource))) return sourceContext.CastUp<TSource, TKey, TResult>();

        throw new InvalidOperationException($"Cannot cast {typeof(TSource).Name} to {typeof(TResult).Name}");
    }

    private static IFluxSetContext<TResult> CastUp<TSource, TKey, TResult>(this IFluxSetContext<TSource, TKey> sourceContext)
        where TSource : class
        where TResult : class
    {
        if (sourceContext is not FluxCastDownSetContext<>)
    }

    private static IFluxSetContext<TResult> CastDown<TSource, TKey, TResult>(this IFluxSetContext<TSource, TKey> sourceContext)
        where TSource : class, TResult
        where TResult : class
        => new FluxCastDownSetContext<TSource, TKey, TResult>(sourceContext);
}

internal class FluxCastDownSetContext<TSource, TKey, TResult>(IFluxSetContext<TSource, TKey> sourceContext) : IFluxSetContext<TResult, TKey>
    where TSource : class, TResult
    where TResult : class
{
    public async Task<TResult> AddAsync(TResult model, params object[]? parameters)
    {
        if (model is not TSource modelCast) throw new InvalidOperationException("Invalid model type");
        return await sourceContext.AddAsync(modelCast, parameters);
    }

    public async Task<TResponse> AddAsync<TResponse>(TResult model, params object[]? parameters)
    {
        if (model is not TSource modelCast) throw new InvalidOperationException("Invalid model type");
        return await sourceContext.AddAsync<TResponse>(modelCast, parameters);
    }

    public IFluxSetContext<TResult1> Cast<TResult1>() where TResult1 : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TResult>> GetAllAsync(params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> GetAsync(TKey? id, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> GetAsync(object? id = null, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<TResult>> GetPageAsync(int offset, int limit, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<TResult>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> UpdateAsync(TKey? id, TResult model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResponse> UpdateAsync<TResponse>(TKey? id, TResult model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> UpdateAsync(object? id, TResult model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> UpdateAsync(TResult model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResponse> UpdateAsync<TResponse>(object? id, TResult model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<TResponse> UpdateAsync<TResponse>(TResult model, bool partial = false, params object[]? parameters)
    {
        throw new NotImplementedException();
    }
}