namespace BitzArt.Flux;

internal class FluxKeyPropertyExpressionMissingException<TModel> : Exception
{
    public FluxKeyPropertyExpressionMissingException() : base($"KeyPropertyExpression is required for {typeof(TModel).Name}. Consider using .WithKey() when configuring a Set.")
    {
    }
}