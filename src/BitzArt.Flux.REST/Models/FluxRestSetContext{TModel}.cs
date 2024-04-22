using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq.Expressions;
using System.Text.Json;
using System.Web;

namespace BitzArt.Flux;

internal class FluxRestSetContext<TModel>(
    HttpClient httpClient, 
    FluxRestServiceOptions serviceOptions, 
    ILogger logger, 
    FluxRestSetOptions<TModel> setOptions) 
    : IFluxSetContext<TModel>
    where TModel : class
{
    // ================ Flux internal wiring ================

    internal readonly HttpClient HttpClient = httpClient;
    internal readonly FluxRestServiceOptions ServiceOptions = serviceOptions;
    internal readonly ILogger _logger = logger;

    protected FluxRestSetOptions<TModel> _setOptions = setOptions;
    internal virtual FluxRestSetOptions<TModel> SetOptions
    {
        get => _setOptions;
        set => _setOptions = value;
    }

    // ============== IEnumerable implementation ==============

    public IEnumerator<TModel> GetEnumerator() => throw new EnumerationNotSupportedException();
    IEnumerator IEnumerable.GetEnumerator() => throw new EnumerationNotSupportedException();

    // ============== IQueryable implementation ==============

    public Type ElementType => typeof(TModel);
    public Expression Expression => Expression.Constant(this);
    public virtual IQueryProvider Provider => new FluxRestQueryProvider<TModel>(this);

    // ============== Data methods implementation ==============

    internal RequestUrlParameterParsingResult GetFullPath(string path, bool handleParameters, object[]? parameters = null)
    {
        if (ServiceOptions.BaseUrl is null) return new RequestUrlParameterParsingResult(path, string.Empty);

        var baseUrl = ServiceOptions.BaseUrl.TrimEnd('/');
        var localPath = path.TrimStart('/');
        var resultPath = $"{baseUrl}/{localPath}";

        if (handleParameters) return RequestParameterParsingUtility.ParseRequestUrl(_logger, resultPath, parameters);

        return new RequestUrlParameterParsingResult(resultPath, string.Empty);
    }

    internal async Task<TResult> HandleRequestAsync<TResult>(HttpRequestMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await HttpClient.SendAsync(message, cancellationToken);
            if (!response.IsSuccessStatusCode) throw new Exception($"External REST Service responded with http status code '{response.StatusCode}'.");
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<TResult>(content, ServiceOptions.SerializerOptions)!;

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error has occurred while processing http request. See inner exception for details.", ex);
        }
    }

    public virtual async Task<IEnumerable<TModel>> GetAllAsync(params object[]? parameters)
    {
        var path = SetOptions.Endpoint is not null ? SetOptions.Endpoint : string.Empty;
        var parse = GetFullPath(path, true, parameters);

        _logger.LogInformation("GetAll {type}: {route}{parsingLog}", typeof(TModel).Name, parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<IEnumerable<TModel>>(message);

        return result;
    }

    public virtual async Task<PageResult<TModel>> GetPageAsync(int offset, int limit, params object[]? parameters) => await GetPageAsync(new PageRequest(offset, limit), parameters);

    public virtual async Task<PageResult<TModel>> GetPageAsync(PageRequest pageRequest, params object[]? parameters)
    {
        var path = GetPageEndpoint();

        var queryIndex = path.IndexOf('?');

        var query = queryIndex == -1 ?
            HttpUtility.ParseQueryString(string.Empty) :
            HttpUtility.ParseQueryString(path[queryIndex..]);

        query.Add("offset", pageRequest.Offset?.ToString());
        query.Add("limit", pageRequest.Limit?.ToString());

        if (queryIndex != -1) path = path[..queryIndex];
        path = path + "?" + query.ToString();

        var parse = GetFullPath(path, true, parameters);
        _logger.LogInformation("GetPage {type}: {route}{parsingLog}", typeof(TModel).Name, parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<PageResult<TModel>>(message);

        return result;
    }

    public virtual async Task<TModel> GetAsync(object? id, params object[]? parameters)
    {
        var idEndpoint = GetIdEndpoint(id, parameters);
        var parse = GetFullPath(idEndpoint, false);
        _logger.LogInformation("Get {type}[{id}]: {route}{parsingLog}", typeof(TModel).Name, id is not null ? id.ToString() : "_", parse.Result, parse.Log);

        var message = new HttpRequestMessage(HttpMethod.Get, parse.Result);
        var result = await HandleRequestAsync<TModel>(message);

        return result;
    }

    public virtual async Task<TModel> AddAsync(TModel model, params object[]? parameters)
        => await AddAsync<TModel>(model, parameters);

    public virtual async Task<TResponse> AddAsync<TResponse>(TModel model, params object[]? parameters)
    {
        var path = GetEndpoint();
        var parse = GetFullPath(path, false);
        _logger.LogInformation("Add {type}: {route}", typeof(TModel).Name, path);

        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);
        var message = new HttpRequestMessage(HttpMethod.Post, parse.Result)
        {
            Content = new StringContent(jsonString)
        };

        var result = await HandleRequestAsync<TResponse>(message);

        return result;
    }

    public virtual async Task<TModel> UpdateAsync(object? id, TModel model, bool partial = false, params object[]? parameters)
        => await UpdateAsync<TModel>(id, model, partial, parameters);

    public virtual async Task<TResponse> UpdateAsync<TResponse>(object? id, TModel model, bool partial = false, params object[]? parameters)
    {
        var idEndpoint = GetIdEndpoint(id, parameters);
        var parse = GetFullPath(idEndpoint, false);

        _logger.LogInformation("Update {type}[{id}]: {route}", typeof(TModel).Name, id is not null ? id.ToString() : "_", parse.Result);

        var method = partial ? HttpMethod.Patch : HttpMethod.Put;
        var jsonString = JsonSerializer.Serialize(model, ServiceOptions.SerializerOptions);

        var message = new HttpRequestMessage(method, parse.Result)
        {
            Content = new StringContent(jsonString)
        };

        var result = await HandleRequestAsync<TResponse>(message);
        
        return result;
    }

    protected string GetPageEndpoint()
    {
        if (SetOptions.PageEndpoint is not null) return SetOptions.PageEndpoint;
        return GetEndpoint();
    }

    protected string GetEndpoint()
    {
        if (SetOptions.Endpoint is null) return string.Empty;
        return SetOptions.Endpoint;
    }

    protected string GetIdEndpoint(object? id, params object[]? parameters)
    {
        if (SetOptions.GetIdEndpointAction is null) throw new FluxRestKeyNotFoundException<TModel>();

        return SetOptions.GetIdEndpointAction(id, parameters);
    }

    public Task<TModel> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }
}
