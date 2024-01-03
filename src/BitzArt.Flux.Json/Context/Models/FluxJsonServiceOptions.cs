namespace BitzArt.Flux;

public class FluxJsonServiceOptions
{
    public string? BaseUrl { get; set; }

    public FluxJsonServiceOptions(string? baseUrl)
    {
        BaseUrl = baseUrl;
    }
}