# Flux.REST

## Installation

To use a REST client in your project, add the nuget package **(The package is currently prerelease)**
```
dotnet add package BitzArt.Flux.REST --prerelease
```

## Usage

### Configure Flux

```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")     // Give your external service a specific name
    .UsingRest("https://test.com")  // External service's base url
        .AddSet<YourModel>()        // Adds an Set of a specific model
        .WithEndpoint("model");     // Set endpoint : https://test.com/model
});
```
### Use IFluxContext in your app

1. Resolve `IFluxContext` from your DI container

```csharp
serviceProvider.GetRequiredService<IFluxContext>();
```

2. Get your set context

```csharp
// Resolve the Set directly
var setContext = fluxContext.Set<YourModel>();

// Or specify an external service
var setContext = fluxContext.Service("service1").Set<YourModel>();
```

> ℹ️
> Selecting a specific service can be useful in situations where you have multiple external services configured with the same model types.

3. Use the SetContext to interact with this Set:

```csharp
var model = await setContext.GetAsync(1); // Will make an http request to https://test.com/model/1

var list = await setContext.GetAllAsync(); // Will make an http request to https://test.com/model

var page = await setContext.GetPageAsync(0, 10); // Will make an http request to https://test.com/model?offset=0&limit=10
```

## Advanced scenarios

### Configuring endpoints

```csharp
WithEndpoint("your-set-endpoint") // Configures the REST endpoint, e.g. https://test.com/your-set-endpoint
```
```csharp
WithIdEndpoint((key) => $"something/{key}") // Configures the ID endpoint, e.g. https://test.com/something/1
```
```csharp
WithPageEndpoint("your-page-endpoint") // Configures the Page endpoint, e.g. https://test.com/your-page-endpoint
```

### Custom variables

You can add custom variables to your endpoint configurations:

```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")
    .UsingRest("https://test.com")
        .AddSet<YourModel>()
        .WithEndpoint("{a}/{b}");
});
```

Provide variable values when calling an appropriate method:

```csharp
var a = "first";
var b = "second";
var model = await setContext.GetAsync(1, a, b); // Will make an http request to https://test.com/first/second/1
```

### Custom Page endpoint with parent id example:

Flux configuration:
```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")
    .UsingRest("https://test.com")
        .AddSet<Book>()
        .WithPageEndpoint("authors/{authorId}/books");
});
```

Usage:
```csharp
var books = fluxContext.Set<Book>();

var authorId = 15;
var booksPage = await books.GetPage(0, 10, authorId); // https://test.com/authors/15/books?offset=0&limit=10
```
