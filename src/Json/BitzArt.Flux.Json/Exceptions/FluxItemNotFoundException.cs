namespace BitzArt.Flux.Json;

internal class FluxItemNotFoundException<TModel> : Exception
{
    public FluxItemNotFoundException(object? id) : base($"{typeof(TModel).Name} with key {id} was not found")
    { }
}