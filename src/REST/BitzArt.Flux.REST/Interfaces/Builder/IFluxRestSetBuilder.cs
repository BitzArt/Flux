using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

/// <summary>
/// Flux REST set builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
/// <typeparam name="TKey">Set key type.</typeparam>
public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestServiceBuilder
    where TModel : class
{
    internal IFluxRestServiceBuilder ServiceBuilder { get; }

    IServiceCollection IFluxBuilder.ServiceCollection => ServiceBuilder.ServiceCollection;
    IFluxBuilder IFluxServiceBuilder.FluxBuilder => ServiceBuilder.FluxBuilder;
    string IFluxServiceBuilder.ServiceName => ServiceBuilder.ServiceName;
    IFluxServiceBuilder IFluxRestServiceBuilder.SourceBuilder => ServiceBuilder.SourceBuilder;
    FluxRestServiceOptions IFluxRestServiceBuilder.ServiceOptions => ServiceBuilder.ServiceOptions;
    Action<IServiceProvider, HttpClient>? IFluxRestServiceBuilder.HttpClientConfiguration
    {
        get => ServiceBuilder.HttpClientConfiguration;
        set => ServiceBuilder.HttpClientConfiguration = value;
    }
}
