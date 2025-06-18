namespace BitzArt.Flux.Json;

internal class FluxJsonFileReadException : Exception
{
    public FluxJsonFileReadException(string path, Exception innerException)
        : base($"Error reading JSON from file '{path}'. See inner exception for details", innerException)
    { }
}