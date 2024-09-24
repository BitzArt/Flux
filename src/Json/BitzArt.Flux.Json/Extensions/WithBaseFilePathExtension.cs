namespace BitzArt.Flux;

public static class WithBaseFilePathExtension
{
    public static IFluxJsonServiceBuilder WithBaseFilePath(this IFluxJsonServiceBuilder builder, string baseFilePath)
    {
        builder.ServiceOptions.BaseFilePath = baseFilePath;

        return builder;
    }
}
