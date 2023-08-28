using BitzArt.Pagination;
using System.Text.Json;

namespace BitzArt.Communicator;

internal class RestEntityCommunicator<TEntity> : IEntityCommunicator<TEntity>
    where TEntity : class
{
    internal readonly HttpClient HttpClient;
    internal readonly RestCommunicatorServiceOptions Options;
    internal readonly string? Endpoint;

    private class KeyNotFoundException : Exception
    {
        private static readonly string Msg = $"Unable to find TKey for type '{typeof(TEntity).Name}'. Consider specifying a key when registering the entity.";
        public KeyNotFoundException() : base(Msg) { }
    }

    public RestEntityCommunicator(HttpClient httpClient, RestCommunicatorServiceOptions options, string? endpoint)
    {
        HttpClient = httpClient;
        Options = options;
        Endpoint = endpoint;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        var path = Endpoint is not null ? Endpoint : string.Empty;
        var response = await HttpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IEnumerable<TEntity>>(content, Options.SerializerOptions)!;

        return result;
    }

    public virtual Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest)
    {
        throw new KeyNotFoundException();
    }

    public virtual Task<TEntity> GetAsync(object id)
    {
        throw new KeyNotFoundException();
    }
}

internal sealed class RestEntityCommunicator<TEntity, TKey> : RestEntityCommunicator<TEntity>, IEntityCommunicator<TEntity, TKey>
    where TEntity : class
{

    public RestEntityCommunicator(HttpClient httpClient, RestCommunicatorServiceOptions options, string? endpoint)
        : base(httpClient, options, endpoint) { }

    public override async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest)
    {
        throw new NotImplementedException();
    }

    public override Task<TEntity> GetAsync(object id) => GetAsync((TKey)id);

    public async Task<TEntity> GetAsync(TKey id)
    {
        var path = Endpoint is not null ? Path.Combine(Endpoint, id!.ToString()!) : id!.ToString()!;
        var response = await HttpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TEntity>(content, Options.SerializerOptions)!;

        return result;
    }
}
