namespace BitzArt.Flux.Rest;

internal static class OperationDescriptorTypeExtensions
{
    /// <summary>
    /// Gets the supported HTTP methods for a given <see cref="OperationDescriptor"/> type.
    /// </summary>
    /// <param name="operationDescriptorType">Type of the operation descriptor.</param>
    /// <returns>A collection of <see cref="HttpMethod"/> instances.</returns>
    public static IEnumerable<HttpMethod> GetSupportedHttpMethods(this Type operationDescriptorType)
    {
        if (operationDescriptorType == typeof(GetOperationDescriptor))
        {
            return [HttpMethod.Get];
        }
        if (operationDescriptorType == typeof(GetAllOperationDescriptor))
        {
            return [HttpMethod.Get];
        }
        if (operationDescriptorType == typeof(GetPageOperationDescriptor))
        {
            return [HttpMethod.Get];
        }
        if (operationDescriptorType == typeof(AddOperationDescriptor))
        {
            return [HttpMethod.Post, HttpMethod.Put];
        }
        if (operationDescriptorType == typeof(UpdateOperationDescriptor))
        {
            return [HttpMethod.Put, HttpMethod.Patch];
        }
        if (operationDescriptorType == typeof(RemoveOperationDescriptor))
        {
            return [HttpMethod.Delete];
        }
        if (operationDescriptorType == typeof(ModelOperationDescriptor))
        {
            return [HttpMethod.Post, HttpMethod.Put, HttpMethod.Patch];
        }
        if (operationDescriptorType == typeof(KeyedOperationDescriptor))
        {
            return [HttpMethod.Get, HttpMethod.Post, HttpMethod.Put, HttpMethod.Patch, HttpMethod.Delete];
        }
        if (operationDescriptorType == typeof(OperationDescriptor))
        {
            return HttpMethods.All.ToHttpMethods();
        }
        throw new NotSupportedException($"Unsupported operation descriptor type: {operationDescriptorType.Name}");
    }
}
