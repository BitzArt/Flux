using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxRestServiceProvider : IFluxServiceProvider
{
    private readonly ICollection<FluxEntitySignature> _entitySignatures;
    private readonly FluxRestServiceOptions _serviceOptions;

    public string ServiceName { get; private set; }

    public FluxRestServiceProvider(FluxRestServiceOptions options, string serviceName)
    {
        _serviceOptions = options;
        ServiceName = serviceName;
        _entitySignatures = new HashSet<FluxEntitySignature>();
    }

    public void AddSignature(FluxEntitySignature entitySignature)
    {
        _entitySignatures.Add(entitySignature);
    }

    public bool ContainsSignature(FluxEntitySignature entitySignature)
    {
        return _entitySignatures.Contains(entitySignature);
    }

    public IFluxEntityContext<TEntity> GetEntityContext<TEntity>(IServiceProvider services, object? options)
        where TEntity : class
    {
        if (options is not FluxRestEntityOptions<TEntity> optionsCasted) throw new Exception("Wrong options type");

        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        return new FluxRestEntityContext<TEntity>(httpClient, _serviceOptions, logger, optionsCasted);
    }

    public IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(IServiceProvider services, object? options)
        where TEntity : class
    {
        if (options is not FluxRestEntityOptions<TEntity, TKey> optionsCasted) throw new Exception("Wrong options type");

        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        return new FluxRestEntityContext<TEntity, TKey>(httpClient, _serviceOptions, logger, optionsCasted);
    }
}