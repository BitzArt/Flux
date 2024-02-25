using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxRestSetContext<TModel, TKey> : FluxRestSetContext<TModel>, IFluxSetContext<TModel, TKey>
    where TModel : class
{
    // ================ Flux internal wiring ================

    internal new FluxRestSetOptions<TModel, TKey> SetOptions
    {
        get => (FluxRestSetOptions<TModel, TKey>)_setOptions;
        set => _setOptions = value;
    }

    // ==================== Constructor ====================

    public FluxRestSetContext(HttpClient httpClient, FluxRestServiceOptions serviceOptions, ILogger logger, FluxRestSetOptions<TModel, TKey> setOptions)
        : base(httpClient, serviceOptions, logger, setOptions)
    {
        SetOptions = setOptions;
    }

    // ============== IQueryable implementation ==============

    public override IQueryProvider Provider => new FluxRestQueryProvider<TModel, TKey>(this);

    // ============== Data methods implementation ==============

    public override Task<TModel> GetAsync(object? id, params object[]? parameters) => GetAsync((TKey?)id, parameters);

    public async Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        string idEndpoint;

        bool handleParameters = false;
        if (SetOptions.GetIdEndpointAction is not null)
        {
            idEndpoint = SetOptions.GetIdEndpointAction(id, parameters);
        }
        else
        {
            idEndpoint = SetOptions.Endpoint is not null ? Path.Combine(SetOptions.Endpoint, id!.ToString()!) : id!.ToString()!;
            handleParameters = true;
        }
        var parse = GetFullPath(idEndpoint, handleParameters, parameters);

        _logger.LogInformation("Get {type}[{id}]: {route}{parsingLog}", typeof(TModel).Name, id!.ToString(), parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<TModel>(message);

        return result;
    }
}
