using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxRestServiceFactory : IFluxServiceFactory
{
    private readonly FluxRestServiceOptions _serviceOptions;

    private readonly IDictionary<FluxEntitySignature, object> _entityOptions;

    public string ServiceName { get; private set; }

    public FluxRestServiceFactory(FluxRestServiceOptions options, string serviceName)
    {
        _serviceOptions = options;
        ServiceName = serviceName;

        _entityOptions = new Dictionary<FluxEntitySignature, object>();
    }

    public void AddEntity<TEntity>(object options)
        where TEntity : class
    {
        if (options is not FluxRestEntityOptions<TEntity> optionsCasted) throw new Exception("Wrong options type");

        var signature = new FluxEntitySignature(typeof(TEntity));

        if (_entityOptions.ContainsKey(signature)) throw new EntityAlreadyRegisteredException(nameof(TEntity));
        _entityOptions.Add(signature, optionsCasted);
    }

    public void AddEntity<TEntity, TKey>(object options)
        where TEntity : class
    {
        if (options is not FluxRestEntityOptions<TEntity, TKey> optionsCasted) throw new Exception("Wrong options type");

        var signatureFull = new FluxEntitySignature(typeof(TEntity), typeof(TKey));
        var signatureMinimal = new FluxEntitySignature(typeof(TEntity));

        if (_entityOptions.ContainsKey(signatureFull)) throw new EntityAlreadyRegisteredException(nameof(TEntity));
        _entityOptions.Add(signatureFull, optionsCasted);
        if (_entityOptions.ContainsKey(signatureMinimal)) throw new EntityAlreadyRegisteredException(nameof(TEntity));
        _entityOptions.Add(signatureMinimal, optionsCasted);
    }

    private FluxRestEntityOptions<TEntity> GetOptions<TEntity>()
        where TEntity : class
    {
        var signature = new FluxEntitySignature(typeof(TEntity));
        var found = _entityOptions.TryGetValue(signature, out var options);
        if (!found) throw new EntityConfigurationNotFoundException();
        return (FluxRestEntityOptions<TEntity>)options!;
    }

    private FluxRestEntityOptions<TEntity, TKey> GetOptions<TEntity, TKey>()
        where TEntity : class
    {
        var signature = new FluxEntitySignature(typeof(TEntity), typeof(TKey));
        var found = _entityOptions.TryGetValue(signature, out var options);
        if (!found) throw new EntityConfigurationNotFoundException();
        return (FluxRestEntityOptions<TEntity, TKey>)options!;
    }

    public bool ContainsSignature<TEntity>()
    {
        var signature = new FluxEntitySignature(typeof(TEntity));
        return _entityOptions.ContainsKey(signature);
    }

    public bool ContainsSignature<TEntity, TKey>()
    {
        var signature = new FluxEntitySignature(typeof(TEntity), typeof(TKey));
        return _entityOptions.ContainsKey(signature);
    }

    public IFluxEntityContext<TEntity> CreateEntityContext<TEntity>(IServiceProvider services)
        where TEntity : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TEntity>();

        return new FluxRestEntityContext<TEntity>(httpClient, _serviceOptions, logger, options);
    }

    public IFluxEntityContext<TEntity, TKey> CreateEntityContext<TEntity, TKey>(IServiceProvider services)
        where TEntity : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TEntity, TKey>();

        return new FluxRestEntityContext<TEntity, TKey>(httpClient, _serviceOptions, logger, options);
    }
}

file class EntityConfigurationNotFoundException : Exception
{
    public EntityConfigurationNotFoundException() : base("Requested Entity Configuration was not found.")
    { }
}

file class EntityAlreadyRegisteredException : Exception
{
    public EntityAlreadyRegisteredException(string name) : base($"Flux Entity {name} was already registered previously")
    { }
}