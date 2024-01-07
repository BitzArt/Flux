namespace BitzArt.Flux;

public class FluxRestSetOptions<TModel>
    where TModel : class
{
    public string? Endpoint { get; set; }
    public string? PageEndpoint { get; set; }
    protected Func<object?, object[]?, string>? _getIdEndpointAction;
    public Func<object?, object[]?, string>? GetIdEndpointAction
    {
        get => _getIdEndpointAction;
        set => _getIdEndpointAction = value;
    }

    public FluxRestSetOptions()
    {
        GetIdEndpointAction = null;
    }
}