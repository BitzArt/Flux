namespace BitzArt.Communicator;

internal class RestCommunicatorServiceBuilder : IRestCommunicatorServiceBuilder
{
    public string Name { get; set; }
    public string? BaseUrl { get; set; }

    public ICommunicatorServiceFactory Factory { get; set; }

    public RestCommunicatorServiceBuilder(ICommunicatorServicePreBuilder prebuilder, string? baseUrl)
    {
        if (prebuilder.Name is null) throw new Exception("Missing Name in Communication Service configuration. Consider using .WithName() when configuring external services.");
        Name = prebuilder.Name;
        Factory = prebuilder.Factory;
        BaseUrl = baseUrl;
    }
}
