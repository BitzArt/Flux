namespace BitzArt.Flux.REST.Endpoints;

internal class SetEndpointRequirements
{
    public HttpMethod HttpMethod { get; }
    public IServiceProvider ServiceProvider { get; }

    public SetEndpointRequirements(HttpMethod httpMethod, IServiceProvider serviceProvider)
    {
        HttpMethod = httpMethod;
        ServiceProvider = serviceProvider;
    }
}

internal class EndpointRequirements<T1> : SetEndpointRequirements
{
    public T1 Parameter1 { get; }

    public EndpointRequirements(HttpMethod httpMethod, IServiceProvider serviceProvider, T1 parameter1)
        : base(httpMethod, serviceProvider)
    {
        Parameter1 = parameter1;
    }
}

internal class EndpointRequirements<T1, T2> : EndpointRequirements<T1>
{
    public T2 Parameter2 { get; }
    public EndpointRequirements(HttpMethod httpMethod, IServiceProvider serviceProvider, T1 parameter1, T2 parameter2)
        : base(httpMethod, serviceProvider, parameter1)
    {
        Parameter2 = parameter2;
    }
}

internal class EndpointRequirements<T1, T2, T3> : EndpointRequirements<T1, T2>
{
    public T3 Parameter3 { get; }
    public EndpointRequirements(HttpMethod httpMethod, IServiceProvider serviceProvider, T1 parameter1, T2 parameter2, T3 parameter3)
        : base(httpMethod, serviceProvider, parameter1, parameter2)
    {
        Parameter3 = parameter3;
    }
}
