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
    public void KeyedItems_WhenItemsIsNotEmptyAndKeyPropertySelectorSet_ShouldContainItems()
    {
        // Arrange/Act
        var items = new List<TestModel>
        {
            new (1, "Item 1"),
            new (2, "Item 2")
        };

        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Assert
        Assert.NotNull(dataCollection.KeyedItems);
        Assert.Equal(items.Count, dataCollection.KeyedItems.Count);
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
        Assert.Throws<ArgumentException>(() => dataCollection.KeyPropertySelector = x => x.Id!.Value);
    }

    [Fact]
    public void KeyedItems_WhenKeyPropertySelectorIsNotSet_ShouldBeNull()
    {
        // Arrange/Act
        var items = new List<TestModel> { new(1, "Item 1") };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);

        // Assert
        Assert.Null(dataCollection.KeyedItems);
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
        dataCollection.KeyPropertySelector = x => x.Id!.Value;
        var newItem = new TestModel(2, "New Item");

        // Act
        dataCollection.Add(newItem);

        // Assert
        Assert.Contains(newItem, dataCollection.Items);
        Assert.Contains(newItem, dataCollection.KeyedItems!.Values);
    }

    [Fact]
    public void Add_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel>();
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

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
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        var newItem = new TestModel(1, "New Item with same Id");

        // Act/Assert
        Assert.Throws<ArgumentException>(() => dataCollection.Add(newItem));
    }

    [Fact]
    public void Clear_WhenItemsIsNotNull_ShouldClearAndUpdateKeyedItems()
    {
        // Arrange
        var items = new List<TestModel> { new(1, "Item in list") };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Act
        dataCollection.Clear();

        // Assert
        Assert.Empty(dataCollection.Items);
        Assert.Empty(dataCollection.KeyedItems!);
    }

    [Fact]
    public void Clear_WhenItemsIsEmpty_ShouldDoNothing()
    {
        // Arrange
        var items = new List<TestModel>();
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);

        // Act
        dataCollection.Clear();

        // Assert
        Assert.Empty(dataCollection.Items);
    }

    [Fact]
    public void Contains_WhenItemIsInCollection_ShouldReturnTrue()
    {
        // Arrange
        var itemToCheck = new TestModel(1, "Item 1");
        var items = new List<TestModel> { itemToCheck };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);

        // Act
        var contains = dataCollection.Contains(itemToCheck);

        // Assert
        Assert.True(contains);
    }

    [Fact]
    public void Contains_WhenItemIsNotInCollection_ShouldReturnFalse()
    {
        // Arrange
        var itemToCheck = new TestModel(1, "Item 1");
        var items = new List<TestModel>();
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Act
        var contains = dataCollection.Contains(itemToCheck);

        // Assert
        Assert.False(contains);
    }

    [Fact]
    public void Contains_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel>();
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Act/Assert
        Assert.Throws<ArgumentNullException>(() => dataCollection.Contains(null!));
    }

    [Fact]
    public void CopyTo_ValidConditions_ShouldCopyItemsToArray()
    {
        // Arrange
        var items = new List<TestModel>
        {
            new(1, "Item 1"),
            new(2, "Item 2")
        };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        var array = new TestModel[2];

        // Act
        dataCollection.CopyTo(array, 0);

        // Assert
        Assert.Equal(items, array);
    }

    [Fact]
    public void CopyTo_WhenArrayIsNull_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel>();
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Act/Assert
        Assert.Throws<ArgumentNullException>(() => dataCollection.CopyTo(null!, 0));
    }

    [Fact]
    public void CopyTo_WhenArrayIndexIsOutOfRange_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel>
        {
            new(1, "Item 1"),
            new(2, "Item 2")
        };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;
        var array = new TestModel[1];

        // Act/Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => dataCollection.CopyTo(array, 0));
    }

    [Fact]
    public void Remove_ValidConditions_ShouldRemoveItem()
    {
        // Arrange
        var itemToRemove = new TestModel(1, "Item 1");
        var items = new List<TestModel> { itemToRemove };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Act
        var removed = dataCollection.Remove(itemToRemove);

        // Assert
        Assert.True(removed);
        Assert.DoesNotContain(itemToRemove, dataCollection.Items);
    }

    [Fact]
    public void Remove_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var items = new List<TestModel> { new(1, "Item 1") };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Act/Assert
        Assert.Throws<ArgumentNullException>(() => dataCollection.Remove(null!));
    }

    [Fact]
    public void Remove_WhenItemIsNotAPartOfCollection_ShouldReturnFalse()
    {
        // Arrange
        var itemToRemove = new TestModel(1, "Item 1");
        var items = new List<TestModel> { new(2, "Item 2") };
        var dataCollection = new FluxJsonDataCollection<TestModel>(items);
        dataCollection.KeyPropertySelector = x => x.Id!.Value;

        // Act
        var removed = dataCollection.Remove(itemToRemove);

        // Assert
        Assert.False(removed);
    }
}