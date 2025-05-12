using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace BitzArt.Flux.Builder;

internal class FluxBuilder : IFluxBuilder, IDisposable
{
    public IServiceCollection ServiceCollection { get; private init; }

    private readonly ConcurrentDictionary<string, FluxServiceRegistration> _serviceRegistrations;
    private record FluxServiceRegistration
    {
        public IFluxServiceBuilder? ConfiguredBuilder { get; set; } = null;
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

    public void OnServiceProtocolConfigured(string name, IFluxServiceBuilder terminatedBuilder)
    {
        if (!_serviceRegistrations.TryGetValue(name, out var registration))
        {
            throw new UnreachableException($"Service '{name}' was not found and can not be marked as terminated.");
        }
        if (registration.ConfiguredBuilder is not null)
        {
            throw new InvalidOperationException($"Service '{name}' has already been configured previously by the implementation: '{registration.ConfiguredBuilder.ImplementationName}'.");
        }

        registration.ConfiguredBuilder = terminatedBuilder;
    }

    public void Dispose()
    {
        var unterminated = _serviceRegistrations
            .Where(kvp => kvp.Value.ConfiguredBuilder is null)
            .ToList();

        if (unterminated.Count > 0)
        {
            var names = string.Join(", ", unterminated.Select(kvp => kvp.Key));

            throw new InvalidOperationException(
                $"The following flux services have not been terminated: {names}. " +
                $"Use a Flux implementation of your choice in order to finalize their configuration.");
        }
    }
}
