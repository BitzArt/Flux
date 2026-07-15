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

    public IQueryable<TModel> AsQueryable(Func<IQueryable<TModel>, IQueryable<TModel>> enrichQuery)
    {
        TModel[] items;

        lock (_lock)
        {
            items = [.. _items];
        }

        return enrichQuery.Invoke(items.AsQueryable());
    }

    public TModel? Get(object? id) => Get(null, id);

    public TModel? Get(Func<IQueryable<TModel>, IQueryable<TModel>>? enrichQuery, object? id)
    {
        lock (_lock)
        {
            if (_keyMap is null)
            {
                if (id is not null && enrichQuery is null)
                {
                    throw new InvalidOperationException($"Cannot get item by id when no key property is configured for type {typeof(TModel).Name}.");
                }

                var resultingItems = (enrichQuery is not null
                    ? enrichQuery.Invoke(_items.AsQueryable())
                    : _items.AsQueryable()).ToList();

                return resultingItems.Count switch
                {
                    0 => null,
                    > 1 => throw new InvalidOperationException($"Multiple matching items of type {typeof(TModel).Name} were found."),
                    _ => resultingItems.First()
                };
            }

            if (enrichQuery is not null)
            {
                return enrichQuery.Invoke(_items.AsQueryable()).SingleOrDefault();
            }

            return _keyMap.Get(id);
        }
    }

    public TModel Add(TModel item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        lock (_lock)
        {
            _keyMap?.Add(item);
            _items.Add(item);

            return item;
        }
    }

    public TModel Update(object? id, TModel item)
    {
        lock (_lock)
        {
            if (_keyMap is null)
            {
                throw new NotSupportedException($"Cannot update item by id when no key property is configured for type {typeof(TModel).Name}.");
            }

            id ??= _keyMap.GetKey(item);

            TModel? existingItem = Get(null, id)
                ?? throw new InvalidOperationException($"No matching item of type {typeof(TModel).Name} was found.");

            _keyMap.Replace(id, item);
            _items.Remove(existingItem);
            _items.Add(item);

            return item;
        }
    }

    public bool Remove(object? id)
    {
        lock (_lock)
        {
            if (_keyMap is null)
            {
                throw new NotSupportedException($"Cannot remove item by id when no key property is configured for type {typeof(TModel).Name}.");
            }

            if (id is null)
            {
                if (_items.Count == 0)
                {
                    return false;
                }

                if (_items.Count > 1)
                {
                    throw new InvalidOperationException($"Multiple matching items of type {typeof(TModel).Name} were found.");
                }

                var itemToRemove = _items.First();
                _keyMap.Remove(itemToRemove);
                return _items.Remove(itemToRemove);
            }

            TModel? existingItem = Get(null, id);

            if (existingItem is null)
            {
                return false;
            }

            _keyMap.Remove(existingItem);
            _items.Remove(existingItem);
            return true;
        }
    }
}
