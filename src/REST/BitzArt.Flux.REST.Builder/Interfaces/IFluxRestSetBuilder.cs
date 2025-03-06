using BitzArt.Flux.REST;

namespace BitzArt.Flux;

/// <summary>
/// Flux REST set builder.
/// </summary>
/// <typeparam name="TModel">
/// The model type of the set.
/// </typeparam>
/// <typeparam name="TKey">
/// The key type of the set.
/// </typeparam>
public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestServiceBuilder
    where TModel : class
{
    internal IFluxRestSetOptions<TModel> SetOptions { get; }

    /// <summary>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TParameters>(string endpoint)
        where TParameters : IOperationParameterCollection?
        => this.WithEndpoint<TModel, TKey, TParameters?>(endpoint);

    /// <summary>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TInputParameters>(
        string endpoint,
        Func<TInputParameters?, RestRequestParameters> transformParameters)
        where TInputParameters : IOperationParameterCollection?
        => this.WithEndpoint<TModel, TKey, TInputParameters?>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TOutputParameters>(
        string endpoint,
        Func<RequestParameters?, TOutputParameters> transformParameters)
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithEndpoint<TModel, TKey, TOutputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpointExtension.WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TInputParameters, TOutputParameters>(
        string endpoint,
        Func<TInputParameters?, TOutputParameters> transformParameters)
        where TInputParameters : IOperationParameterCollection?
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithEndpoint<TModel, TKey, TInputParameters?, TOutputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    ///  <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithPageEndpoint<TParameters>(string endpoint)
        where TParameters : IOperationParameterCollection?
        => this.WithPageEndpoint<TModel, TKey, TParameters?>(endpoint);

    /// <summary>
    /// <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    ///  <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithPageEndpoint<TInputParameters>(
        string endpoint,
        Func<TInputParameters?, RestRequestParameters> transformParameters)
        where TInputParameters : IOperationParameterCollection?
        => this.WithPageEndpoint<TModel, TKey, TInputParameters?>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    ///  <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithPageEndpoint<TOutputParameters>(
        string endpoint,
        Func<RequestParameters?, TOutputParameters> transformParameters)
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithPageEndpoint<TModel, TKey, TOutputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    ///  <inheritdoc cref="WithPageEndpointExtension.WithPageEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithPageEndpoint<TInputParameters, TOutputParameters>(
        string endpoint,
        Func<TInputParameters?, TOutputParameters> transformParameters)
        where TInputParameters : notnull, IOperationParameterCollection
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithPageEndpoint<TModel, TKey, TInputParameters, TOutputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TParameters>(string endpoint)
        where TParameters : notnull, IOperationParameterCollection
        => this.WithIdEndpoint<TModel, TKey, TParameters>(endpoint);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TParameters>(Func<TKey?, string> getEndpoint)
        where TParameters : notnull, IOperationParameterCollection
        => this.WithIdEndpoint<TModel, TKey, TParameters>(getEndpoint);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TInputParameters>(
        string endpoint,
        Func<TInputParameters?, RestRequestParameters> transformParameters)
        where TInputParameters : notnull, IOperationParameterCollection
        => this.WithIdEndpoint<TModel, TKey, TInputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TOutputParameters>(
        string endpoint,
        Func<RequestParameters?, TOutputParameters> transformParameters)
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithIdEndpoint<TModel, TKey, TOutputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TInputParameters, TOutputParameters>(
        string endpoint,
        Func<TInputParameters?, TOutputParameters> transformParameters)
        where TInputParameters : notnull, IOperationParameterCollection
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithIdEndpoint<TModel, TKey, TInputParameters, TOutputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TInputParameters>(
        Func<TKey, string> getEndpoint,
        Func<TInputParameters?, RestRequestParameters> transformParameters)
        where TInputParameters : notnull, IOperationParameterCollection
        => this.WithIdEndpoint<TModel, TKey, TInputParameters>(getEndpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TOutputParameters>(
        Func<TKey, string> getEndpoint,
        Func<RequestParameters?, TOutputParameters> transformParameters)
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithIdEndpoint<TModel, TKey, TOutputParameters>(getEndpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithIdEndpointExtension.WithIdEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TInputParameters, TOutputParameters>(
        Func<TKey, string> getEndpoint,
        Func<TInputParameters, TOutputParameters> transformParameters)
        where TInputParameters : notnull, IOperationParameterCollection
        where TOutputParameters : IFluxRestOperationParameters
        => this.WithIdEndpoint<TModel, TKey, TInputParameters, TOutputParameters>(getEndpoint, transformParameters);
}
