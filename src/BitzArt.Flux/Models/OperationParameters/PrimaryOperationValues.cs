using BitzArt.Pagination;

namespace BitzArt.Flux;

internal class PrimaryOperationValues : IKeyedOperationParameters, IPagedOperationParameters, IModelOperationParameters
{
    private object? _id;
    public object Id
    {
        get => FetchParameter(() => _id, nameof(Id));
        set => _id = value;
    }

    private PageRequest? _pageRequest;
    public PageRequest PageRequest
    {
        get => FetchParameter(() => _pageRequest, nameof(PageRequest));
        set => _pageRequest = value;
    }

    private object? _model;
    public object Model
    {
        get => FetchParameter(() => _model, nameof(Model));
        set => _model = value;
    }

    private static TParameter FetchParameter<TParameter>(Func<TParameter?> fetch, string name)
    {
        var parameter = fetch.Invoke() ?? throw new InvalidOperationException($"Missing required operation parameter '{name}'.");
        return parameter;
    }
}
