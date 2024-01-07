namespace BitzArt.Flux;

internal class SetConfigurationNotFoundException : Exception
{
    public SetConfigurationNotFoundException() : base("Requested Set Configuration was not found.")
    { }
}
