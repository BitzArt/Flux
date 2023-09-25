# Flux | PRELEASE, Work In Progress

**Flux** is a universal WebAPI Client. It introduces an intuitive way for your software to communicate with external systems, whether they use REST, SOAP, or some other communication standard.

**1. Easy Setup: Configure Once, Use Anywhere**

Start by setting up the configuration. Instead of dealing with the intricacies of each external service, configure everything in one place. Define communication rules, endpoints, and other essentials. This step keeps your domain code clean and free from the complexities of different services and communication methods.

**2. Natural Interaction: Forget Protocols, Focus on Results**

Once the setup is done, interaction becomes a breeze. With the groundwork laid out, you can focus on getting things done. Fetch data, send updates, or perform other tasks without worrying about HTTP, REST, SOAP, or any other protocol. **Flux** handles the technicalities seamlessly in the background.

**Key Features:**

- **Practical Abstraction:** **Flux** simplifies your workflow by making interactions with external services straightforward. It's not tied to any specific technology – its goal is to make your work easier.

- **Cleaner Code:** Maintain a clean and organized codebase. With the configuration handling the heavy lifting, your interactions remain consistent and easy to manage. This is Dependency Inversion at it's finest.

- **Flexible Services:** Your code working with external dependencies follows a unified approach. The package manages the intricacies behind the scenes. Switching from one service to another or adapting to different protocols becomes hassle-free. The configuration takes care of the adaptation, sparing your code from unnecessary complexities.

- **Developer-Focused:** This package is designed to save you time and effort. It's all about enhancing simplicity in your work and making coding a more enjoyable experience.

# Installation

To use a REST client in your project, add the nuget package:
```
dotnet add package BitzArt.Flux.REST
```

# Usage

## Configure Flux

```csharp
services.AddFlux(flux =>
{
    flux.AddService("service1")     // Give your external service a specific name
    .UsingRest("https://test.com")  // External service's base url
        .AddEntity<YourEntity>()    // Adds an Entity of a specific type
        .WithEndpoint("entity");    // Entity endpoint : https://test.com/entity
});
```
## Use IFluxContext in your app

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

# Advanced scenarios

## Entity endpoint configuration

```csharp
WithEndpoint("your-entity-endpoint") // Sets the entity REST endpoint, e.g. https://test.com/your-entity-endpoint
```
```csharp
WithIdEndpoint((key) => $"something/{key}") // Sets the entity ID endpoint, e.g. https://test.com/something/1
```
```csharp
WithPageEndpoint("your-page-endpoint") // Sets the entity Page endpoint, e.g. https://test.com/your-page-endpoint
```

## Custom variables

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
