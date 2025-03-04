using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class FluxRestSetContext<TModel, TKey>(
    HttpClient httpClient,
    FluxRestServiceOptions serviceOptions,
    ILogger logger,
    IFluxRestSetOptions<TModel> setOptions)
    : FluxSetContext<TModel, TKey>
     where TModel : class
{
    // ============================== General ==============================

    internal IFluxRestSetOptions<TModel> SetOptions { get; set; } = setOptions;

    internal readonly FluxRestServiceOptions ServiceOptions = serviceOptions;

    internal readonly HttpClient HttpClient = httpClient;

    private readonly ILogger _logger = logger;

    private async Task<TResult> HandleRequestAsync<TResult>(HttpRequestMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await HttpClient.SendAsync(message, cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new FluxRestNonSuccessStatusCodeException(response);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<TResult>(content, ServiceOptions.SerializerOptions)!;

            return result;
        }
        catch (FluxRestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new FluxRestException("An error has occurred while processing http request. See inner exception for details.", ex);
        }
    }

    // ============================== GetAsync ==============================

    public override async Task<TModel> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var preparationParameters = new RequestPreparationParameters<RestRequestParameters, TKey>(EndpointType.Id, id, (path) =>
            new HttpRequestMessage(HttpMethod.Get, path));

        var requestMessage = SetOptions.EndpointCollection.Resolve<RestRequestParameters>(preparationParameters);

        return await HandleRequestAsync<TModel>(requestMessage, cancellationToken);
    }

    public override async Task<TModel> GetAsync<TInputParameters>(TKey id, TInputParameters parameters, CancellationToken cancellationToken = default)
    {
        var preparationParameters = new RequestPreparationParameters<TInputParameters, TKey>(EndpointType.Id, id, parameters, (path) =>
            new HttpRequestMessage(HttpMethod.Get, path));

        var requestMessage = SetOptions.EndpointCollection.Resolve<TInputParameters>(preparationParameters);

        return await HandleRequestAsync<TModel>(requestMessage, cancellationToken);
    }

    // ============================== GetAllAsync ==============================

    public override async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var preparationParameters = new RequestPreparationParameters<RestRequestParameters, TKey>(EndpointType.Default, (path) =>
            new HttpRequestMessage(HttpMethod.Get, path));

        var requestMessage = SetOptions.EndpointCollection.Resolve<RestRequestParameters>(preparationParameters);

        return await HandleRequestAsync<IEnumerable<TModel>>(requestMessage, cancellationToken);
    }

    public override async Task<IEnumerable<TModel>> GetAllAsync<TInputParameters>(TInputParameters parameters, CancellationToken cancellationToken = default)
    {
        var preparationParameters = new RequestPreparationParameters<TInputParameters, TKey>(EndpointType.Default, parameters, (path) =>
            new HttpRequestMessage(HttpMethod.Get, path));

        var requestMessage = SetOptions.EndpointCollection.Resolve<TInputParameters>(preparationParameters);

        return await HandleRequestAsync<IEnumerable<TModel>>(requestMessage, cancellationToken);
    }

    // ============================== GetPageAsync ==============================

    public override async Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, CancellationToken cancellationToken = default)
    {
        var preparationParameters = new RequestPreparationParameters<RestRequestParameters, TKey>(EndpointType.Page, pageRequest, null, (path) =>
            new HttpRequestMessage(HttpMethod.Get, path));

        var requestMessage = SetOptions.EndpointCollection.Resolve<RestRequestParameters>(preparationParameters);

        return await HandleRequestAsync<PageResult<TModel>>(requestMessage, cancellationToken);
    }

    public override async Task<PageResult<TModel>> GetPageAsync<TInputParameters>(PageRequest pageRequest, TInputParameters parameters, CancellationToken cancellationToken = default)
    {
        var preparationParameters = new RequestPreparationParameters<TInputParameters, TKey>(EndpointType.Page, pageRequest, parameters, (path) =>
             new HttpRequestMessage(HttpMethod.Get, path));

        var requestMessage = SetOptions.EndpointCollection.Resolve<TInputParameters>(preparationParameters);

        return await HandleRequestAsync<PageResult<TModel>>(requestMessage, cancellationToken);
    }

    // ============================== AddAsync ==============================

    public override async Task<TResponse> AddAsync<TResponse>(TModel model, CancellationToken cancellationToken = default)
    {
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);

        var preparationParameters = new RequestPreparationParameters<RestRequestParameters, TKey>(EndpointType.Default, (path) =>
            new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json)
            });

        var requestMessage = SetOptions.EndpointCollection.Resolve<RestRequestParameters>(preparationParameters);

        return await HandleRequestAsync<TResponse>(requestMessage, cancellationToken);
    }

    public override async Task<TResponse> AddAsync<TInputParameters, TResponse>(TModel model, TInputParameters parameters, CancellationToken cancellationToken = default)
    {
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);

        var preparationParameters = new RequestPreparationParameters<TInputParameters, TKey>(EndpointType.Default, parameters, (path) =>
            new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json)
            });

        var requestMessage = SetOptions.EndpointCollection.Resolve<TInputParameters>(preparationParameters);

        return await HandleRequestAsync<TResponse>(requestMessage, cancellationToken);
    }

    // ============================== UpdateAsync ==============================

    public override async Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, CancellationToken cancellationToken = default)
    {
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);
        var method = partial ? HttpMethod.Patch : HttpMethod.Put;

        var preparationParameters = new RequestPreparationParameters<RestRequestParameters, TKey>(EndpointType.Id, (path) =>
            new HttpRequestMessage(method, path)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json)
            });

        var requestMessage = SetOptions.EndpointCollection.Resolve<RestRequestParameters>(preparationParameters);

        return await HandleRequestAsync<TResponse>(requestMessage, cancellationToken);
    }

    public override async Task<TResponse> UpdateAsync<TResponse>(TKey id, TModel model, bool partial = false, CancellationToken cancellationToken = default)
    {
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);
        var method = partial ? HttpMethod.Patch : HttpMethod.Put;

        var preparationParameters = new RequestPreparationParameters<RestRequestParameters, TKey>(EndpointType.Id, id, (path) =>
            new HttpRequestMessage(method, path)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json)
            });

        var requestMessage = SetOptions.EndpointCollection.Resolve<RestRequestParameters>(preparationParameters);

        return await HandleRequestAsync<TResponse>(requestMessage, cancellationToken);
    }

    public override async Task<TResponse> UpdateAsync<TInputParameters, TResponse>(TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
    {
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);
        var method = partial ? HttpMethod.Patch : HttpMethod.Put;

        var preparationParameters = new RequestPreparationParameters<TInputParameters, TKey>(EndpointType.Id, parameters, (path) =>
            new HttpRequestMessage(method, path)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json)
            });

        var requestMessage = SetOptions.EndpointCollection.Resolve<RestRequestParameters>(preparationParameters);

        return await HandleRequestAsync<TResponse>(requestMessage, cancellationToken);
    }

    public override async Task<TResponse> UpdateAsync<TInputParameters, TResponse>(TKey id, TModel model, TInputParameters parameters, bool partial = false, CancellationToken cancellationToken = default)
    {
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);
        var method = partial ? HttpMethod.Patch : HttpMethod.Put;

        var preparationParameters = new RequestPreparationParameters<TInputParameters, TKey>(EndpointType.Id, id, parameters, (path) =>
            new HttpRequestMessage(method, path)
            {
                Content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json)
            });

        var requestMessage = SetOptions.EndpointCollection.Resolve<TInputParameters>(preparationParameters);

        return await HandleRequestAsync<TResponse>(requestMessage, cancellationToken);
    }
}
