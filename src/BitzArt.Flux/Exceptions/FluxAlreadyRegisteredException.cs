namespace BitzArt.Flux;

internal class FluxAlreadyRegisteredException : InvalidOperationException
{
    private const string Msg = "Flux is already registered in this service collection. " +
        "In order to connect multiple external services, " +
        "register them by using the 'AddService' method multiple times when configuring Flux.";

    public FluxAlreadyRegisteredException() : base(Msg) { }
}
