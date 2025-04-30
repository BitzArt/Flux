namespace BitzArt.Flux.Sets;

internal readonly record struct FluxSetSignature
{
    public FluxServiceSignature ServiceSignature { get; private init; }

    public object? SetKey { get; private init; }

    public FluxSetSignature(string? serviceName, object? setKey) : this(new FluxServiceSignature(serviceName), setKey) { }

    public FluxSetSignature(FluxServiceSignature serviceSignature, object? setKey)
    {
        ServiceSignature = serviceSignature;
        SetKey = setKey;
    }
}