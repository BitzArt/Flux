using BitzArt.Pagination;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

public class FluxPageRequestRecord<TModel> : FluxRequestRecord<FluxPageRequestInfo, PageResult<TModel>>
    where TModel : class
{
    public FluxPageRequestRecord() : base() { }

    public FluxPageRequestRecord(FluxPageRequestInfo request) : base(request) { }

    [JsonPropertyName("isExhausted")]
    public bool? IsExhausted { get; set; } = null;
}

public class FluxRequestRecord<TRequest, TResult>
    where TRequest : FluxRequestInfo
    where TResult : class
{
    [JsonPropertyName("request")]
    public TRequest Request { get; set; } = null!;

    [JsonPropertyName("result")]
    public TResult? Result { get; set; }

    public FluxRequestRecord() { }

    public FluxRequestRecord(TRequest request)
    {
        Request = request;
    }
}
