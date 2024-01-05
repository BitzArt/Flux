namespace BitzArt.Flux;

internal class FluxJsonDeserializationException<TModel> : Exception
{
    public FluxJsonDeserializationException()
        : base($"Failed to deserialize JSON to {typeof(TModel).Name}")
    { }
}