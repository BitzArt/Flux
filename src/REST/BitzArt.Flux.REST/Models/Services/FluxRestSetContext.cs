using BitzArt.Flux.Sets;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class FluxRestSetContext<TModel, TKey> : FluxSetContext<TModel, TKey, FluxRestSetConfiguration>
    where TModel : class
    where TKey : notnull
{
    private readonly HttpClient _httpClient;

    public FluxRestSetContext(FluxRestSetConfiguration configuration, IServiceProvider serviceProvider, ILogger logger, HttpClient httpClient)
        : base(configuration, serviceProvider, logger)
    {
        _httpClient = httpClient;
    }

    public override async Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var httpRequestMessage = Configuration.Resolve(descriptor, ServiceProvider);

        var operationName = descriptor.GetFriendlyOperationName();
        Logger.LogInformation("{operationName}: {uri}", operationName, httpRequestMessage.RequestUri);

        var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);

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
