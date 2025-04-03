using BitzArt.Pagination;

namespace BitzArt.Flux.Sets;

/// <summary>
/// Wrapper for <see cref="IFluxSetContext{TModel, TKey}"/> to allow using it with unspecified key type.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class FluxSetContextUnspecifiedKeyTypeWrapper<TModel, TKey> : IFluxSetContext<TModel>
    where TModel : class
    where TKey : notnull
{
    // Actual set context with a specified key type
    private readonly IFluxSetContext<TModel, TKey> _setContext;

    public FluxSetContextUnspecifiedKeyTypeWrapper(IFluxSetContext<TModel, TKey> setContext)
    {
        _setContext = setContext;
    }

    private static TKey ConvertKey(object value)
    {
        if (value is not TKey valueCasted)
            throw new InvalidOperationException($"Invalid key type. Expected '{typeof(TKey).Name}' but got '{value.GetType().Name}'.");

        return valueCasted;
    }

    // ============================== GetAsync ==============================

    Task<TModel> IFluxSetContext<TModel, object>.GetAsync(IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetAsync(parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAsync<TResponse>(IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetAsync<TResponse>(parameters, cancellationToken);

    Task<TModel> IFluxSetContext<TModel, object>.GetAsync(object id, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetAsync(ConvertKey(id), parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAsync<TResponse>(object id, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetAsync<TResponse>(ConvertKey(id), parameters, cancellationToken);

    Task<TModel> IFluxSetContext<TModel, object>.GetAsync(GetOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.GetAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAsync<TResponse>(GetOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.GetAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetAllAsync ==============================

    Task<IEnumerable<TModel>> IFluxSetContext<TModel, object>.GetAllAsync(IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetAllAsync(parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAllAsync<TResponse>(IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetAllAsync<TResponse>(parameters, cancellationToken);

    Task<IEnumerable<TModel>> IFluxSetContext<TModel, object>.GetAllAsync(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.GetAllAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAllAsync<TResponse>(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.GetAllAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetPageAsync ==============================

    Task<PageResult<TModel>> IFluxSetContext<TModel, object>.GetPageAsync(int offset, int limit, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetPageAsync(offset, limit, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetPageAsync<TResponse>(int offset, int limit, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetPageAsync<TResponse>(offset, limit, parameters, cancellationToken);

    Task<PageResult<TModel>> IFluxSetContext<TModel, object>.GetPageAsync(PageRequest pageRequest, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetPageAsync(pageRequest, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetPageAsync<TResponse>(PageRequest pageRequest, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.GetPageAsync<TResponse>(pageRequest, parameters, cancellationToken);

    Task<PageResult<TModel>> IFluxSetContext<TModel, object>.GetPageAsync(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.GetPageAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetPageAsync<TResponse>(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.GetPageAsync<TResponse>(descriptor, cancellationToken);

    // ============================== AddAsync ==============================

    Task IFluxSetContext<TModel, object>.AddAsync(TModel value, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.AddAsync(value, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.AddAsync<TResponse>(TModel value, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.AddAsync<TResponse>(value, parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.AddAsync(TModel value, object id, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.AddAsync(value, ConvertKey(id), parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.AddAsync<TResponse>(TModel value, object id, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.AddAsync<TResponse>(value, ConvertKey(id), parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.AddAsync(AddOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.AddAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.AddAsync<TResponse>(AddOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.AddAsync<TResponse>(descriptor, cancellationToken);

    // ============================== UpdateAsync ==============================

    Task IFluxSetContext<TModel, object>.UpdateAsync(TModel value, bool partial, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.UpdateAsync(value, partial, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.UpdateAsync<TResponse>(TModel value, bool partial, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.UpdateAsync<TResponse>(value, partial, parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.UpdateAsync(TModel value, object id, bool partial, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.UpdateAsync(value, ConvertKey(id), partial, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.UpdateAsync<TResponse>(TModel value, object id, bool partial, IOperationParameterCollection? parameters, CancellationToken cancellationToken)
        => _setContext.UpdateAsync<TResponse>(value, ConvertKey(id), partial, parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.UpdateAsync(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.UpdateAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.UpdateAsync<TResponse>(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.UpdateAsync<TResponse>(descriptor, cancellationToken);

    // ============================== ExecuteAsync ==============================

    Task IFluxSetContext<TModel, object>.ExecuteAsync(OperationDescriptor descriptor, CancellationToken cancellationToken)
        => _setContext.ExecuteAsync(descriptor, cancellationToken);

    Task IFluxSetContext<TModel, object>.ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken)
        => _setContext.ExecuteAsync(descriptor, responseType, cancellationToken);
}
