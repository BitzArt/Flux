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

    public FluxRestSetContext(
        HttpClient httpClient,
        FluxRestServiceOptions serviceOptions,
        ILogger logger,
        FluxRestSetOptions<TModel, TKey> setOptions)
        : base(httpClient, serviceOptions, logger, setOptions)
    {
        SetOptions = setOptions;
    }

    // ============== Data methods implementation ==============

    public override Task<TModel> GetAsync(object? id, params object[]? parameters) => GetAsync((TKey?)id, parameters);

    public async Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        var parse = GetIdEndpointFullPath(id, parameters);

        _logger.LogInformation("Get {type}[{id}]: {route}{parsingLog}", typeof(TModel).Name, id!.ToString(), parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<TModel>(message);

        return result;
    }

    public async Task<TModel> UpdateAsync(TKey? id, TModel model, bool partial = false, params object[]? parameters)
        => await UpdateAsync<TModel>(id, model, partial, parameters);

    public async Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters)
        => await base.UpdateAsync<TResponse>(id, model, partial, parameters);

    protected override RequestUrlParameterParsingResult GetIdEndpointFullPath(object? id, params object[]? parameters)
    {
        if (SetOptions.GetIdEndpointAction is not null)
        {
            if (id is null)
            {
                var action = SetOptions.BaseGetIdEndpointAction!;
                return GetFullPath(action(id, parameters), false, parameters);
            }

            if (id is not TKey idCasted) throw new ArgumentException($"Id must be of type {typeof(TKey).Name}");

            var idEndpoint = SetOptions.GetIdEndpointAction(idCasted, parameters);
            return GetFullPath(idEndpoint, false, parameters);
        }
        else
        {
            var idEndpoint = SetOptions.Endpoint is not null ? Path.Combine(SetOptions.Endpoint, id!.ToString()!) : id!.ToString()!;
            return GetFullPath(idEndpoint, true, parameters);
        }
    }
}
