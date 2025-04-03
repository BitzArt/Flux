namespace BitzArt.Flux;

/// <summary>
/// Indicates that multiple service contexts were found when only one was expected.
/// </summary>
public class MultipleServiceContextsFoundException : Exception
{
    internal const string DefaultMessage = "Multiple matching Flux Service Contexts were found.";

    /// <summary>
    /// Initializes a new instance of the <see cref="MultipleServiceContextsFoundException"/> class.
    /// </summary>
    public MultipleServiceContextsFoundException(string message = DefaultMessage, Exception? innerException = null)
        : base(message, innerException) { }
}