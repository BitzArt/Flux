namespace BitzArt.Flux.Json;

internal class FluxJsonDataCollection<TModel>
    where TModel : class
{
    private readonly Lock _lock;
    private readonly List<TModel> _items;
    private KeyedItemMap<TModel>? _keyMap;

    public FluxJsonDataCollection(IEnumerable<TModel> items)
    {
        _items = [.. items];
        _lock = new();
    }

    public void SetKeyPropertySelector<TKey>(Func<TModel, TKey> value)
    {
        _keyMap = new KeyedItemMap<TModel, TKey>(value, _items);
    }

    public IReadOnlyCollection<TModel> GetAll() => _items.AsReadOnly();

    public TModel Get(object? id)
    {
        lock (_lock)
        {
            if (_keyMap is null)
            {
                if (id is not null)
                {
                    throw new InvalidOperationException($"Cannot get item by id when no key property is configured for type {typeof(TModel).Name}.");
                }

                var count = _items.Count;

                if (count == 0)
                {
                    throw new InvalidOperationException($"No items of type {typeof(TModel).Name} are available.");
                }

                if (count > 1)
                {
                    throw new InvalidOperationException($"Multiple items of type {typeof(TModel).Name} are available.");
                }

                return _items[0];
            }

            return _keyMap.Get(id);
        }
    }

    public TModel Add(TModel item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        lock (_lock)
        {
            _items.Add(item);
            _keyMap?.Add(item);
        }

        return item;
    }

    public TModel Update(object? id, TModel item)
    {
        if (_keyMap is null)
        {
            throw new NotSupportedException($"Cannot update item by id when no key property is configured for type {typeof(TModel).Name}.");
        }

        id ??= _keyMap.GetKey(item);
        TModel existingItem = Get(id);

        Remove(existingItem);
        Add(item);

        return item;
    }

    public bool Remove(object? id)
    {
        lock (_lock)
        {
            if (_keyMap is null)
            {
                throw new NotSupportedException($"Cannot remove item by id when no key property is configured for type {typeof(TModel).Name}.");
            }

            TModel existingItem = _keyMap.Get(id);

            var removed = _items.Remove(existingItem);

            if (!removed)
            {
                return false;
            }

            _keyMap?.Remove(existingItem);

            return true;
        }
    }
}
