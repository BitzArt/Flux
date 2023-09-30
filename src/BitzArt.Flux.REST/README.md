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
        .AddModel<YourModel>()      // Adds an Model of a specific type
        .WithEndpoint("model");    // Model endpoint : https://test.com/model
});
```
### Use IFluxContext in your app

1. Resolve `IFluxContext` from your DI container
```csharp
serviceProvider.GetRequiredService<IFluxContext>();
```
2. Get your model context
```csharp
var modelContext = fluxContext.Model<YourModel>()
```
3. Use the IModelContext to interact with this model:
```csharp
var model = await modelContext.GetAsync(1); // Will make an http request to https://test.com/model/1

var list = await modelContext.GetAllAsync(); // Will make an http request to https://test.com/model

var page = await modelContext.GetPageAsync(0, 10); // Will make an http request to https://test.com/model?offset=0&limit=10
```

## Advanced scenarios

### Model endpoint configuration

```csharp
WithEndpoint("your-model-endpoint") // Sets the model REST endpoint, e.g. https://test.com/your-model-endpoint
```
```csharp
WithIdEndpoint((key) => $"something/{key}") // Sets the model ID endpoint, e.g. https://test.com/something/1
```
```csharp
WithPageEndpoint("your-page-endpoint") // Sets the model Page endpoint, e.g. https://test.com/your-page-endpoint
```

### Custom variables

You can add custom variables to your endpoint configurations:

```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")
    .UsingRest("https://test.com")
        .AddModel<YourModel>()
        .WithEndpoint("{a}/{b}");
});
```

Provide variable values when calling an appropriate method:

```csharp
var a = "first";
var b = "second";
var model = await modelContext.GetAsync(1, a, b); // Will make an http request to https://test.com/first/second/1
```

### Custom Page endpoint with parent id example:

Flux configuration:
```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")
    .UsingRest("https://test.com")
        .AddModel<Book>()
        .WithPageEndpoint("authors/{authorId}/books");
});
```

Usage:
```csharp
var books = fluxContext.Model<Book>();

var authorId = 15;
var booksPage = await books.GetPage(0, 10, authorId); // https://test.com/authors/15/books?offset=0&limit=10
```
