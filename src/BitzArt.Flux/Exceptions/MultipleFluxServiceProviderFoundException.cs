namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when multiple Flux Service Providers are found.
/// </summary>
public class MultipleFluxServiceProviderFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MultipleFluxServiceProviderFoundException"/> class.
    /// </summary>
    public MultipleFluxServiceProviderFoundException()
        : base("Multiple matching Flux Service Providers were found.")
    { }
}