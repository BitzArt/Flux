using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BitzArt.Flux.Json;

internal class FluxJsonSetOptions<TModel, TKey> : IFluxJsonSetOptions<TModel>, IEnumerable<TModel>
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
            if (value is null) throw new ArgumentNullException(nameof(Items), "Items collection cannot be null.");
            _items = value.ToList();
            UpdateKeyedItems();
        }
    }

    private Expression<Func<TModel, TKey>>? _keyPropertyExpression;
    internal Expression<Func<TModel, TKey>>? KeyPropertyExpression
    {
        get => _keyPropertyExpression;
        set
        {
            if (value is null) throw new ArgumentNullException(nameof(KeyPropertyExpression), "KeyPropertyExpression cannot be null.");
            _keyPropertyExpression = value;
            UpdateKeyedItems();
        }
    }

    Expression<Func<TModel, object>>? IFluxJsonSetOptions<TModel>.KeyPropertyExpression
    {
        get
        {
            if (KeyPropertyExpression is null) return null;

            var convert = Expression.Convert(KeyPropertyExpression.Body, typeof(object));
            return Expression.Lambda<Func<TModel, object>>(convert, KeyPropertyExpression.Parameters);
        }

        set
        {
            if (value is null)
            {
                KeyPropertyExpression = null;
            }
            else
            {
                var convert = Expression.Convert(value.Body, typeof(TKey));
                KeyPropertyExpression = Expression.Lambda<Func<TModel, TKey>>(convert, value.Parameters);
            }
        }
    }

    private void UpdateKeyedItems()
    {
        if (_items is null || _keyPropertyExpression is null) return;

        var keySelector = _keyPropertyExpression.Compile();

        KeyedItems = _items.ToDictionary(
            item => keySelector.Invoke(item)!,
            item => item);
    }

    public IEnumerator<TModel> GetEnumerator()
    {
        if (_items is null) throw new InvalidOperationException("Items collection is not initialized.");

        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}