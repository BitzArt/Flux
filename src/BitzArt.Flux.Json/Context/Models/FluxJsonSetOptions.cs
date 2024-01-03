namespace BitzArt.Flux;

public class FluxJsonSetOptions<TModel>
    where TModel : class
{
    public string? FilePath { get; set; }
}

public class FluxJsonSetOptions<TModel, TKey> : FluxJsonSetOptions<TModel>
    where TModel : class
{
}