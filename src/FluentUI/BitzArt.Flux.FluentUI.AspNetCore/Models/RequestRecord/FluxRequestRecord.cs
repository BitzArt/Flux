using BitzArt.Pagination;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

/// <summary>
/// Represents a record of a paginated Flux request. <br />
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class FluxPageRequestRecord<TModel> : FluxRequestRecord<FluxPageRequestInfo, PageResult<TModel>>
    where TModel : class
{
    /// <summary>
    /// Creates a new instance of <see cref="FluxPageRequestRecord{TModel}"/>.
    /// </summary>
    public FluxPageRequestRecord() : base() { }

    /// <summary>
    /// Creates a new instance of <see cref="FluxPageRequestRecord{TModel}"/>.
    /// </summary>
    /// <param name="request"></param>
    public FluxPageRequestRecord(FluxPageRequestInfo request) : base(request) { }

    /// <summary>
    /// If true, the request is exhausted. <br />
    /// The request being exhausted means that
    /// the next time it is being compared with another request,
    /// the comparison result should be false.
    /// </summary>
    [JsonPropertyName("isExhausted")]
    public bool? IsExhausted { get; set; } = null;
}

/// <summary>
/// Represents a record of a Flux request.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResult"></typeparam>
public class FluxRequestRecord<TRequest, TResult>
    where TRequest : FluxRequestInfo
    where TResult : class
{
    /// <summary>
    /// Request data.
    /// </summary>
    [JsonPropertyName("request")]
    public TRequest Request { get; set; } = null!;

    /// <summary>
    /// This request's result.
    /// </summary>
    [JsonPropertyName("result")]
    public TResult? Result { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="FluxRequestRecord{TRequest, TResult}"/>.
    /// </summary>
    public FluxRequestRecord() { }

    /// <summary>
    /// Creates a new instance of <see cref="FluxRequestRecord{TRequest, TResult}"/>.
    /// </summary>
    /// <param name="request"></param>
    public FluxRequestRecord(TRequest request)
    {
        Request = request;
    }
}
