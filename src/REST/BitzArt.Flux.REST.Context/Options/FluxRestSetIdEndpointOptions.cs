namespace BitzArt.Flux.REST;

internal class FluxRestSetIdEndpointOptions<TModel, TKey>
    : FluxRestSetEndpointOptions<TModel, TKey>, IFluxRestSetIdEndpointOptions<TModel>
    where TModel : class
{
    internal Func<TKey?, object[]?, string>? GetPathFunc { get; set; }

    Func<object?, object[]?, string>? IFluxRestSetIdEndpointOptions<TModel>.GetPathFunc
    {
        get => GetPathFunc is null ? null : (key, parameters) =>
        {
            if (key is not TKey keyTyped) throw new InvalidOperationException($"Key type mismatch. Expected {typeof(TKey)}, but got {key?.GetType()}.");
            return GetPathFunc.Invoke(keyTyped, parameters);
        };

        set
        {
            if (value is null)
            {
                GetPathFunc = null;
                return;
            }

            GetPathFunc = (key, parameters) => value.Invoke(key, parameters);
        }
    }
}