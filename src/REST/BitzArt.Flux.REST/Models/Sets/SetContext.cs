using BitzArt.Flux.Sets;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class SetContext<TModel, TKey> : FluxSetContext<TModel, TKey, SetConfiguration>
    where TModel : class
    where TKey : notnull
{
    private readonly HttpClient _httpClient;

    public SetContext(SetConfiguration configuration, HttpClient httpClient) : base(configuration)
    {
        _httpClient = httpClient;
    }

    public override Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
        => ExecuteInternalAsync(descriptor, responseType, cancellationToken: cancellationToken);

    private async Task<object?> ExecuteInternalAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
    {
        var httpRequestMessage = Configuration.Endpoints.Resolve(descriptor);

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

        var data = JsonSerializer.Deserialize(content, responseType, Configuration.ServiceConfiguration.SerializerOptions);

        return data;
    }
}
