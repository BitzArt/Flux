using BitzArt.Flux.REST;
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

    public void AddSet<TModel>(object options, string? name)
        where TModel : class
    {
        if (options is not IFluxRestSetOptions<TModel> optionsCasted) throw new Exception("Wrong options type");

        var signature = new FluxSetSignature(typeof(TModel), Name: name);

        if (_setOptions.ContainsKey(signature)) throw new SetAlreadyRegisteredException(nameof(TModel));
        _setOptions.Add(signature, optionsCasted);

        if (name is not null)
        {
            var signatureNoName = new FluxSetSignature(typeof(TModel));
            ProcessUnnamedSignatureForNamedSet(signatureNoName, optionsCasted);
        }
    }

    public void AddSet<TModel, TKey>(object options, string? name)
        where TModel : class
    {
        if (options is not FluxRestSetOptions<TModel, TKey> optionsCasted) throw new Exception("Wrong options type");

        var signatureFull = new FluxSetSignature(typeof(TModel), typeof(TKey), Name: name);
        var signatureMinimal = new FluxSetSignature(typeof(TModel), Name: name);

        if (_setOptions.ContainsKey(signatureFull)) throw new SetAlreadyRegisteredException(nameof(TModel));
        _setOptions.Add(signatureFull, optionsCasted);
        if (_setOptions.ContainsKey(signatureMinimal)) throw new SetAlreadyRegisteredException(nameof(TModel));
        _setOptions.Add(signatureMinimal, optionsCasted);

        if (name is not null)
        {
            var signatureFullNoName = new FluxSetSignature(typeof(TModel), typeof(TKey));
            ProcessUnnamedSignatureForNamedSet(signatureFullNoName, optionsCasted);

            var signatureMinimalNoName = new FluxSetSignature(typeof(TModel));
            ProcessUnnamedSignatureForNamedSet(signatureMinimalNoName, optionsCasted);
        }
    }

    private void ProcessUnnamedSignatureForNamedSet(FluxSetSignature signature, object options)
    {
        // If such unnamed signature is already present, remove it,
        // and don't add the current one,
        // making it impossible to access this model's Sets
        // without explicitly specifying the Set's name.
        if (_setOptions.ContainsKey(signature)) _setOptions.Remove(signature);

        // If such unnamed signature is not already present, add the current one.
        else _setOptions.Add(signature, options);
    }

    private IFluxRestSetOptions<TModel> GetOptions<TModel>(string? name = null)
        where TModel : class
    {
        var signature = new FluxSetSignature(typeof(TModel), Name: name);
        var found = _setOptions.TryGetValue(signature, out var options);
        if (!found) throw new SetConfigurationNotFoundException();
        return (IFluxRestSetOptions<TModel>)options!;
    }

    private FluxRestSetOptions<TModel, TKey> GetOptions<TModel, TKey>(string? name = null)
        where TModel : class
    {
        var signature = new FluxSetSignature(typeof(TModel), typeof(TKey), Name: name);
        var found = _setOptions.TryGetValue(signature, out var options);
        if (!found) throw new SetConfigurationNotFoundException();
        return (FluxRestSetOptions<TModel, TKey>)options!;
    }

    public bool ContainsSignature<TModel>(string? setName)
    {
        var signature = new FluxSetSignature(typeof(TModel), Name: setName);
        return _setOptions.ContainsKey(signature);
    }

    public bool ContainsSignature<TModel, TKey>(string? setName)
    {
        var signature = new FluxSetSignature(typeof(TModel), typeof(TKey), Name: setName);
        return _setOptions.ContainsKey(signature);
    }

    public IFluxSetContext<TModel> CreateSetContext<TModel>(IServiceProvider services, string? name)
        where TModel : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TModel>(name);

        return new FluxRestSetContext<TModel, object>(httpClient, _serviceOptions, logger, options);
    }

    public IFluxSetContext<TModel, TKey> CreateSetContext<TModel, TKey>(IServiceProvider services, string? name)
        where TModel : class
    {
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(ServiceName);
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Flux");

        var options = GetOptions<TModel, TKey>(name);

        return new FluxRestSetContext<TModel, TKey>(httpClient, _serviceOptions, logger, options);
    }
}
