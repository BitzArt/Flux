using System.Collections;

namespace BitzArt.Flux.Json;

internal class FluxJsonDataCollection<TModel, TKey> : IFluxJsonDataCollection<TModel>, IEnumerable<TModel>, ICollection<TModel>
    where TModel : class
    where TKey : notnull
{
    internal Dictionary<TKey, TModel>? KeyedItems { get; private set; }

    private ICollection<TModel>? _items;
    public ICollection<TModel>? Items
    {
        get => _items;
        set
        {
            _items = value;
            UpdateKeyedItems();
        }
    }

    private Func<TModel, TKey>? _keyPropertySelector;
    internal Func<TModel, TKey>? KeyPropertySelector
    {
        get => _keyPropertySelector;
        set
        {
            _keyPropertySelector = value;
            UpdateKeyedItems();
        }
    }

    Func<TModel, object>? IFluxJsonDataCollection<TModel>.KeyPropertySelector
    {
        get
        {
            if (KeyPropertySelector is null) return null;

            return item => KeyPropertySelector(item);
        }
        set
        {
            if (value is null)
            {
                KeyPropertySelector = null;
            }
            else
            {
                KeyPropertySelector = item =>
                {
                    var key = value(item);

                    if (key is not TKey typedKey)
                        throw new InvalidCastException($"Cannot cast value of type {value?.GetType()} to {typeof(TKey)}");

                    return typedKey;
                };
            }
        }
    }

    public int Count => _items?.Count ?? 0;
    public bool IsReadOnly => Items is not null && Items.IsReadOnly;

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

    public IEnumerator<TModel> GetEnumerator()
    {
        if (_items is null) throw new InvalidOperationException("Items collection is not initialized.");

        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

        if (array is null) throw new ArgumentNullException(nameof(array), "Array cannot be null.");

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
}