using BitzArt.Communicator;

namespace BitzArt;

public static class ICommunicatorServicePreBuilderExtensions
{
    public static ICommunicatorServicePreBuilder WithName(this ICommunicatorServicePreBuilder prebuilder, string name)
    {
        if (name is null) throw new Exception("'name' can not be null");
        if (string.IsNullOrWhiteSpace(name)) throw new Exception("'name' can not be empty");
        prebuilder.Name = name;

        return prebuilder;
    }
}
