using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class QueryableSetTests
{
    private const string Data =
        """
        [
          {
            "id": 1,
            "name": "item 1"
          },
          {
            "id": 2,
            "name": "item 2"
          },
          {
            "id": 3,
            "name": "item 3"
          },
        {
            "id": 4,
            "name": "item 4"
          },
        {
            "id": 5,
            "name": "item 5"
          }
        ]
        """;

    private static IServiceProvider PrepareServices()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("test")
                .UsingJson()
                    .AddSet<TestModel, int>()
                        .FromJson(Data)
                        .WithKey(x => x.Id!);
        });

        return services.BuildServiceProvider();
    }

    [Fact]
    public void GetEnumerator_FromJsonSet_ReturnsEnumerator()
    {
        var serviceProvider = PrepareServices();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var enumerator = setContext.GetEnumerator();

        Assert.NotNull(enumerator);
        Assert.True(enumerator.MoveNext());
    }

    [Fact]
    public void ToList_OnJsonSet_ReturnsListWithAllElements()
    {
        var serviceProvider = PrepareServices();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = setContext.ToList();

        Assert.NotNull(data);
        Assert.True(data.Any());
        Assert.Equal(5, data.Count);
    }

    [Fact]
    public void FirstOrDefault_OnJsonSet_ReturnsFirst()
    {
        var serviceProvider = PrepareServices();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var item = setContext.FirstOrDefault();

        Assert.NotNull(item);
        Assert.Equal(1, item.Id);
        Assert.Equal("item 1", item.Name);
    }

    [Fact]
    public void Where_OnJsonSet_Filters()
    {
        var serviceProvider = PrepareServices();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var q = setContext.Where(x => x.Id == 1);

        var item = q.FirstOrDefault();

        Assert.NotNull(item);
        Assert.Equal(1, item.Id);
        Assert.Equal("item 1", item.Name);
    }
}
