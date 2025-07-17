namespace BitzArt.Flux.Json;

public class JsonServiceTests
{
    private readonly IFluxSetContext<TestModel> _setContext = TestSetContext.GetTestSetContext();

    [Fact]
    public async Task GetAllAsync_TestModel_ShouldReturnAll()
    {
        // Arrange/Act
        var result = await _setContext.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(0, 5)]
    [InlineData(5, 5)]
    public async Task GetPageAsync_TestModel_ShouldReturnPage(int offset, int limit)
    {
        // Arrange/Act
        var result = await _setContext.GetPageAsync(offset, limit);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Items);
        Assert.True(result.Items.Any());
        Assert.Equal(limit, result.Items.Count());
    }

    [Fact]
    public async Task GetAsync_TestModel_ReturnsModel()
    {
        // Arrange/Act
        var result = await _setContext.GetAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsync_NotExistingTestModel_ShouldThrow()
    {
        // Arrange
        Task Action() => _setContext.GetAsync(100);

        // Act/Assert
        await Assert.ThrowsAsync<FluxItemNotFoundException<TestModel>>(Action);
    }
}