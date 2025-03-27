namespace BitzArt.Flux.REST;

internal enum EndpointType : byte
{
    Default = 1,
    Page = 2,
    Id = 3,
}

internal static class EndpointTypeExtensions
{
    public static string GetFriendlyEndpointTypeName(this EndpointType endpointType)
    {
        return endpointType switch
        {
            EndpointType.Default => "Endpoint",
            EndpointType.Page => "Page endpoint",
            EndpointType.Id => "Id endpoint",
            _ => throw new ArgumentOutOfRangeException(nameof(endpointType), endpointType, null),
        };
    }
}
