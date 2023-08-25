using BitzArt.Communicator;

namespace BitzArt;

public static class AddEntityExtension
{
    public static IRestCommunicatorServiceBuilder AddEntity<TEntity, TKey>(this IRestCommunicatorServiceBuilder builder, string endpoint)
    {
        return builder;
    }
}
