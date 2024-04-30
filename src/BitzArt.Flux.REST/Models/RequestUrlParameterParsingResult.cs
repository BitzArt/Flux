namespace BitzArt.Flux;

internal record RequestUrlParameterParsingResult
{
    public required string Result { get; set; }
    public required string Log { get; set; }

    public RequestUrlParameterParsingResult(string result, string log)
    {
        Result = result;
        Log = log;
    }
}
