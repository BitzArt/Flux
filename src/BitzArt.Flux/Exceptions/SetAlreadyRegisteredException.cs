namespace BitzArt.Flux;

/// <summary>
/// An exception thrown when a Flux Set is already registered,
/// and a new one is being registered with the same name.
/// </summary>
public class SetAlreadyRegisteredException : Exception
{
    internal static string GetDefaultMessage(string name)
        => $"An unnamed Flux Set for a model '{name}' was already registered previously. Consider giving specific names to different sets for this model.";

    /// <summary>
    /// Initializes a new instance of the <see cref="SetAlreadyRegisteredException"/> class.
    /// </summary>
    /// <param name="name">Model name.</param>
    /// <param name="innerException">Inner exception.</param>
    /// <returns>A new instance of the <see cref="SetAlreadyRegisteredException"/> class.</returns>
    public static SetAlreadyRegisteredException ByModelName(string name, Exception? innerException = null)
        => new(GetDefaultMessage(name), innerException);

    /// <summary>
    /// Initializes a new instance of the <see cref="SetAlreadyRegisteredException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Inner exception.</param>
    public SetAlreadyRegisteredException(string message, Exception? innerException = null)
        : base(message, innerException) { }
}