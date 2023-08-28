using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public class RestCommunicatorServiceProvider : ICommunicatorServiceProvider
{
    private ICollection<CommunicatorEntitySignature> _entitySignatures;
    private RestCommunicatorServiceOptions _options;

    public string ServiceName { get; private set; }
    public string BaseUrl { get; private set; }

    public RestCommunicatorServiceProvider(RestCommunicatorServiceOptions options, string serviceName, string baseUrl)
    {
        _options = options;
        ServiceName = serviceName;
        BaseUrl = baseUrl;
        _entitySignatures = new HashSet<CommunicatorEntitySignature>();
    }

    public void AddSignature(CommunicatorEntitySignature entitySignature)
    {
        _entitySignatures.Add(entitySignature);
    }

    public bool ContainsSignature(CommunicatorEntitySignature entitySignature)
    {
        return _entitySignatures.Contains(entitySignature);
    }

    public IEntityCommunicator<TEntity> GetEntityCommunicator<TEntity>(IServiceProvider services, string? endpoint)
        where TEntity : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);

        return new RestEntityCommunicator<TEntity>(httpClient, _options, endpoint);
    }

    public IEntityCommunicator<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, string? endpoint)
        where TEntity : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);

        return new RestEntityCommunicator<TEntity, TKey>(httpClient, _options, endpoint);
    }
}