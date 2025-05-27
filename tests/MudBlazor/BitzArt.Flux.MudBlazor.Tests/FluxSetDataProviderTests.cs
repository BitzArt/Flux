using System.Text.Json;

namespace BitzArt.Flux.MudBlazor.Tests;

public class FluxSetDataProviderTests
{
    private class TestModel { }

    [Fact]
    public async Task Test()
    {
        var parameters = new OperationParameterCollection(
        [
            1, 2, "a"
        ]);
        /*[
            ("var1", "value"),
            ("var2", 15)
        ]);*/
        var q = new FluxSetDataPageQuery<TestModel>(null!, parameters.Parameters, null!);

        var s = JsonSerializer.Serialize(q);
    }
}