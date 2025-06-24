namespace BitzArt.Flux.Json;

internal class FluxJsonSetConfiguration<TModel, TKey>
    where TModel : class
    where TKey : notnull
{
    public FluxJsonServiceConfiguration ServiceConfiguration { get; private init; }

    public FluxJsonDataCollection<TModel, TKey> DataCollection { get; set; } = new FluxJsonDataCollection<TModel, TKey>();

    public FluxJsonSetConfiguration(FluxJsonServiceConfiguration serviceConfiguration)
    {
        ServiceConfiguration = serviceConfiguration;
    }
}
