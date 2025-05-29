namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the base file path for JSON services.
/// </summary>
public static class WithBaseFilePathExtension
{
    /// <summary>
    /// Configures the base file path to use when loading JSON files.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="baseFilePath">
    /// The base file path to use when loading JSON files.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonServiceBuilder"/> for further service configuration.
    /// </returns>
    public static IFluxJsonServiceBuilder WithBaseFilePath(this IFluxJsonServiceBuilder builder, string baseFilePath)
    {
        builder.ServiceOptions.BaseFilePath = baseFilePath;

        return builder;
    }
}
