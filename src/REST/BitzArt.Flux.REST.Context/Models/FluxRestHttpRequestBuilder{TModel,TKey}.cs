namespace BitzArt.Flux;

internal class FluxRestHttpRequestBuilder<TModel, TKey>(
    FluxRestSetContext<TModel, TKey> set
    ) : FluxRestHttpRequestBuilder<TModel>(set)
    where TModel : class
{
}
