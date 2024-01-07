namespace BitzArt.Flux;

internal class FluxServiceProviderNotFoundException : Exception
{
    public FluxServiceProviderNotFoundException()
        : base("Requested Flux Service Provider was not found.")
    { }
}
