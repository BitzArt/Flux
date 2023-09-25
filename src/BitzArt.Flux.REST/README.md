# Flux.REST

## Installation

To use a REST client in your project, add the nuget package:
```
dotnet add package BitzArt.Flux.REST
```

## Usage

### Configure Flux

```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")     // Give your external service a specific name
    .UsingRest("https://test.com")  // External service's base url
        .AddEntity<YourEntity>()    // Adds an Entity of a specific type
        .WithEndpoint("entity");    // Entity endpoint : https://test.com/entity
});
```
### Use IFluxContext in your app

1. Resolve `IFluxContext` from your DI container
```csharp
serviceProvider.GetRequiredService<IFluxContext>();
```
2. Get your entity context
```csharp
var entityContext = fluxContext.Entity<YourEntity>()
```
3. Use the IEntityContext to interact with this entity:
```csharp
var entity = await entityContext.GetAsync(1); // Will make an http request to https://test.com/entity/1

var list = await entityContext.GetAllAsync(); // Will make an http request to https://test.com/entity

var page = await entityContext.GetPageAsync(0, 10); // Will make an http request to https://test.com/entity?offset=0&limit=10
```

## Advanced scenarios

### Entity endpoint configuration

```csharp
WithEndpoint("your-entity-endpoint") // Sets the entity REST endpoint, e.g. https://test.com/your-entity-endpoint
```
```csharp
WithIdEndpoint((key) => $"something/{key}") // Sets the entity ID endpoint, e.g. https://test.com/something/1
```
```csharp
WithPageEndpoint("your-page-endpoint") // Sets the entity Page endpoint, e.g. https://test.com/your-page-endpoint
```

### Custom variables

You can add custom variables to your endpoint configurations:

```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")
    .UsingRest("https://test.com")
        .AddEntity<YourEntity>()
        .WithEndpoint("{a}/{b}");
});
```

Provide variable values when calling an appropriate method:

```csharp
var a = "first";
var b = "second";
var entity = await entityContext.GetAsync(1, a, b); // Will make an http request to https://test.com/first/second/1
```
