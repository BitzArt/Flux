using System.Diagnostics;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for <see cref="OperationDescriptor"/>.
/// </summary>
public static class OperationDescriptorExtensions
{
    /// <summary>
    /// Gets a friendly name for a descriptor operation type.
    /// </summary>
    /// <param name="descriptor"><see cref="OperationDescriptor"/> to get the friendly name for.</param>
    /// <returns>A string representing the friendly name of the operation.</returns>
    public static string GetFriendlyOperationName(this OperationDescriptor descriptor)
    {
        return descriptor switch
        {
            GetOperationDescriptor => "Get",
            GetAllOperationDescriptor => "Get All",
            GetPageOperationDescriptor => "Get Page",
            AddOperationDescriptor => "Add",
            UpdateOperationDescriptor => "Update",
            RemoveOperationDescriptor => "Remove",

            _ => throw new UnreachableException($"Unsupported operation descriptor type: {descriptor.GetType().FullName}")
        };
    }
}
