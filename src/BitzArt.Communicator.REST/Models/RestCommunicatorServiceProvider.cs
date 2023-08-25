using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace BitzArt.Communicator;

public class RestCommunicatorServiceProvider : ICommunicatorServiceProvider
{
    private ICollection<CommunicatorEntitySignature> _entitySignatures;

    public string ServiceName { get; private set; }
    public string BaseUrl { get; private set; }

    public RestCommunicatorServiceProvider(string serviceName, string baseUrl)
    {
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

    public IEntityCommunicator<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, string endpoint)
        where TEntity : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);

        return new RestEntityCommunicator<TEntity, TKey>(httpClient, endpoint);
    }
}