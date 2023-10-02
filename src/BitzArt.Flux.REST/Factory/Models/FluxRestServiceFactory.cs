using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxRestServiceFactory : IFluxServiceFactory
{
    private readonly FluxRestServiceOptions _serviceOptions;

    private readonly IDictionary<FluxSetSignature, object> _setOptions;

    public string ServiceName { get; private set; }

    public FluxRestServiceFactory(FluxRestServiceOptions options, string serviceName)
    {
        _serviceOptions = options;
        ServiceName = serviceName;

        _setOptions = new Dictionary<FluxSetSignature, object>();
    }

    public void AddSet<TModel>(object options)
        where TModel : class
    {
        if (options is not FluxRestSetOptions<TModel> optionsCasted) throw new Exception("Wrong options type");

        var signature = new FluxSetSignature(typeof(TModel));

        if (_setOptions.ContainsKey(signature)) throw new SetAlreadyRegisteredException(nameof(TModel));
        _setOptions.Add(signature, optionsCasted);
    }

    public void AddSet<TModel, TKey>(object options)
        where TModel : class
    {
        if (options is not FluxRestSetOptions<TModel, TKey> optionsCasted) throw new Exception("Wrong options type");

        var signatureFull = new FluxSetSignature(typeof(TModel), typeof(TKey));
        var signatureMinimal = new FluxSetSignature(typeof(TModel));

        if (_setOptions.ContainsKey(signatureFull)) throw new SetAlreadyRegisteredException(nameof(TModel));
        _setOptions.Add(signatureFull, optionsCasted);
        if (_setOptions.ContainsKey(signatureMinimal)) throw new SetAlreadyRegisteredException(nameof(TModel));
        _setOptions.Add(signatureMinimal, optionsCasted);
    }

    private FluxRestSetOptions<TModel> GetOptions<TModel>()
        where TModel : class
    {
        var signature = new FluxSetSignature(typeof(TModel));
        var found = _setOptions.TryGetValue(signature, out var options);
        if (!found) throw new SetConfigurationNotFoundException();
        return (FluxRestSetOptions<TModel>)options!;
    }

    private FluxRestSetOptions<TModel, TKey> GetOptions<TModel, TKey>()
        where TModel : class
    {
        var signature = new FluxSetSignature(typeof(TModel), typeof(TKey));
        var found = _setOptions.TryGetValue(signature, out var options);
        if (!found) throw new SetConfigurationNotFoundException();
        return (FluxRestSetOptions<TModel, TKey>)options!;
    }

    public bool ContainsSignature<TModel>()
    {
        var signature = new FluxSetSignature(typeof(TModel));
        return _setOptions.ContainsKey(signature);
    }

    public bool ContainsSignature<TModel, TKey>()
    {
        var signature = new FluxSetSignature(typeof(TModel), typeof(TKey));
        return _setOptions.ContainsKey(signature);
    }

    public IFluxSetContext<TModel> CreateSetContext<TModel>(IServiceProvider services)
        where TModel : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TModel>();

        return new FluxRestSetContext<TModel>(httpClient, _serviceOptions, logger, options);
    }

    public IFluxSetContext<TModel, TKey> CreateSetContext<TModel, TKey>(IServiceProvider services)
        where TModel : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TModel, TKey>();

        return new FluxRestSetContext<TModel, TKey>(httpClient, _serviceOptions, logger, options);
    }
}

file class SetConfigurationNotFoundException : Exception
{
    public SetConfigurationNotFoundException() : base("Requested Set Configuration was not found.")
    { }
}

file class SetAlreadyRegisteredException : Exception
{
    public SetAlreadyRegisteredException(string name) : base($"Flux Set {name} was already registered previously")
    { }
}