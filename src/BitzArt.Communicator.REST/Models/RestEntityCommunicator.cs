using BitzArt.Pagination;
using System.Text.Json;

namespace BitzArt.Communicator;

internal class RestEntityCommunicator<TEntity, TKey> : EntityCommunicatorBase<TEntity, TKey>
    where TEntity : class
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;

    public RestEntityCommunicator(HttpClient httpClient, string endpoint)
    {
        _httpClient = httpClient;
        _endpoint = endpoint;
    }

    public override async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest)
    {
        throw new NotImplementedException();
    }

    public override async Task<TEntity> GetAsync(TKey id)
    {
        var response = await _httpClient.GetAsync(Path.Combine(_endpoint, id!.ToString()!));
        if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TEntity>(content)!;

        return result;
    }
}
