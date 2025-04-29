using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace BitzArt.Flux.Builder;

internal class FluxBuilder : IFluxBuilder
{
    public IServiceCollection ServiceCollection { get; private init; }

    private readonly ConcurrentDictionary<string, FluxServiceRegistration> _serviceRegistrations;
    private record FluxServiceRegistration
    {
        public IFluxServiceBuilder? TerminatedBuilder { get; set; } = null;
    }

    public FluxBuilder(IServiceCollection serviceCollection)
    {
        ServiceCollection = serviceCollection;

        _serviceRegistrations = [];
    }

    public void OnServiceAdded(string name)
    {
        if (!_serviceRegistrations.TryAdd(name, new()))
        {
            throw new InvalidOperationException($"Service with name '{name}' already exists.");
        }
    }

    public void OnServiceTerminated(string name, IFluxServiceBuilder terminatedBuilder)
    {
        if (!_serviceRegistrations.TryGetValue(name, out var registration))
        {
            throw new InvalidOperationException($"Service with name '{name}' was not found and thus can not be terminated.");
        }
        if (registration.TerminatedBuilder is not null)
        {
            throw new InvalidOperationException($"Service with name '{name}' has already been terminated previously.");
        }

        registration.TerminatedBuilder = terminatedBuilder;
    }
}
