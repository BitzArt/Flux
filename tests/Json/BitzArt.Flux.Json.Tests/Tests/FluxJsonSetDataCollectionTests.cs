namespace BitzArt.Flux.Json;

public class FluxJsonSetDataCollectionTests
{
    [Fact]
    public void Create_ValidConditions_ShouldCreateNewDataCollection()
    {
        // Arrange/Act
        var items = new List<TestModel>
        {
            new (1, "Item 1"),
            new (2, "Item 2")
        };

        var dataCollection = new FluxJsonDataCollection<TestModel>(items);

        // Assert
        Assert.NotNull(dataCollection);
    }

    [Fact]
    public void Create_WhenItemsIsNull_ShouldThrow()
    {
        // Arrange/Act/Assert
        Assert.Throws<ArgumentNullException>(() => new FluxJsonDataCollection<TestModel>(null!));
    }

    [Fact]
    public void FetchOperations_WhenItemsIsNotEmptyAndKeyPropertySelectorSet_ShouldFetchCorrectly()
    {
        // Arrange
        var items = new List<TestModel>
        {
            new (1, "Item 1"),
            new (2, "Item 2")
        };

        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.SetKeyPropertySelector(x => x.Id!.Value);

        // Act/Assert
        Assert.Equal(items.Count, dataCollection.GetAll().Count);
        Assert.All(items, item =>
        {
            Assert.Equal(item, dataCollection.Get(item.Id));
        });
    }

    [Fact]
    public void KeyedItems_WhenHasItemsWithSameKeys_ShouldThrow()
    {
        // Arrange/Act
        var items = new List<TestModel>
           {
               new (1, "Item"),
               new (1, "Item with same Id")
           };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);

        // Assert
        Assert.ThrowsAny<Exception>(() => dataCollection.SetKeyPropertySelector(x => x.Id!.Value));
    }

    [Fact]
    public void GetById_WhenKeyPropertySelectorIsNotSet_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel> { new(1, "Item 1") };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);

        // Act/Assert
        Assert.ThrowsAny<Exception>(() => dataCollection.Get(1));
    }

    [Fact]
    public void Add_ValidConditions_ShouldAddItemToItemsAndUpdateKeyedItems()
    {
        // Arrange
        var items = new List<TestModel>
        {
            new(1, "Item in list")
        };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.SetKeyPropertySelector(x => x.Id!.Value);
        var newItem = new TestModel(2, "New Item");

        // Act
        dataCollection.Add(newItem);

        // Assert
        Assert.Contains(newItem, dataCollection.GetAll());
        Assert.Equal(newItem, dataCollection.Get(2));
    }

    [Fact]
    public void Add_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel>();
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.SetKeyPropertySelector(x => x.Id!.Value);

        // Act/Assert
        Assert.Throws<ArgumentNullException>(() => dataCollection.Add(null!));
    }

    [Fact]
    public void Add_WhenItemWithSameKeyExists_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel>
        {
            new(1, "Item in list")
        };

        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.SetKeyPropertySelector(x => x.Id!.Value);

        var newItem = new TestModel(1, "New Item with same Id");

        // Act/Assert
        Assert.ThrowsAny<Exception>(() => dataCollection.Add(newItem));
    }

    [Fact]
    public void Remove_ValidConditions_ShouldRemoveItem()
    {
        // Arrange
        var itemToRemove = new TestModel(1, "Item 1");
        var items = new List<TestModel> { itemToRemove };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.SetKeyPropertySelector(x => x.Id!.Value);

        // Act
        var removed = dataCollection.Remove(itemToRemove.Id);

        // Assert
        Assert.True(removed);
        Assert.DoesNotContain(itemToRemove, dataCollection.GetAll());
    }

    [Fact]
    public void Remove_WhenItemIsNotAPartOfCollection_ShouldThrow()
    {
        // Arrange
        var itemToRemove = new TestModel(1, "Item 1");
        var items = new List<TestModel> { new(2, "Item 2") };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.SetKeyPropertySelector(x => x.Id!.Value);

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => dataCollection.Remove(itemToRemove.Id));
    }
}
