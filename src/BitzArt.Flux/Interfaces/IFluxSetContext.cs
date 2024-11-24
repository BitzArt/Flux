using BitzArt.Pagination;

namespace BitzArt.Flux;

/// <summary>
/// Flux context for a preconfigured data Set.<br/>
/// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see>
/// for more information on how to use it.
/// </summary>
public interface IFluxSetContext<TModel>
    where TModel : class
{
    // ============== GET ALL ==============

    /// <summary>
    /// Fetches all objects from the set.
    /// </summary>
    public Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters);

    // ============== GET (Single) ==============

    /// <summary>
    /// Fetches a single object from the set by its key.
    /// </summary>
    public Task<TModel> GetAsync(object? id = null, params object[]? parameters);

    // ============== GET PAGE ==============

    /// <summary>
    /// Fetches a page of objects from the set.
    /// </summary>
    public Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters);

    /// <inheritdoc cref="GetPageAsync(int, int, object[])"/>
    public Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters);

    // ============== ADD ==============

    /// <summary>
    /// Adds a new object to the set.
    /// </summary>
    public Task<TModel> AddAsync(TModel model, params object[]? parameters);

    /// <summary>
    /// Adds a new object to the set and returns a specific response type.
    /// </summary>
    public Task<TResponse> AddAsync<TResponse>(TModel model, params object[]? parameters);

    // ============== UPDATE ==============

    /// <summary>
    /// Updates an object in the set.
    /// </summary>
    public Task<TModel> UpdateAsync(object? id, TModel model, bool partial = false, params object[]? parameters);

    /// <inheritdoc cref="UpdateAsync(object, TModel, bool, object[])"/>
    public Task<TModel> UpdateAsync(TModel model, bool partial = false, params object[]? parameters);

    /// <summary>
    /// Updates an object in the set and returns a specific response type.
    /// </summary>
    public Task<TResponse> UpdateAsync<TResponse>(object? id, TModel model, bool partial = false, params object[]? parameters);

    /// <inheritdoc/>
    public Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, params object[]? parameters);

    // ============== CAST ==============

    /// <summary>
    /// Casts the context to a different model type.
    /// </summary>
    /// <typeparam name="TResult"> The new model type. </typeparam>
    /// <returns> A new context with the specified model type. </returns>
    public IFluxSetContext<TResult> Cast<TResult>()
        where TResult : class;
}

/// <inheritdoc/>"
public interface IFluxSetContext<TModel, TKey> : IFluxSetContext<TModel>
    where TModel : class
{
    /// <inheritdoc cref="IFluxSetContext{TModel}.GetAsync(object?, object[])"/>"
    public Task<TModel> GetAsync(TKey? id, params object[]? parameters);

    /// <inheritdoc cref="IFluxSetContext{TModel}.UpdateAsync(object?, TModel, bool, object[])"/>
    public Task<TModel> UpdateAsync(TKey? id, TModel model, bool partial = false, params object[]? parameters);

    /// <inheritdoc cref="IFluxSetContext{TModel}.UpdateAsync(TModel, bool, object[])"/>
    public Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters);
}