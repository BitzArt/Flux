using BitzArt.Flux.Operations;
using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <inheritdoc cref="IFluxSetContext{TModel, TKey}"/>
public interface IFluxSetContext<TModel> : IFluxSetContext<TModel, object>
    where TModel : class
{
}

/// <summary>
/// Flux context for a preconfigured data set.<br/>
/// See <see href="https://bitzart.github.io/Flux/03.use.html"/>
/// for more information on how you can use Flux in your applications.
/// </summary>
/// <typeparam name="TModel">Model type of the set.</typeparam>
/// <typeparam name="TKey">Key type of the set.</typeparam>
public interface IFluxSetContext<TModel, TKey>
    where TModel : class
    where TKey : notnull
{
    // ============================== GetAsync ==============================

    /// <inheritdoc cref="GetAsync{TResponse}(TKey, OperationParameterCollection?, CancellationToken)"/>
    public Task<TModel> GetAsync(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAsync{TResponse}(TKey, OperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> GetAsync<TResponse>(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAsync{TResponse}(TKey, OperationParameterCollection?, CancellationToken)"/>
    public Task<TModel> GetAsync(TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="id">Unique identifier of the object to fetch.</param>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAsync<TResponse>(TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAsync{TResponse}(Action{GetOperationDescriptor}, CancellationToken)"/>
    public Task<TModel> GetAsync(Action<GetOperationDescriptor> configureOperation, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAsync{TResponse}(GetOperationDescriptor, CancellationToken)"/>
    public Task<TModel> GetAsync(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <param name="configureOperation">Operation configuration.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAsync<TResponse>(Action<GetOperationDescriptor> configureOperation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches an object from the set.
    /// </summary>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAsync<TResponse>(GetOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== GetAllAsync ==============================

    /// <inheritdoc cref="GetAllAsync{TResponse}(OperationParameterCollection?, CancellationToken)"/>
    public Task<IEnumerable<TModel>> GetAllAsync(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="parameters">Parameters to be used in the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAllAsync<TResponse>(OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAllAsync{TResponse}(Action{GetAllOperationDescriptor}, CancellationToken)"/>
    public Task<IEnumerable<TModel>> GetAllAsync(Action<GetAllOperationDescriptor> configureOperation, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAllAsync{TResponse}(GetAllOperationDescriptor, CancellationToken)"/>
    public Task<IEnumerable<TModel>> GetAllAsync(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="configureOperation">Operation configuration.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAllAsync<TResponse>(Action<GetAllOperationDescriptor> configureOperation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetAllAsync<TResponse>(GetAllOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== GetPageAsync ==============================

    /// <inheritdoc cref="GetPageAsync{TResponse}(int, int, OperationParameterCollection?, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="offset">Page offset.</param>
    /// <param name="limit">Page limit.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetPageAsync<TResponse>(int offset, int limit, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetPageAsync{TResponse}(PageRequest, OperationParameterCollection?, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="pageRequest">A <see cref="PageRequest"/> containing page request parameters.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetPageAsync<TResponse>(PageRequest pageRequest, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetPageAsync{TResponse}(GetPageOperationDescriptor, CancellationToken)"/>
    public Task<PageResult<TModel>> GetPageAsync(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> GetPageAsync<TResponse>(GetPageOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== AddAsync ==============================

    /// <inheritdoc cref="AddAsync{TResponse}(TModel, TKey, OperationParameterCollection?, CancellationToken)"/>
    public Task AddAsync(TModel value, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AddAsync{TResponse}(TModel, TKey, OperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> AddAsync<TResponse>(TModel value, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AddAsync{TResponse}(TModel, TKey, OperationParameterCollection?, CancellationToken)"/>
    public Task AddAsync(TModel value, TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new object to the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="value">The value to add to the set.</param>
    /// <param name="id">
    /// Unique identifier of the object to add.
    /// <para>
    /// <b>Note:</b> For some <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux implementations</see>,<br/>
    /// the presence of this identifier may result in a keyed operation (e.g. 'PUT' in REST),<br/>
    /// rather than a non-keyed operation (e.g. 'POST' in REST).
    /// </para>
    /// </param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> AddAsync<TResponse>(TModel value, TKey id, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AddAsync{TResponse}(AddOperationDescriptor, CancellationToken)"/>
    public Task AddAsync(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new object to the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> AddAsync<TResponse>(AddOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== UpdateAsync ==============================

    /// <inheritdoc cref="UpdateAsync(TModel, TKey, bool, OperationParameterCollection?, CancellationToken)"/>
    public Task UpdateAsync(TModel value, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="UpdateAsync{TResponse}(TModel, TKey, bool, OperationParameterCollection?, CancellationToken)"/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="UpdateAsync{TResponse}(TModel, TKey, bool, OperationParameterCollection?, CancellationToken)"/>
    public Task UpdateAsync(TModel value, TKey id, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing object in the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="value">The value to update the existing object with.</param>
    /// <param name="id">Unique identifier of the object to update.</param>
    /// <param name="parameters">Parameters used by the operation.</param>
    /// <param name="partial">Whether to perform a partial update (e.g. PATCH in REST), rather than a full update (e.g. PUT in REST).</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> UpdateAsync<TResponse>(TModel value, TKey id, bool partial = false, OperationParameterCollection? parameters = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="UpdateAsync{TResponse}(UpdateOperationDescriptor, CancellationToken)"/>
    public Task UpdateAsync(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing object in the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> UpdateAsync<TResponse>(UpdateOperationDescriptor descriptor, CancellationToken cancellationToken = default);

    // ============================== ExecuteAsync ==============================

    /// <summary>
    /// Executes an operation on the set.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResponse> ExecuteAsync<TResponse>(OperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="ExecuteAsync(OperationDescriptor, Type?, CancellationToken)"/>
    public Task ExecuteAsync(OperationDescriptor descriptor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes an operation on the set.
    /// </summary>
    /// <param name="descriptor">Operation descriptor.</param>
    /// <param name="responseType">Type to deserialize the response to (if any).</param>
    /// <param name="cancellationToken">Cancellation token for this operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default);
}
