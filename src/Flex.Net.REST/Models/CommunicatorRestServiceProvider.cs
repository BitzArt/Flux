using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Flex;

internal class CommunicatorRestServiceProvider : ICommunicatorServiceProvider
{
    private readonly ICollection<CommunicatorEntitySignature> _entitySignatures;
    private readonly CommunicatorRestServiceOptions _serviceOptions;

    public string ServiceName { get; private set; }

    public CommunicatorRestServiceProvider(CommunicatorRestServiceOptions options, string serviceName)
    {
        _serviceOptions = options;
        ServiceName = serviceName;
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

    public ICommunicationContext<TEntity> GetEntityCommunicator<TEntity>(IServiceProvider services, object? options)
        where TEntity : class
    {
        if (options is not CommunicatorRestEntityOptions<TEntity> optionsCasted) throw new Exception("Wrong options type");

        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Communicator");

        return new CommunicatorRestEntityContext<TEntity>(httpClient, _serviceOptions, logger, optionsCasted);
    }

    public ICommunicationContext<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, object? options)
        where TEntity : class
    {
        if (options is not CommunicatorRestEntityOptions<TEntity, TKey> optionsCasted) throw new Exception("Wrong options type");

        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Communicator");

        return new CommunicatorRestEntityContext<TEntity, TKey>(httpClient, _serviceOptions, logger, optionsCasted);
    }
}