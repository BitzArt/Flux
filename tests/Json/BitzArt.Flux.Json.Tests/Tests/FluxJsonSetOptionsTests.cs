using BitzArt.Flux.Json;

namespace BitzArt.Flux;

public class FluxJsonSetOptionsTests
{
    [Fact]
    public void CreateFluxJsonSetOptions_WhenHasItemsWithSameKeys_ShouldThrow()
    {
        // Arrange/Act
        var items = new List<TestModel>
           {
               new TestModel { Id = 1, Name = "Item" },
               new TestModel { Id = 1, Name = "Item with same Id" }
           };

        // Assert
        Assert.Throws<ArgumentException>(() => new FluxJsonDataCollection<TestModel, int>
        {
            Items = items,
            KeyPropertySelector = x => x.Id!.Value
        });
    }

    [Fact]
    public void KeyedItems_WhenItemsIsNull_ShouldBeNull()
    {
        // Arrange/Act
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = null,
            KeyPropertySelector = x => x.Id!.Value
        };

        // Assert
        Assert.Null(options.KeyedItems);
    }

    [Fact]
    public void KeyedItems_WhenItemsIsEmpty_ShouldBeEmpty()
    {
        // Arrange/Act
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>(),
            KeyPropertySelector = x => x.Id!.Value
        };

        // Assert
        Assert.NotNull(options.KeyedItems);
        Assert.Empty(options.KeyedItems);
    }

    [Fact]
    public void KeyedItems_WhenItemsIsNotEmpty_ShouldContainItems()
    {
        // Arrange/Act
        var items = new List<TestModel>
        {
            new (1, "Item 1"),
            new (2, "Item 2")
        };

        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = items,
            KeyPropertySelector = x => x.Id!.Value
        };

        // Assert
        Assert.NotNull(options.KeyedItems);
        Assert.Equal(items.Count, options.KeyedItems.Count);
    }

    [Fact]
    public void KeyedItems_WhenKeyPropertySelectorIsNull_ShouldBeNull()
    {
        // Arrange/Act
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>
            {
                new(1, "Item 1")
            },
            KeyPropertySelector = null
        };

        // Assert
        Assert.Null(options.KeyedItems);
    }

    [Fact]
    public void GetEnumerator_WhenItemsIsNull_ShouldThrow()
    {
        // Arrange/Act
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = null,
            KeyPropertySelector = x => x.Id!.Value
        };

        // Assert
        Assert.Throws<InvalidOperationException>(() => _ = options.GetEnumerator());
    }

    [Fact]
    public void Add_WhenItemsIsNull_ShouldThrow()
    {
        // Arrange
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = null,
            KeyPropertySelector = x => x.Id!.Value
        };
        var newItem = new TestModel(1, "New Item");

        // Act/Assert
        Assert.Throws<InvalidOperationException>(() => options.Add(newItem));
    }

    [Fact]
    public void Add_WhenItemsIsNotNull_ShouldAddItemToItems()
    {
        // Arrange
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>() { new (1, "Item in list")},
            KeyPropertySelector = x => x.Id!.Value
        };
        var newItem = new TestModel(2, "New Item");

        // Act
        options.Add(newItem);

        // Assert
        Assert.Contains(newItem, options.Items!);
    }

    [Fact]
    public void Add_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>(),
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act/Assert
        Assert.Throws<ArgumentNullException>(() => options.Add(null!));
    }

    [Fact]
    public void Clear_WhenItemsIsNotNull_ShouldClear()
    {
        // Arrange
        var options = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel> { new(1, "Item in list") },
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act
        options.Clear();

        // Assert
        Assert.Empty(options.Items);
    }
}