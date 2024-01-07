namespace BitzArt.Flux;

public static class AddServiceExtension
{
    /// <summary>
    /// Adds a Service to the IFluxBuilder.
    /// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
    /// </summary>
    public static IFluxServicePreBuilder AddService(this IFluxBuilder builder, string name)
    {
        return new FluxServicePreBuilder(builder.Services, builder.Factory, name);
    }
}
