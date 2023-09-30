using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitzArt.Flux;

internal class FluxRestServiceFactory : IFluxServiceFactory
{
    private readonly FluxRestServiceOptions _serviceOptions;

    private readonly IDictionary<FluxModelSignature, object> _modelOptions;

    public string ServiceName { get; private set; }

    public FluxRestServiceFactory(FluxRestServiceOptions options, string serviceName)
    {
        _serviceOptions = options;
        ServiceName = serviceName;

        _modelOptions = new Dictionary<FluxModelSignature, object>();
    }

    public void AddModel<TModel>(object options)
        where TModel : class
    {
        if (options is not FluxRestModelOptions<TModel> optionsCasted) throw new Exception("Wrong options type");

        var signature = new FluxModelSignature(typeof(TModel));

        if (_modelOptions.ContainsKey(signature)) throw new ModelAlreadyRegisteredException(nameof(TModel));
        _modelOptions.Add(signature, optionsCasted);
    }

    public void AddModel<TModel, TKey>(object options)
        where TModel : class
    {
        if (options is not FluxRestModelOptions<TModel, TKey> optionsCasted) throw new Exception("Wrong options type");

        var signatureFull = new FluxModelSignature(typeof(TModel), typeof(TKey));
        var signatureMinimal = new FluxModelSignature(typeof(TModel));

        if (_modelOptions.ContainsKey(signatureFull)) throw new ModelAlreadyRegisteredException(nameof(TModel));
        _modelOptions.Add(signatureFull, optionsCasted);
        if (_modelOptions.ContainsKey(signatureMinimal)) throw new ModelAlreadyRegisteredException(nameof(TModel));
        _modelOptions.Add(signatureMinimal, optionsCasted);
    }

    private FluxRestModelOptions<TModel> GetOptions<TModel>()
        where TModel : class
    {
        var signature = new FluxModelSignature(typeof(TModel));
        var found = _modelOptions.TryGetValue(signature, out var options);
        if (!found) throw new ModelConfigurationNotFoundException();
        return (FluxRestModelOptions<TModel>)options!;
    }

    private FluxRestModelOptions<TModel, TKey> GetOptions<TModel, TKey>()
        where TModel : class
    {
        var signature = new FluxModelSignature(typeof(TModel), typeof(TKey));
        var found = _modelOptions.TryGetValue(signature, out var options);
        if (!found) throw new ModelConfigurationNotFoundException();
        return (FluxRestModelOptions<TModel, TKey>)options!;
    }

    public bool ContainsSignature<TModel>()
    {
        var signature = new FluxModelSignature(typeof(TModel));
        return _modelOptions.ContainsKey(signature);
    }

    public bool ContainsSignature<TModel, TKey>()
    {
        var signature = new FluxModelSignature(typeof(TModel), typeof(TKey));
        return _modelOptions.ContainsKey(signature);
    }

    public IFluxModelContext<TModel> CreateModelContext<TModel>(IServiceProvider services)
        where TModel : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TModel>();

        return new FluxRestModelContext<TModel>(httpClient, _serviceOptions, logger, options);
    }

    public IFluxModelContext<TModel, TKey> CreateModelContext<TModel, TKey>(IServiceProvider services)
        where TModel : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TModel, TKey>();

        return new FluxRestModelContext<TModel, TKey>(httpClient, _serviceOptions, logger, options);
    }
}

file class ModelConfigurationNotFoundException : Exception
{
    public ModelConfigurationNotFoundException() : base("Requested Model Configuration was not found.")
    { }
}

file class ModelAlreadyRegisteredException : Exception
{
    public ModelAlreadyRegisteredException(string name) : base($"Flux Model {name} was already registered previously")
    { }
}