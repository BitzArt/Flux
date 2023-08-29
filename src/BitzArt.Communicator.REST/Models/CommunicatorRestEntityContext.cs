using BitzArt.Pagination;
using System.IO;
using System.Text.Json;
using System.Web;

namespace BitzArt.Communicator;

internal class CommunicatorRestEntityContext<TEntity> : ICommunicationContext<TEntity>
    where TEntity : class
{
    internal readonly HttpClient HttpClient;
    internal readonly CommunicatorRestServiceOptions ServiceOptions;
    internal readonly CommunicatorRestEntityOptions<TEntity> EntityOptions;

    private class KeyNotFoundException : Exception
    {
        private static readonly string Msg = $"Unable to find TKey for type '{typeof(TEntity).Name}'. Consider specifying a key when registering the entity.";
        public KeyNotFoundException() : base(Msg) { }
    }

    public CommunicatorRestEntityContext(HttpClient httpClient, CommunicatorRestServiceOptions serviceOptions)
    {
        HttpClient = httpClient;
        ServiceOptions = serviceOptions;
        EntityOptions = null!;
    }

    public CommunicatorRestEntityContext(HttpClient httpClient, CommunicatorRestServiceOptions serviceOptions, CommunicatorRestEntityOptions<TEntity> entityOptions)
        : this(httpClient, serviceOptions)
    {
        EntityOptions = entityOptions;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var path = EntityOptions.Endpoint is not null ? EntityOptions.Endpoint : string.Empty;
        if (HttpClient.BaseAddress is not null) path = Path.Combine(HttpClient.BaseAddress.ToString(), path);
        var response = await HttpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IEnumerable<TEntity>>(content, ServiceOptions.SerializerOptions)!;

        return result;
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest)
    {
        var path = EntityOptions.Endpoint is not null ? EntityOptions.Endpoint : string.Empty;

        var query = HttpUtility.ParseQueryString(path);
        query.Add("offset", pageRequest.Offset?.ToString());
        query.Add("limit", pageRequest.Limit?.ToString());

        var queryIndex = path.IndexOf('?');
        if (queryIndex != -1) path = path[..queryIndex];
        path = path + "?" + query.ToString();

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
    public new readonly CommunicatorRestEntityOptions<TEntity, TKey> EntityOptions;

    public CommunicatorRestEntityContext(HttpClient httpClient, CommunicatorRestServiceOptions serviceOptions, CommunicatorRestEntityOptions<TEntity, TKey> entityOptions)
        : base(httpClient, serviceOptions)
    {
        EntityOptions = entityOptions;
    }

    public override Task<TEntity> GetAsync(object id) => GetAsync((TKey)id);

    public async Task<TEntity> GetAsync(TKey id)
    {
        string idEndpoint;

        if (EntityOptions.GetIdEndpointAction is not null) idEndpoint = EntityOptions.GetIdEndpointAction(id);
        else idEndpoint = EntityOptions.Endpoint is not null ? Path.Combine(EntityOptions.Endpoint, id!.ToString()!) : id!.ToString()!;

        var response = await HttpClient.GetAsync(idEndpoint);
        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TEntity>(content, ServiceOptions.SerializerOptions)!;

        return result;
    }
}
