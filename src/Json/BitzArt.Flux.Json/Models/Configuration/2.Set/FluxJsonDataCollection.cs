using System.Collections;
using System.Linq.Expressions;

namespace BitzArt.Flux.Json;

internal class FluxJsonDataCollection<TModel> : ICollection<TModel>
    where TModel : class
{
    public FluxJsonDataCollection(ICollection<TModel> items)
    {
        Items = items;
    }

    internal Dictionary<object, TModel>? KeyedItems { get; private set; }

    private ICollection<TModel> _items = null!;
    internal ICollection<TModel> Items
    {
        get
        {
            if (_items is null)
                throw new InvalidOperationException("Items collection is not initialized. Cannot retrieve items.");

            // ToList() is used to create a snapshot of the items collection.
            // This ensures that any modifications made to the returned list do not affect the original collection.
            var result = _items.ToList();

            return result;
        }

        private set
        {
            _items = value ?? throw new ArgumentNullException(nameof(value), "Items collection cannot be null.");
            UpdateKeyedItems();
        }
    }

    private Func<TModel, object?>? _keyPropertySelector;
    internal Func<TModel, object?>? KeyPropertySelector
    {
        private get => _keyPropertySelector;
        set
        {
            _keyPropertySelector = value;
            UpdateKeyedItems();
        }
    }

    private void UpdateKeyedItems()
    {
        if (_items is null || _keyPropertySelector is null)
        {
            KeyedItems = null;
            return;
        }

        KeyedItems = _items.ToDictionary(
            item => _keyPropertySelector.Invoke(item)!,
            item => item);
    }

    public void Add(TModel item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (_items is null)
            throw new InvalidOperationException("Items collection is not initialized. Cannot add item.");

        _items.Add(item);
        UpdateKeyedItems();
    }

    public void Clear()
    {
        if (_items is null) return;

        _items.Clear();
        UpdateKeyedItems();
    }

    public bool Contains(TModel item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        return _items is not null && _items.Contains(item);
    }

    public void CopyTo(TModel[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array, nameof(array));

        if (_items is null) return;

        var subArrayLength = array.Length - arrayIndex;
        if (arrayIndex < 0 || arrayIndex >= array.Length || subArrayLength < _items.Count)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Array index is out of range.");

        _items.CopyTo(array, arrayIndex);
    }

    public bool Remove(TModel item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (_items is null) return false;

        var removed = _items.Remove(item);
        if (removed)
        {
            UpdateKeyedItems();
        }
        return removed;
    }

    public int Count => _items.Count;

    public bool IsReadOnly => _items.IsReadOnly;

    public IEnumerator<TModel> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal TModel GetById(object id)
    {
        if (KeyedItems is null)
            throw new InvalidOperationException("Keyed items are not initialized. Cannot retrieve item by ID.");

        if (!KeyedItems.TryGetValue(id, out var item))
            throw new FluxItemNotFoundException<TModel>(id);

        return item;
    }
}