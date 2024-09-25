namespace BitzArt.Flux.REST;

internal record RequestUrlParameterParsingResult
{
    public string Result { get; set; }
    public string Log { get; set; }

    public RequestUrlParameterParsingResult(string result, string log)
    {
        Result = result;
        Log = log;
    }
}
