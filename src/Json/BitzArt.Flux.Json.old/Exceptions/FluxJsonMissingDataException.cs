namespace BitzArt.Flux;

internal class FluxJsonMissingDataException : Exception
{
    public FluxJsonMissingDataException() : base("Missing set data. Consider populating the set with json data when configuring Flux.")
    { }
}
