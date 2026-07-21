namespace BitzArt.Flux;

/// <summary>
/// Describes an operation to be executed by the Flux engine. <br />
/// </summary>
public abstract class OperationDescriptor
{
    /// <summary>
    /// Input parameters to be used in the operation.
    /// </summary>
    public IOperationParameterCollection? Parameters { get; set; }

    public IOperationParameterCollection? ExtensionParameters { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationDescriptor"/> class.
    /// </summary>
    /// <param name="parameters">Input parameters to be used in the operation.</param>
    public OperationDescriptor(IOperationParameterCollection? parameters)
    {
        Parameters = parameters;
    }

    /// <summary>
    /// A static class containing information about available <see cref="OperationDescriptor"/> types.
    /// </summary>
    public static class Types
    {
        /// <summary>
        /// All <see cref="OperationDescriptor"/> types.
        /// </summary>
        public static IEnumerable<Type> All => Base.Concat(Concrete).ToList().AsReadOnly();

        /// <summary>
        /// Concrete <see cref="OperationDescriptor"/> types.
        /// </summary>
        public static IEnumerable<Type> Concrete => new[]
        {
            typeof(GetOperationDescriptor),
            typeof(GetAllOperationDescriptor),
            typeof(GetPageOperationDescriptor),
            typeof(AddOperationDescriptor),
            typeof(UpdateOperationDescriptor),
            typeof(RemoveOperationDescriptor)
        }.AsReadOnly();

        /// <summary>
        /// Base <see cref="OperationDescriptor"/> types.
        /// </summary>
        public static IEnumerable<Type> Base => new[]
        {
            typeof(ModelOperationDescriptor),
            typeof(KeyedOperationDescriptor),
            typeof(OperationDescriptor)
        }.AsReadOnly();
    }
}
