namespace BitzArt.Flux;

/// <summary>
/// Exception thrown when enumeration is not supported in a given context.
/// </summary>
public class EnumerationNotSupportedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerationNotSupportedException"/> class.
    /// </summary>
    public EnumerationNotSupportedException()
        : base("Enumeration is not supported in this context.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerationNotSupportedException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public EnumerationNotSupportedException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerationNotSupportedException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public EnumerationNotSupportedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
