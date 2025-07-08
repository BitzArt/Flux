using System.Diagnostics;

namespace BitzArt.Flux.Rest;

internal static class OperationDescriptorExtensions
{
    public static HttpMethod GetExpectedHttpMethod(this OperationDescriptor descriptor)
    {
        return descriptor switch
        {
            GetOperationDescriptor => HttpMethod.Get,

            GetAllOperationDescriptor => HttpMethod.Get,

            GetPageOperationDescriptor => HttpMethod.Get,

            AddOperationDescriptor addOperationDescriptor
                => addOperationDescriptor.Id is null ? HttpMethod.Post : HttpMethod.Put,

            UpdateOperationDescriptor updateOperationDescriptor
                => updateOperationDescriptor.Partial ? HttpMethod.Patch : HttpMethod.Put,

            RemoveOperationDescriptor => HttpMethod.Delete,

            _ => throw new UnreachableException($"Unsupported operation type: {descriptor.GetType().Name}.")
        };
    }
}
