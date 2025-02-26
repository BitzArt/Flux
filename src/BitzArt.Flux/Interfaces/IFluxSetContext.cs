namespace BitzArt.Flux;

/// <inheritdoc cref="IFluxSetContext{TModel, TKey}"/>
public interface IFluxSetContext<TModel> : IFluxSetParameterlessOperations<TModel>, IFluxSetParameterizedOperations<TModel>
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
public interface IFluxSetContext<TModel, TKey> : IFluxSetContext<TModel>, IFluxSetParameterlessOperations<TModel, TKey>, IFluxSetParameterizedOperations<TModel, TKey>
    where TModel : class
    where TKey : notnull
{
}
