namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when a Flux Set Configuration is not found.
/// </summary>
public class SetConfigurationNotFoundException : Exception
{
    internal const string DefaultMessage = "Requested Set Configuration was not found.";
    /// <summary>
    /// Initializes a new instance of the <see cref="SetConfigurationNotFoundException"/> class.
    /// </summary>
    public SetConfigurationNotFoundException(string message = DefaultMessage, Exception? innerException = null)
        : base(message, innerException) { }
}
