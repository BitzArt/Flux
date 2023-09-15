namespace BitzArt.Flux;

public static class AddServiceExtension
{
    public static IFluxServicePreBuilder AddService(this IFluxBuilder builder, string name)
    {
        var service = new FluxServicePreBuilder(builder.Services, builder.Factory, name);

        return service;
    }
}
