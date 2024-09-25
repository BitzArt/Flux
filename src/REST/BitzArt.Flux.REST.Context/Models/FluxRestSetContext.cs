using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Web;

namespace BitzArt.Flux.REST;

internal class FluxRestSetContext<TModel, TKey>(
    HttpClient httpClient,
    FluxRestServiceOptions serviceOptions,
    ILogger logger,
    IFluxRestSetOptions<TModel> setOptions
    ) : FluxSetContext<TModel, TKey>
    where TModel : class
{
    // ================ Flux internal wiring ================

    internal readonly HttpClient HttpClient = httpClient;
    internal readonly FluxRestServiceOptions ServiceOptions = serviceOptions;
    private readonly ILogger _logger = logger;

    internal IFluxRestSetOptions<TModel> SetOptions { get; set; } = setOptions;

    // ============== Data methods implementation ==============

    // ============== GET ALL ==============

    public override async Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters)
    {
        var path = GetEndpointFullPath(parameters);

        _logger.LogInformation("GetAll {type}: {route}{parsingLog}", typeof(TModel).Name, path.Result, path.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, path.Result);
        var result = await HandleRequestAsync<IEnumerable<TModel>>(message);

        return result;
    }

    // ============== GET (Single) ==============

    public override async Task<TModel> GetAsync(TKey? id, params object[]? parameters)
    {
        var parse = GetIdEndpointFullPath(id, parameters);

        _logger.LogInformation("Get {type}[{id}]: {route}{parsingLog}", typeof(TModel).Name, id!.ToString(), parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<TModel>(message);

        return result;
    }

    // ============== GET PAGE ==============

    public override async Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        var path = GetPageEndpoint();
        var parse = GetFullPath(path, true, parameters);
        path = parse.Result;

        var queryIndex = path.IndexOf('?');

        var query = queryIndex == -1 ?
            HttpUtility.ParseQueryString(string.Empty) :
            HttpUtility.ParseQueryString(path[queryIndex..]);

        query.Add("offset", pageRequest.Offset?.ToString());
        query.Add("limit", pageRequest.Limit?.ToString());

        if (queryIndex != -1) path = path[..queryIndex];
        path = path + "?" + query.ToString();

        parse.Result = path;

        _logger.LogInformation("GetPage {type}: {route}{parsingLog}", typeof(TModel).Name, parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<PageResult<TModel>>(message);

        return result;
    }

    // ============== ADD ==============

    public override async Task<TResponse> AddAsync<TResponse>(TModel model, params object[]? parameters)
    {
        var parse = GetEndpointFullPath(parameters);
        _logger.LogInformation("Add {type}: {route}", typeof(TModel).Name, parse.Result);

        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);
        var message = new HttpRequestMessage(HttpMethod.Post, parse.Result)
        {
            Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
        };

        var result = await HandleRequestAsync<TResponse>(message);

        return result;
    }

    // ============== UPDATE ==============

    public override Task<TResponse> UpdateAsync<TResponse>(TModel model, bool partial = false, params object[]? parameters)
        => UpdateAsync<TResponse>(id: default, model, partial, parameters);

    public override async Task<TResponse> UpdateAsync<TResponse>(TKey? id, TModel model, bool partial = false, params object[]? parameters)
    {
        var path = GetIdEndpointFullPath(id, parameters);
        _logger.LogInformation("Update {type}[{id}]: {route}", typeof(TModel).Name, id is not null ? id.ToString() : "_", path.Result);

        var method = partial ? HttpMethod.Patch : HttpMethod.Put;
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);

        var message = new HttpRequestMessage(method, path.Result)
        {
            Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
        };

        var result = await HandleRequestAsync<TResponse>(message);

        return result;
    }

    // ============== Request Handling ==============

    private RequestUrlParameterParsingResult GetEndpointFullPath(params object[]? parameters)
    {
        var endpoint = GetEndpoint();
        return GetFullPath(endpoint, true, parameters);
    }

    private string GetEndpoint()
    {
        if (SetOptions.EndpointOptions.Path is null) return string.Empty;
        return SetOptions.EndpointOptions.Path;
    }

    private RequestUrlParameterParsingResult GetIdEndpointFullPath(TKey? id, params object[]? parameters)
    {
        if (SetOptions.IdEndpointOptions.GetPathFunc is not null)
        {
            if (id is null)
            {
                var pathFunc = SetOptions.IdEndpointOptions.GetPathFunc!;
                return GetFullPath(pathFunc(id, parameters), false, parameters);
            }

            if (id is not TKey idCasted) throw new ArgumentException($"Id must be of type {typeof(TKey).Name}");

            var idEndpoint = SetOptions.IdEndpointOptions.GetPathFunc(idCasted, parameters);
            return GetFullPath(idEndpoint, false, parameters);
        }
        else
        {
            var idEndpoint = SetOptions.EndpointOptions.Path is not null ? Path.Combine(SetOptions.EndpointOptions.Path, id!.ToString()!) : id!.ToString()!;
            return GetFullPath(idEndpoint, true, parameters);
        }
    }

    private RequestUrlParameterParsingResult GetFullPath(string path, bool handleParameters, object[]? parameters = null)
    {
        if (ServiceOptions.BaseUrl is null) return new RequestUrlParameterParsingResult(path, string.Empty);

        var baseUrl = ServiceOptions.BaseUrl.TrimEnd('/');
        var localPath = path.TrimStart('/');
        var resultPath = $"{baseUrl}/{localPath}";

        if (handleParameters) return RequestParameterParsingUtility.ParseRequestUrl(_logger, resultPath, parameters);

        return new RequestUrlParameterParsingResult(resultPath, string.Empty);
    }

    private string GetPageEndpoint()
    {
        if (SetOptions.PageEndpointOptions.Path is not null) return SetOptions.PageEndpointOptions.Path;
        return GetEndpoint();
    }

    private string GetIdEndpoint(object? id, params object[]? parameters)
    {
        if (SetOptions.IdEndpointOptions.GetPathFunc is null) throw new FluxRestKeyNotFoundException<TModel>();

        return SetOptions.IdEndpointOptions.GetPathFunc(id, parameters);
    }

    private async Task<TResult> HandleRequestAsync<TResult>(HttpRequestMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await HttpClient.SendAsync(message, cancellationToken);
            if (!response.IsSuccessStatusCode) throw new FluxRestNonSuccessStatusCodeException(response);
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
}
