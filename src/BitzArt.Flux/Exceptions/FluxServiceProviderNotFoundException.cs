namespace BitzArt.Flux;

public class FluxServiceProviderNotFoundException : Exception
{
    public FluxServiceProviderNotFoundException()
        : base("Requested Flux Service Provider was not found.")
    { }
}
