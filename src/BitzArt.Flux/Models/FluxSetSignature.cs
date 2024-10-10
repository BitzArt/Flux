namespace BitzArt.Flux;

/// <summary>
/// A signature of a Flux set.
/// </summary>
public record FluxSetSignature(Type ModelType, Type? KeyType = null, string? Name = null);