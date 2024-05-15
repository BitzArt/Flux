namespace BitzArt.Flux;

public class FluxSetOperationOnNonFluxQueryableException : Exception
{
    public FluxSetOperationOnNonFluxQueryableException(string methodName)
        : base($"Calling {methodName} is only supported with Flux Sets")
    {
    }
}
