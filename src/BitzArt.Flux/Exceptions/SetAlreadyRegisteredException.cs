namespace BitzArt.Flux;

internal class SetAlreadyRegisteredException : Exception
{
    public SetAlreadyRegisteredException(string name) : base($"An unnamed Flux Set for a model '{name}' was already registered previously. Consider giving specific names to different sets for this model.")
    { }
}