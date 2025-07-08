using BitzArt.Flux.Json;

namespace BitzArt.Flux;

public class FluxJsonSetdataCollectionTests
{
    [Fact]
    public void KeyedItems_WhenItemsIsNotEmptyAndKeySelectorSet_ShouldContainItems()
    {
        // Arrange/Act
        var items = new List<TestModel>
        {
            new (1, "Item 1"),
            new (2, "Item 2")
        };

        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = items,
            KeyPropertySelector = x => x.Id!.Value
        };

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
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = null,
            KeyPropertySelector = x => x.Id!.Value
        };

        // Assert
        Assert.Null(dataCollection.KeyedItems);
    }

    [Fact]
    public void KeyedItems_WhenKeyPropertySelectorIsNull_ShouldBeNull()
    {
        // Arrange/Act
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>
            {
                new(1, "Item 1")
            },
            KeyPropertySelector = null
        };

        // Assert
        Assert.Null(dataCollection.KeyedItems);
    }

    [Fact]
    public void GetEnumerator_WhenItemsIsNull_ShouldThrow()
    {
        // Arrange/Act
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = null,
            KeyPropertySelector = x => x.Id!.Value
        };

        // Assert
        Assert.Throws<InvalidOperationException>(() => _ = dataCollection.GetEnumerator());
    }

    [Fact]
    public void Add_ValidConditions_ShouldAddItemToItems()
    {
        // Arrange
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>() { new(1, "Item in list") },
            KeyPropertySelector = x => x.Id!.Value
        };
        var newItem = new TestModel(2, "New Item");

        // Act
        dataCollection.Add(newItem);

        // Assert
        Assert.Contains(newItem, dataCollection.Items!);
    }

    [Fact]
    public void Add_WhenItemsIsNull_ShouldThrow()
    {
        // Arrange
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = null,
            KeyPropertySelector = x => x.Id!.Value
        };
        var newItem = new TestModel(1, "New Item");

        // Act/Assert
        Assert.Throws<InvalidOperationException>(() => dataCollection.Add(newItem));
    }

    [Fact]
    public void Add_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>(),
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act/Assert
        Assert.Throws<ArgumentNullException>(() => dataCollection.Add(null!));
    }

    [Fact]
    public void Clear_WhenItemsIsNotNull_ShouldClear()
    {
        // Arrange
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel> { new(1, "Item in list") },
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act
        dataCollection.Clear();

        // Assert
        Assert.Empty(dataCollection.Items);
    }

    [Fact]
    public void Contains_WhenItemIsInCollection_ShouldReturnTrue()
    {
        // Arrange
        var item = new TestModel(1, "Item 1");

        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel> { item },
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act
        var contains = dataCollection.Contains(item);

        // Assert
        Assert.True(contains);
    }

    [Fact]
    public void Contains_WhenItemIsNotInCollection_ShouldReturnFalse()
    {
        // Arrange
        var item = new TestModel(1, "Item 1");
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>(),
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act
        var contains = dataCollection.Contains(item);

        // Assert
        Assert.False(contains);
    }

    [Fact]
    public void Contains_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>(),
            KeyPropertySelector = x => x.Id!.Value
        };

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
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = items,
            KeyPropertySelector = x => x.Id!.Value
        };
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
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>(),
            KeyPropertySelector = x => x.Id!.Value
        };

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
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = items,
            KeyPropertySelector = x => x.Id!.Value
        };
        var array = new TestModel[1];

        // Act/Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => dataCollection.CopyTo(array, 0));
    }

    [Fact]
    public void Remove_ValidConditions_ShouldRemoveItem()
    {
        // Arrange
        var item = new TestModel(1, "Item 1");
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel> { item },
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act
        var removed = dataCollection.Remove(item);

        // Assert
        Assert.True(removed);
        Assert.DoesNotContain(item, dataCollection.Items!);
    }

    [Fact]
    public void Remove_WhenItemIsNull_ShouldThrow()
    {
        // Arrange
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel>(),
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act/Assert
        Assert.Throws<ArgumentNullException>(() => dataCollection.Remove(null!));
    }

    [Fact]
    public void Remove_WhenItemIsNotAPartOfCollection_ShouldReturnFalse()
    {
        // Arrange
        var item = new TestModel(1, "Item 1");
        var dataCollection = new FluxJsonDataCollection<TestModel, int>
        {
            Items = new List<TestModel> { new(2, "Item 2") },
            KeyPropertySelector = x => x.Id!.Value
        };

        // Act
        var removed = dataCollection.Remove(item);

        // Assert
        Assert.False(removed);
    }
}