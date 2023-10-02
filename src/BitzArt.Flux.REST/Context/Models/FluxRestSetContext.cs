using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Web;

namespace BitzArt.Flux;

internal class FluxRestSetContext<TModel> : IFluxSetContext<TModel>
    where TModel : class
{
    internal readonly HttpClient HttpClient;
    internal readonly FluxRestServiceOptions ServiceOptions;
    internal readonly ILogger _logger;

    protected FluxRestSetOptions<TModel> _setOptions;
    internal virtual FluxRestSetOptions<TModel> SetOptions
    {
        get => _setOptions;
        set => _setOptions = value;
    }

    internal RequestUrlParameterParsingResult GetFullPath(string path, bool handleParameters, object[]? parameters = null)
    {
        if (ServiceOptions.BaseUrl is null) return new RequestUrlParameterParsingResult(path, string.Empty);

        var baseUrl = ServiceOptions.BaseUrl.TrimEnd('/');
        var localPath = path.TrimStart('/');
        var resultPath = $"{baseUrl}/{localPath}";

        if (handleParameters) return RequestParameterParsingUtility.ParseRequestUrl(_logger, resultPath, parameters);

        return new RequestUrlParameterParsingResult(resultPath, string.Empty);
    }

    internal async Task<TResult> HandleRequestAsync<TResult>(HttpRequestMessage message) where TResult : class
    {
        try
        {
            var response = await HttpClient.SendAsync(message);
            if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TResult>(content, ServiceOptions.SerializerOptions)!;

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error has occured while processing http request. See inner exception for details.", ex);
        }
    }

    private class KeyNotFoundException : Exception
    {
        private static readonly string Msg = $"Unable to find TKey for type '{typeof(TModel).Name}'. Consider specifying a key when registering the set.";
        public KeyNotFoundException() : base(Msg) { }
    }

    public FluxRestSetContext(HttpClient httpClient, FluxRestServiceOptions serviceOptions, ILogger logger, FluxRestSetOptions<TModel> setOptions)
    {
        HttpClient = httpClient;
        ServiceOptions = serviceOptions;
        _logger = logger;
        _setOptions = setOptions;
    }

    public virtual async Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters)
    {
        var path = SetOptions.Endpoint is not null ? SetOptions.Endpoint : string.Empty;
        var parse = GetFullPath(path, true, parameters);

        _logger.LogInformation("GetAll {type}: {route}{parsingLog}", typeof(TModel).Name, parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<IEnumerable<TModel>>(message);

        return result;
    }

    public virtual async Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters) => await GetPageAsync(new PageRequest(offset, limit), parameters);

    public virtual async Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        var path = GetPageEndpoint();

        var queryIndex = path.IndexOf('?');

        var query = queryIndex == -1 ?
            HttpUtility.ParseQueryString(string.Empty) :
            HttpUtility.ParseQueryString(path.Substring(queryIndex));

        query.Add("offset", pageRequest.Offset?.ToString());
        query.Add("limit", pageRequest.Limit?.ToString());

        if (queryIndex != -1) path = path[..queryIndex];
        path = path + "?" + query.ToString();

        var parse = GetFullPath(path, true, parameters);
        _logger.LogInformation("GetPage {type}: {route}{parsingLog}", typeof(TModel).Name, parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<PageResult<TModel>>(message);

        return result;
    }

    public virtual async Task<TModel> GetAsync(object? id, params object[]? parameters)
    {
        if (SetOptions.GetIdEndpointAction is null) throw new KeyNotFoundException();

        var idEndpoint = SetOptions.GetIdEndpointAction(id, parameters);
        var parse = GetFullPath(idEndpoint, false);
        _logger.LogInformation("Get {type}[{id}]: {route}{parsingLog}", typeof(TModel).Name, id is not null ? id.ToString() : "_", parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<TModel>(message);

        return result;
    }

    protected string GetPageEndpoint()
    {
        if (SetOptions.PageEndpoint is not null) return SetOptions.PageEndpoint;
        return GetEndpoint();
    }

    protected string GetEndpoint()
    {
        if (SetOptions.Endpoint is null) return string.Empty;
        return SetOptions.Endpoint;
    }
}

internal class FluxRestSetContext<TModel, TKey> : FluxRestSetContext<TModel>, IFluxSetContext<TModel, TKey>
    where TModel : class
{
    internal new FluxRestSetOptions<TModel, TKey> SetOptions
    {
        get => (FluxRestSetOptions<TModel, TKey>)_setOptions;
        set
        {
            _setOptions = value;
        }
    }

    public FluxRestSetContext(HttpClient httpClient, FluxRestServiceOptions serviceOptions, ILogger logger, FluxRestSetOptions<TModel, TKey> setOptions)
        : base(httpClient, serviceOptions, logger, setOptions)
    {
        SetOptions = setOptions;
    }

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
