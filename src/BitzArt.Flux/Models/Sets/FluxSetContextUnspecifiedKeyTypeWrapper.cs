using BitzArt.Flux.Operations;
using BitzArt.Pagination;

namespace BitzArt.Flux.Sets;

/// <summary>
/// A wrapper for <see cref="IFluxSetContext{TModel, TKey}"/> to allow injection as <see cref="IFluxSetContext{TModel}"/>
/// (notice a lack of TKey generic parameter). <br />
/// It uses the actual registered set context with a specified key type internally
/// and delegates all calls to it,
/// while converting the key type to the specified type when necessary.
/// </summary>
internal class FluxSetContextUnspecifiedKeyTypeWrapper<TModel, TKey>(IFluxSetContext<TModel, TKey> setContext) : IFluxSetContext<TModel>
    where TModel : class
    where TKey : notnull
{
    // Actual set context with a specified key type
    internal IFluxSetContext<TModel, TKey> InnerSetContext { get; private init; } = setContext;

    private static TKey ConvertKey(object value)
    {
        if (value is not TKey valueCasted)
            throw new InvalidOperationException($"Invalid key type. Expected '{typeof(TKey).Name}' but got '{value.GetType().Name}'.");

        return valueCasted;
    }

    // ============================== GetAsync ==============================

    Task<TModel> IFluxSetContext<TModel, object>.GetAsync(OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetAsync(parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAsync<TResponse>(OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetAsync<TResponse>(parameters, cancellationToken);

    Task<TModel> IFluxSetContext<TModel, object>.GetAsync(object id, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetAsync(ConvertKey(id), parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAsync<TResponse>(object id, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetAsync<TResponse>(ConvertKey(id), parameters, cancellationToken);

    Task<TModel> IFluxSetContext<TModel, object>.GetAsync(GetOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.GetAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAsync<TResponse>(GetOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.GetAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetAllAsync ==============================

    Task<IEnumerable<TModel>> IFluxSetContext<TModel, object>.GetAllAsync(OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetAllAsync(parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAllAsync<TResponse>(OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetAllAsync<TResponse>(parameters, cancellationToken);

    Task<IEnumerable<TModel>> IFluxSetContext<TModel, object>.GetAllAsync(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.GetAllAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetAllAsync<TResponse>(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.GetAllAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetPageAsync ==============================

    Task<PageResult<TModel>> IFluxSetContext<TModel, object>.GetPageAsync(int offset, int limit, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetPageAsync(offset, limit, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetPageAsync<TResponse>(int offset, int limit, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetPageAsync<TResponse>(offset, limit, parameters, cancellationToken);

    Task<PageResult<TModel>> IFluxSetContext<TModel, object>.GetPageAsync(PageRequest pageRequest, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetPageAsync(pageRequest, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetPageAsync<TResponse>(PageRequest pageRequest, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.GetPageAsync<TResponse>(pageRequest, parameters, cancellationToken);

    Task<PageResult<TModel>> IFluxSetContext<TModel, object>.GetPageAsync(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.GetPageAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.GetPageAsync<TResponse>(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.GetPageAsync<TResponse>(descriptor, cancellationToken);

    // ============================== AddAsync ==============================

    Task IFluxSetContext<TModel, object>.AddAsync(TModel value, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.AddAsync(value, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.AddAsync<TResponse>(TModel value, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.AddAsync<TResponse>(value, parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.AddAsync(TModel value, object id, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.AddAsync(value, ConvertKey(id), parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.AddAsync<TResponse>(TModel value, object id, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.AddAsync<TResponse>(value, ConvertKey(id), parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.AddAsync(AddOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.AddAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.AddAsync<TResponse>(AddOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.AddAsync<TResponse>(descriptor, cancellationToken);

    // ============================== UpdateAsync ==============================

    Task IFluxSetContext<TModel, object>.UpdateAsync(TModel value, bool partial, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.UpdateAsync(value, partial, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.UpdateAsync<TResponse>(TModel value, bool partial, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.UpdateAsync<TResponse>(value, partial, parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.UpdateAsync(TModel value, object id, bool partial, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.UpdateAsync(value, ConvertKey(id), partial, parameters, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.UpdateAsync<TResponse>(TModel value, object id, bool partial, OperationParameterCollection? parameters, CancellationToken cancellationToken)
        => InnerSetContext.UpdateAsync<TResponse>(value, ConvertKey(id), partial, parameters, cancellationToken);

    Task IFluxSetContext<TModel, object>.UpdateAsync(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.UpdateAsync(descriptor, cancellationToken);

    Task<TResponse> IFluxSetContext<TModel, object>.UpdateAsync<TResponse>(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.UpdateAsync<TResponse>(descriptor, cancellationToken);

    // ============================== ExecuteAsync ==============================

    Task<TResponse> IFluxSetContext<TModel, object>.ExecuteAsync<TResponse>(OperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.ExecuteAsync<TResponse>(descriptor, cancellationToken);

    Task IFluxSetContext<TModel, object>.ExecuteAsync(OperationDescriptor descriptor, CancellationToken cancellationToken)
        => InnerSetContext.ExecuteAsync(descriptor, cancellationToken);

    Task IFluxSetContext<TModel, object>.ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken)
        => InnerSetContext.ExecuteAsync(descriptor, responseType, cancellationToken);
}
