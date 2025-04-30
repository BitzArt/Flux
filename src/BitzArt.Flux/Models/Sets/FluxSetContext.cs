using BitzArt.Flux.Operations;
using BitzArt.Pagination;

namespace BitzArt.Flux.Sets;

/// <inheritdoc/>
/// <typeparam name="TModel">Model type.</typeparam>
/// <typeparam name="TKey">Key type.</typeparam>
/// <typeparam name="TConfig">Configuration object type.</typeparam>
public abstract class FluxSetContext<TModel, TKey, TConfig> : FluxSetContext<TModel, TKey>
    where TModel : class
    where TKey : notnull
    where TConfig : notnull
{
    /// <summary>
    /// Set configuration.
    /// </summary>
    public TConfig Configuration { get; private init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluxSetContext{TModel, TKey, TConfig}"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public FluxSetContext(TConfig configuration)
    {
        Configuration = configuration;
    }
}

/// <summary>
/// <para>
/// Base class for set context implementations.
/// </para>
/// <para>
/// <b>Note:</b> This class is a part of internal implementation details and should not be used directly. <br />
/// It is only exposed for the purposes of <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementations</see>.
/// </para>
/// </summary>
/// <typeparam name="TModel">Model type.</typeparam>
/// <typeparam name="TKey">Key type.</typeparam>
public abstract class FluxSetContext<TModel, TKey> : IFluxSetContext<TModel, TKey>
    where TModel : class
    where TKey : notnull
{
    // ============================== GetAsync ==============================

    /// <inheritdoc/>
    public Task<TModel> GetAsync(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TResponse>(new GetOperationDescriptor(id: null, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(id, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAsync<TResponse>(new GetOperationDescriptor(id: id, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(Action<GetOperationDescriptor> configureOperation, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(configureOperation, cancellationToken);

    /// <inheritdoc/>
    public Task<TModel> GetAsync(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => GetAsync<TModel>(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(Action<GetOperationDescriptor> configureOperation, CancellationToken cancellationToken = default)
    {
        var descriptor = new GetOperationDescriptor(id: null, parameters: null);
        configureOperation.Invoke(descriptor);
        return GetAsync<TResponse>(descriptor, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<TResponse> GetAsync<TResponse>(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetAllAsync ==============================

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> GetAllAsync(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAllAsync<IEnumerable<TModel>>(parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAllAsync<TResponse>(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetAllAsync<TResponse>(new GetAllOperationDescriptor(parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> GetAllAsync(Action<GetAllOperationDescriptor> configureOperation, CancellationToken cancellationToken = default)
        => GetAllAsync<IEnumerable<TModel>>(configureOperation, cancellationToken);

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> GetAllAsync(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => GetAllAsync<IEnumerable<TModel>>(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetAllAsync<TResponse>(Action<GetAllOperationDescriptor> configureOperation, CancellationToken cancellationToken = default)
    {
        var descriptor = new GetAllOperationDescriptor(parameters: null);
        configureOperation.Invoke(descriptor);
        return GetAllAsync<TResponse>(descriptor, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<TResponse> GetAllAsync<TResponse>(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<PageResult<TModel>>(offset, limit, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetPageAsync<TResponse>(int offset, int limit, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<TResponse>(new PageRequest(offset, limit), parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<PageResult<TModel>>(pageRequest, parameters, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetPageAsync<TResponse>(PageRequest pageRequest, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => GetPageAsync<TResponse>(new GetPageOperationDescriptor(pageRequest: pageRequest, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<PageResult<TModel>> GetPageAsync(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => GetPageAsync<PageResult<TModel>>(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> GetPageAsync<TResponse>(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== AddAsync ==============================

    /// <inheritdoc/>
    public Task AddAsync(TModel value, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync(new AddOperationDescriptor(id: null, value: value, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync<TResponse>(new AddOperationDescriptor(id: null, value: value, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task AddAsync(TModel value, TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync(new AddOperationDescriptor(id: id, value: value, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => AddAsync<TResponse>(new AddOperationDescriptor(id: id, value: value, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task AddAsync(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> AddAsync<TResponse>(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc/>
    public Task UpdateAsync(TModel value, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync(new UpdateOperationDescriptor(id: null, value: value, partial: partial, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync<TResponse>(new UpdateOperationDescriptor(id: null, value: value, partial: partial, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task UpdateAsync(TModel value, TKey id, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync(new UpdateOperationDescriptor(id: id, value: value, partial: partial, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, TKey id, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default)
        => UpdateAsync<TResponse>(new UpdateOperationDescriptor(id: id, value: value, partial: partial, parameters: parameters?.Parameters), cancellationToken);

    /// <inheritdoc/>
    public Task UpdateAsync(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync(descriptor, cancellationToken);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync<TResponse>(descriptor, cancellationToken);

    // ============================== ExecuteAsync ==============================

    /// <inheritdoc/>
    public Task<TResponse> ExecuteAsync<TResponse>(OperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ((Task<TResponse>)ExecuteAsync(descriptor, typeof(TResponse), cancellationToken));

    /// <inheritdoc/>
    public Task ExecuteAsync(OperationDescriptor descriptor, CancellationToken cancellationToken = default)
        => ExecuteAsync(descriptor, null, cancellationToken);

    /// <inheritdoc/>
    public abstract Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default);
}
