using System.Collections;

namespace BitzArt.Flux.Json;

internal class FluxJsonDataCollection<TModel> : ICollection<TModel>
    where TModel : class
{
    internal Dictionary<object, TModel>? KeyedItems { get; private set; }

    private ICollection<TModel> _items;

    public void Set(ICollection<TModel> items)
    {
        _items = items;
        UpdateKeyedItems();
    }

    private Func<TModel, object>? _keyPropertySelector;
    public Func<TModel, object>? KeyPropertySelector
    {
        get => _keyPropertySelector;
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

        if (Items is null)
            throw new InvalidOperationException("Items collection is not initialized. Cannot add item.");

        Items.Add(item);
        UpdateKeyedItems();
    }

    public void Clear()
    {
        if (Items is null) return;

        Items.Clear();
        UpdateKeyedItems();
    }

    public bool Contains(TModel item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        return Items is not null && Items.Contains(item);
    }

    public void CopyTo(TModel[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array, nameof(array));

        if (Items is null) return;

        var subArrayLength = array.Length - arrayIndex;
        if (arrayIndex < 0 || arrayIndex >= array.Length || subArrayLength < Items.Count) 
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Array index is out of range.");

        Items.CopyTo(array, arrayIndex);
    }

    public bool Remove(TModel item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (Items is null) return false;

        var removed = Items.Remove(item);
        if (removed)
        {
            UpdateKeyedItems();
        }
        return removed;
    }

    public int Count => _items.Count;

    public bool IsReadOnly => throw new NotImplementedException();

    public IEnumerator<TModel> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}