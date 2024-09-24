namespace BitzArt.Flux;

public class SetConfigurationNotFoundException : Exception
{
    public SetConfigurationNotFoundException() : base("Requested Set Configuration was not found.")
    { }
}
