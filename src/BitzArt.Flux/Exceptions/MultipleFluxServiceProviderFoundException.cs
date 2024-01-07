namespace BitzArt.Flux;

internal class MultipleFluxServiceProviderFoundException : Exception
{
    public MultipleFluxServiceProviderFoundException()
        : base("Multiple matching Flux Service Providers were found.")
    { }
}