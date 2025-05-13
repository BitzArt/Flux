using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

/// <summary>
/// Flux REST set builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
/// <typeparam name="TKey">Set key type.</typeparam>
public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestServiceBuilder, IFluxSetBuilder
    where TModel : class
{
    internal new IFluxRestServiceBuilder ServiceBuilder { get; }

    // ================ Inherited members ================

    IServiceCollection IFluxBuilder.ServiceCollection => FluxBuilder.ServiceCollection;
    IFluxServiceBuilder IFluxSetBuilder.ServiceBuilder => ServiceBuilder;
    ServiceConfiguration IFluxRestServiceBuilder.ServiceConfiguration => ServiceBuilder.ServiceConfiguration;
    string IFluxServiceBuilder.ImplementationName => ServiceBuilder.ImplementationName;
}
