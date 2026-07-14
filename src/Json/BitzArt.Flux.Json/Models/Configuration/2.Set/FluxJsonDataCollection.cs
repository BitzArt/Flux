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
        return enrichQuery.Invoke(_items.AsQueryable());
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

                var resultingItems = enrichQuery is not null
                    ? enrichQuery.Invoke(_items.AsQueryable())
                    : _items.AsQueryable();

                var count = resultingItems.Count();

                if (count == 0)
                {
                    return null;
                }

                if (count > 1)
                {
                    throw new InvalidOperationException($"Multiple matching items of type {typeof(TModel).Name} were found.");
                }

                return resultingItems.First();
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
        
        TModel? existingItem = Get(null, id) 
            ?? throw new InvalidOperationException($"No matching item of type {typeof(TModel).Name} was found.");
        
        _items.Remove(existingItem);
        Add(item);

        return item;
    }

    public bool Remove(object? id)
    {
        lock (_lock)
        {
            if (_keyMap is null)
            {
                throw new NotSupportedException($"Cannot update item by id when no key property is configured for type {typeof(TModel).Name}.");
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

                return _items.Remove(_items.First());
            }

            TModel? existingItem = Get(null, id);

            if (existingItem is null)
            {
                return false;
            }

            _items.Remove(existingItem);
            return true;
        }
    }
}
