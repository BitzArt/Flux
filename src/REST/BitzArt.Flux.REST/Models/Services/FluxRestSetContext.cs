using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class FluxRestSetContext<TModel, TKey> : FluxSetContext<TModel, TKey, FluxRestSetConfiguration>
    where TModel : class
    where TKey : notnull
{
    private readonly HttpClient _httpClient;
    private readonly IFluxRestInterceptor? _interceptor;

    public FluxRestSetContext(FluxRestSetConfiguration configuration, IServiceProvider serviceProvider, ILogger logger, HttpClient httpClient)
        : base(configuration, serviceProvider, logger)
    {
        _httpClient = httpClient;

        _interceptor = serviceProvider.GetKeyedService<IFluxRestInterceptor>(Configuration.ServiceConfiguration.ServiceName);
    }

    public override async Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var httpRequestMessage = Configuration.Resolve(descriptor, ServiceProvider);

        var operationName = descriptor.GetFriendlyOperationName();
        Logger.LogInformation("[{type}] {operationName}: {uri}", typeof(TModel).Name, operationName, httpRequestMessage.RequestUri);

        if (_interceptor is not null)
        {
            await _interceptor.OnRequestAsync(_httpClient, httpRequestMessage, cancellationToken);
        }

        var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);

        if (_interceptor is not null)
        {
            await _interceptor.OnResponseAsync(_httpClient, response, cancellationToken);
        }

        if (!response.IsSuccessStatusCode)
        {
            var ex = new FluxRestOperationException($"REST service responded with a non-success status code: '{response.StatusCode}'.", response: response);
            Logger.LogError(ex, "REST service responded with a non-success status code: '{statusCode}'.", response.StatusCode);
            throw ex;
        }

        if (responseType is null)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var data = JsonSerializer.Deserialize(content, responseType, Configuration.ServiceConfiguration.JsonSerializerOptions);

        return data;
    }
}
