namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when a Flux Set is already registered,
/// and a new one is being registered with the same name.
/// </summary>
public class SetAlreadyRegisteredException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SetAlreadyRegisteredException"/> class.
    /// </summary>
    /// <param name="name"></param>
    public SetAlreadyRegisteredException(string name) : base($"An unnamed Flux Set for a model '{name}' was already registered previously. Consider giving specific names to different sets for this model.")
    { }
}