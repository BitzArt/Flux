namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when a Flux Set Configuration is not found.
/// </summary>
public class SetConfigurationNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SetConfigurationNotFoundException"/> class.
    /// </summary>
    public SetConfigurationNotFoundException() : base("Requested Set Configuration was not found.")
    { }
}
