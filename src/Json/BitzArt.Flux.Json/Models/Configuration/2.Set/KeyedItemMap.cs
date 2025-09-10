namespace BitzArt.Flux.Json;

internal class KeyedItemMap<TModel, TKey> : KeyedItemMap<TModel>
{
    private readonly Func<TModel, TKey?> _keyPropertySelector;
    private readonly Dictionary<object, TModel> _keyedItems;
    private readonly List<TModel> _keylessItems;

    public KeyedItemMap(Func<TModel, TKey?> keyPropertySelector, IEnumerable<TModel> items)
    {
        _keyPropertySelector = keyPropertySelector;

        _keyedItems = [];
        _keylessItems = [];

        foreach (var item in items)
        {
            PersistItem(item);
        }
    }

    private void PersistItem(TModel item)
    {
        var key = _keyPropertySelector.Invoke(item);

        if (key is null)
        {
            _keylessItems.Add(item);
            return;
        }

        if (_keyedItems.ContainsKey(key))
        {
            throw new InvalidOperationException($"Duplicate key '{key}' found for type '{typeof(TModel).Name}'.");
        }

        _keyedItems[key] = item;
    }

    public override object? GetKey(TModel item) => _keyPropertySelector.Invoke(item);

    public override TModel Get(object? id)
    {
        if (id is not TKey key)
        {
            throw new ArgumentException($"Invalid key type. Expected '{typeof(TKey).Name}', but got '{id?.GetType().Name ?? "null"}'.", nameof(id));
        }

        if (!_keyedItems.TryGetValue(key, out var item))
        {
            throw new InvalidOperationException($"{typeof(TModel).Name} with key {id} was not found");
        }

        return item;
    }

    public override void Add(TModel item)
    {
        PersistItem(item);
    }

    public override bool Remove(TModel item)
    {
        var key = _keyPropertySelector.Invoke(item);

        if (key is null)
        {
            return _keylessItems.Remove(item);
        }

        return _keyedItems.Remove(key);
    }
}

internal abstract class KeyedItemMap<TModel>
{
    public abstract object? GetKey(TModel item);
    public abstract TModel Get(object? id);
    public abstract void Add(TModel item);
    public abstract bool Remove(TModel item);
}