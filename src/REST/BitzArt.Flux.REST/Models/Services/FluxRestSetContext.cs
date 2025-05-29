using BitzArt.Flux.Sets;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class FluxRestSetContext<TModel, TKey> : FluxSetContext<TModel, TKey, FluxRestSetConfiguration>
    where TModel : class
    where TKey : notnull
{
    private readonly HttpClient _httpClient;

    public FluxRestSetContext(FluxRestSetConfiguration configuration, IServiceProvider serviceProvider, HttpClient httpClient) : base(configuration, serviceProvider)
    {
        _httpClient = httpClient;
    }

    public override async Task<object?> ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var httpRequestMessage = Configuration.Resolve(descriptor, ServiceProvider);

        var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new FluxRestOperationException($"REST service responded with a non-success status code: '{response.StatusCode}'.", response: response);
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
