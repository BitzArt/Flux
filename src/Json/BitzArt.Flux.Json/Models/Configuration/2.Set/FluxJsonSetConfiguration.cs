namespace BitzArt.Flux.Json;

internal class FluxJsonSetConfiguration
{
    public FluxJsonServiceConfiguration ServiceConfiguration { get; private init; }

    public FluxJsonSetConfiguration(FluxJsonServiceConfiguration serviceConfiguration)
    {
        ServiceConfiguration = serviceConfiguration;
    }
}
