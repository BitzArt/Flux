namespace BitzArt.Flux.Json;

internal class FluxJsonSetConfiguration<TModel>
    where TModel : class
{
    public FluxJsonServiceConfiguration ServiceConfiguration { get; private init; }

    private FluxJsonDataCollection<TModel>? _dataCollection;
    public FluxJsonDataCollection<TModel> DataCollection
    {
        get => _dataCollection ?? throw new InvalidOperationException("Set data collection is not initialized. " +
            "Please ensure that the set is properly configured before attempting to access the data collection " +
            "by using the 'FromJson' or 'FromJsonFile' extension methods.");

        set => _dataCollection = value;
    }

    public FluxJsonSetConfiguration(FluxJsonServiceConfiguration serviceConfiguration)
    {
        ServiceConfiguration = serviceConfiguration;
    }
}
