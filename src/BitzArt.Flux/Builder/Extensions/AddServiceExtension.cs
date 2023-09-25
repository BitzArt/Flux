namespace BitzArt.Flux;

public static class AddServiceExtension
{
    public static IFluxServicePreBuilder AddService(this IFluxBuilder builder, string name)
    {
        return new FluxServicePreBuilder(builder.Services, builder.Factory, name);
    }
}
