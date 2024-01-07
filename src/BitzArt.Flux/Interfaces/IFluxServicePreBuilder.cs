using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Service Prebuilder instance. <br/>
/// Stores properties prepared for the Flux Service Builder.<br/>
/// Use a <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementation</see>
/// in order to create an actual Flux Service Builder from this.
/// </summary>
public interface IFluxServicePreBuilder
{
    internal string? Name { get; set; }
    internal IServiceCollection Services { get; }
    internal IFluxFactory Factory { get; }
}
