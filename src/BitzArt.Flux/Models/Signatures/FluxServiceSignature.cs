namespace BitzArt.Flux.Sets;

internal readonly record struct FluxServiceSignature
{
    public string? ServiceName { get; private init; }

    public FluxServiceSignature(string? serviceName)
    {
        ServiceName = serviceName;
    }
}
