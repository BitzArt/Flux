namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when a Flux Service Provider is not found.
/// </summary>
public class FluxServiceProviderNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluxServiceProviderNotFoundException"/> class.
    /// </summary>
    public FluxServiceProviderNotFoundException()
        : base("Requested Flux Service Provider was not found.")
    { }
}
