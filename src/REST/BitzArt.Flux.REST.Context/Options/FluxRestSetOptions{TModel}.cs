namespace BitzArt.Flux;

internal class FluxRestSetOptions<TModel>
    where TModel : class
{
    public string? Endpoint { get; set; }
    public string? PageEndpoint { get; set; }

    private Func<object?, object[]?, string>? _getIdEndpointAction;

    protected internal Func<object?, object[]?, string>? GetIdEndpointAction
    {
        get => GetIdEndpointActionInternal;
        set => GetIdEndpointActionInternal = value;
    }

    protected internal virtual Func<object?, object[]?, string>? GetIdEndpointActionInternal
    {
        get => _getIdEndpointAction;
        set => _getIdEndpointAction = value;
    }

    internal virtual Type? KeyType => null;

    public FluxRestSetOptions()
    {
        GetIdEndpointAction = null;
    }
}