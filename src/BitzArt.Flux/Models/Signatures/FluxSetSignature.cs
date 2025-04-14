namespace BitzArt.Flux.Sets;

internal readonly record struct FluxSetSignature
{
    public FluxServiceSignature ServiceSignature { get; private init; }

    public string? SetName { get; private init; }

    public FluxSetSignature(string? serviceName, string? setName) : this(new FluxServiceSignature(serviceName), setName) { }

    public FluxSetSignature(FluxServiceSignature serviceSignature, string? setName)
    {
        ServiceSignature = serviceSignature;
        SetName = setName;
    }
}