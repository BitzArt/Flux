namespace BitzArt.Flux;

public class JsonServiceTests
{
    private readonly IFluxSetContext<TestModel> _setContext = TestSetContext.GetTestSetContext();

    [Fact]
    public async Task GetAllAsync_TestModel_ReturnsAll()
    {
        var result = await _setContext.GetAllAsync();

        Assert.NotNull(result);
        Assert.True(result.Any());
    }
    
    [Theory]
    [InlineData(0, 10)]
    [InlineData(0, 5)]
    [InlineData(5, 5)]
    public async Task GetPageAsync_TestModel_ReturnsPage(int offset, int limit)
    {
        var result = await _setContext.GetPageAsync(offset, limit);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Any());
        Assert.Equal(limit, result.Data.Count());
    }
    
    [Fact]
    public async Task GetAsync_TestModel_ReturnsModel()
    {
        var result = await _setContext.GetAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    
    [Fact]
    public async Task GetAsync_NotExistingTestModel_Throws()
    {
        Task Action() => _setContext.GetAsync(100);
        await Assert.ThrowsAsync<FluxItemNotFoundException<TestModel>>(Action);
    }
}