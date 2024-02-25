namespace BitzArt.Flux;

public class EnumerationNotSupportedException : Exception
{
    public EnumerationNotSupportedException()
        : base("Enumeration is not supported in this context.")
    {
    }

    public EnumerationNotSupportedException(string message)
        : base(message)
    {
    }

    public EnumerationNotSupportedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
