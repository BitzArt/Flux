# Advanced Usage

## Resolving Flux Context

`IFluxContext` is the basic entry point for working with Flux. Inject it into the class that needs to communicate with an external service:

```csharp
public class ExampleService(IFluxContext fluxContext)
{
    private readonly IFluxContext _fluxContext = fluxContext;
}
```

You can also resolve it directly from an `IServiceProvider`:

```csharp
var fluxContext = serviceProvider.GetRequiredService<IFluxContext>();
```

## Resolving a specific Flux Service
A service context is useful when multiple services contain sets for the same model type.

Use `Service` when you need to work with a specific configured **Flux Service**:

```csharp
IFluxServiceContext fluxService = fluxContext.Service("school-api");
```

The service name must match the name passed to `AddService` during configuration.

## Resolving a specific Flux Set Context

A **Set Context** provides access to the operations available for a configured **Set**.

Resolve a set directly by its model type:

```csharp
IFluxSetContext<Student> setContext = fluxContext.Set<Student>();
```

When the model type is configured in multiple services, specify the service explicitly:

```csharp
IFluxSetContext<Student> setContext = 
    fluxContext.Service("school-api").Set<Student>();
```

If a service contains multiple sets for the same model type, specify the set key used during configuration:

```csharp
IFluxSetContext<Student> setContext = 
    fluxContext.Service("school-api").Set<Student>(setKey: "set-name");
```

## Passing Operation Parameters

`OperationParameterCollection` provide additional values required by a specific operation. Their meaning depends on the configuration for a specific Flux implementation.

### Named Parameters

Use named parameters when each value has a specific name:

```csharp
List<KeyValuePair<string, object>> values = [new("courseId", courseId)];
OperationParameterCollection parameters = new(values);

var students = await setContext.GetAllAsync(parameters);
```

Named parameters are passed as name-value pairs. For example, the REST implementation can use them to replace endpoint placeholders or add query parameters.

### Positional Parameters

Use positional parameters when the configured operation expects values in a specific order:

```csharp
List<object> values = [courseId];
OperationParameterCollection parameters = new(values);

var students = await setContext.GetAllAsync(parameters);
```

Flux passes positional parameters in the order in which they appear in the collection.

## Using Operation Descriptors

**TODO**
