using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Web;

namespace BitzArt.Flux;

internal class FluxRestEntityContext<TEntity> : IFluxEntityContext<TEntity>
    where TEntity : class
{
    internal readonly HttpClient HttpClient;
    internal readonly FluxRestServiceOptions ServiceOptions;
    internal readonly ILogger _logger;

    protected FluxRestEntityOptions<TEntity> _entityOptions;
    internal virtual FluxRestEntityOptions<TEntity> EntityOptions
    {
        get => _entityOptions;
        set => _entityOptions = value;
    }

    internal string GetFullPath(string path, bool handleParameters, object[]? parameters = null)
    {
        if (ServiceOptions.BaseUrl is null) return path;
        
        var p1 = ServiceOptions.BaseUrl.TrimEnd('/');
        var p2 = path.TrimStart('/');
        var result = $"{p1}/{p2}";

        if (handleParameters) result = RequestParameterParsingUtility.ParseRequestUrl(_logger, result, parameters);
        
        return result;
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
        private static readonly string Msg = $"Unable to find TKey for type '{typeof(TEntity).Name}'. Consider specifying a key when registering the entity.";
        public KeyNotFoundException() : base(Msg) { }
    }

    public FluxRestEntityContext(HttpClient httpClient, FluxRestServiceOptions serviceOptions, ILogger logger, FluxRestEntityOptions<TEntity> entityOptions)
    {
        HttpClient = httpClient;
        ServiceOptions = serviceOptions;
        _logger = logger;
        _entityOptions = entityOptions;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(params object[]? parameters)
    {
        var path = EntityOptions.Endpoint is not null ? EntityOptions.Endpoint : string.Empty;
        var route = GetFullPath(path, true, parameters);

        _logger.LogInformation("GetAll {type}: {path}", typeof(TEntity).Name, route);

        var message = new HttpRequestMessage(HttpMethod.Get, route);
        var result = await HandleRequestAsync<IEnumerable<TEntity>>(message);

        return result;
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(int offset, int limit, params object[]? parameters) => await GetPageAsync(new PageRequest(offset, limit), parameters);

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        var path = EntityOptions.Endpoint is not null ? EntityOptions.Endpoint : string.Empty;
        var queryIndex = path.IndexOf('?');

        var query = queryIndex == -1 ?
            HttpUtility.ParseQueryString(string.Empty) :
            HttpUtility.ParseQueryString(path.Substring(queryIndex));

        query.Add("offset", pageRequest.Offset?.ToString());
        query.Add("limit", pageRequest.Limit?.ToString());

        if (queryIndex != -1) path = path[..queryIndex];
        path = path + "?" + query.ToString();

        var route = GetFullPath(path, true, parameters);
        _logger.LogInformation("GetPage {type}: {route}", typeof(TEntity).Name, route);

        var message = new HttpRequestMessage(HttpMethod.Get, route);
        var result = await HandleRequestAsync<PageResult<TEntity>>(message);

        return result;
    }

    public virtual async Task<TEntity> GetAsync(object? id, params object[]? parameters)
    {
        if (EntityOptions.GetIdEndpointAction is null) throw new KeyNotFoundException();

        var idEndpoint = EntityOptions.GetIdEndpointAction(id, parameters);
        var route = GetFullPath(idEndpoint, false);
        _logger.LogInformation("Get {type}[{id}]: {route}", typeof(TEntity).Name, id is not null ? id.ToString() : "_", route);

        var message = new HttpRequestMessage(HttpMethod.Get, route);
        var result = await HandleRequestAsync<TEntity>(message);

        return result;
    }
}

internal class FluxRestEntityContext<TEntity, TKey> : FluxRestEntityContext<TEntity>, IFluxEntityContext<TEntity, TKey>
    where TEntity : class
{
    internal new FluxRestEntityOptions<TEntity, TKey> EntityOptions
    {
        get => (FluxRestEntityOptions<TEntity, TKey>)_entityOptions;
        set
        {
            _entityOptions = value;
        }
    }

    public FluxRestEntityContext(HttpClient httpClient, FluxRestServiceOptions serviceOptions, ILogger logger, FluxRestEntityOptions<TEntity, TKey> entityOptions)
        : base(httpClient, serviceOptions, logger, entityOptions)
    {
        EntityOptions = entityOptions;
    }

    public override Task<TEntity> GetAsync(object? id, params object[]? parameters) => GetAsync((TKey?)id, parameters);

    public async Task<TEntity> GetAsync(TKey? id, params object[]? parameters)
    {
        string idEndpoint;

        bool handleParameters = false;
        if (EntityOptions.GetIdEndpointAction is not null)
        {
            idEndpoint = EntityOptions.GetIdEndpointAction(id, parameters);
        }
        else
        {
            idEndpoint = EntityOptions.Endpoint is not null ? Path.Combine(EntityOptions.Endpoint, id!.ToString()!) : id!.ToString()!;
            handleParameters = true;
        }
        var route = GetFullPath(idEndpoint, handleParameters, parameters);

        _logger.LogInformation("Get {type}[{id}]: {route}", typeof(TEntity).Name, id!.ToString(), route);

        var message = new HttpRequestMessage(HttpMethod.Get, route);
        var result = await HandleRequestAsync<TEntity>(message);

        return result;
    }
}
