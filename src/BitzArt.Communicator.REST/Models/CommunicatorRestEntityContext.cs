using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Web;

namespace BitzArt.Communicator;

internal class CommunicatorRestEntityContext<TEntity> : ICommunicationContext<TEntity>
    where TEntity : class
{
    internal readonly HttpClient HttpClient;
    internal readonly CommunicatorRestServiceOptions ServiceOptions;
    internal readonly ILogger _logger;

    protected CommunicatorRestEntityOptions<TEntity> _entityOptions;
    internal virtual CommunicatorRestEntityOptions<TEntity> EntityOptions
    {
        get => _entityOptions;
        set => _entityOptions = value;
    }

    internal string GetFullPath(string path)
        => HttpClient.BaseAddress is not null ?
        Path.Combine(HttpClient.BaseAddress.ToString(), path) :
        path;

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

    public CommunicatorRestEntityContext(HttpClient httpClient, CommunicatorRestServiceOptions serviceOptions, ILogger logger, CommunicatorRestEntityOptions<TEntity> entityOptions)
    {
        HttpClient = httpClient;
        ServiceOptions = serviceOptions;
        _logger = logger;
        EntityOptions = entityOptions;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var path = EntityOptions.Endpoint is not null ? EntityOptions.Endpoint : string.Empty;
        if (HttpClient.BaseAddress is not null) path = Path.Combine(HttpClient.BaseAddress.ToString(), path);

        _logger.LogInformation("GetAll {type}: {path}", typeof(TEntity).Name, GetFullPath(path));

        var msg = new HttpRequestMessage(HttpMethod.Get, path);
        var result = await HandleRequestAsync<IEnumerable<TEntity>>(msg);

        return result;
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(int offset, int limit) => await GetPageAsync(new PageRequest(offset, limit));

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest)
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

        _logger.LogInformation("GetPage {type}: {path}", typeof(TEntity).Name, GetFullPath(path));

        var response = await HttpClient.GetAsync(path);

        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<PageResult<TEntity>>(content, ServiceOptions.SerializerOptions)!;

        return result;
    }

    public virtual async Task<TEntity> GetAsync(object id)
    {
        if (EntityOptions.GetIdEndpointAction is null) throw new KeyNotFoundException();

        var idEndpoint = EntityOptions.GetIdEndpointAction(id);
        _logger.LogInformation("Get {type}[{id}]: {path}", typeof(TEntity).Name, id.ToString(), GetFullPath(idEndpoint));
        var response = await HttpClient.GetAsync(idEndpoint);

        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TEntity>(content, ServiceOptions.SerializerOptions)!;

        return result;
    }
}

internal class CommunicatorRestEntityContext<TEntity, TKey> : CommunicatorRestEntityContext<TEntity>, ICommunicationContext<TEntity, TKey>
    where TEntity : class
{
    internal new CommunicatorRestEntityOptions<TEntity, TKey> EntityOptions
    {
        get => (CommunicatorRestEntityOptions<TEntity, TKey>)_entityOptions;
        set
        {
            _entityOptions = value;
        }
    }

    public CommunicatorRestEntityContext(HttpClient httpClient, CommunicatorRestServiceOptions serviceOptions, ILogger logger, CommunicatorRestEntityOptions<TEntity, TKey> entityOptions)
        : base(httpClient, serviceOptions, logger, entityOptions)
    {
        EntityOptions = entityOptions;
    }

    public override Task<TEntity> GetAsync(object id) => GetAsync((TKey)id);

    public async Task<TEntity> GetAsync(TKey id)
    {
        string idEndpoint;

        if (EntityOptions.GetIdEndpointAction is not null) idEndpoint = EntityOptions.GetIdEndpointAction(id);
        else idEndpoint = EntityOptions.Endpoint is not null ? Path.Combine(EntityOptions.Endpoint, id!.ToString()!) : id!.ToString()!;

        _logger.LogInformation("Get {type}[{id}]: {path}", typeof(TEntity).Name, id!.ToString(), GetFullPath(idEndpoint));

        var response = await HttpClient.GetAsync(idEndpoint);
        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TEntity>(content, ServiceOptions.SerializerOptions)!;

        return result;
    }
}
