using System.Collections.ObjectModel;

namespace PhlegmaticOne.WPF.Core.Infrastructure.ObservableDictionary;

public class ObservableDictionary<TKey, TValue> : ObservableCollection<ObservableKeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
{
    public bool IsReadOnly => false;
    public ICollection<TKey> Keys => AsCollection().Select(x => x.Key).ToList();
    public ICollection<TValue> Values => AsCollection().Select(x => x.Value).ToList();
    public TValue this[TKey key]
    {
        get
        {
            if (!TryGetValue(key, out var result))
            {
                throw new ArgumentException("Key not found");
            }
            return result;
        }
        set
        {
            if (ContainsKey(key))
            {
                GetKvpByTheKey(key)!.Value = value;
            }
            else
            {
                Add(key, value);
            }
        }
    }

    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key) == false)
        {
            Add(new ObservableKeyValuePair<TKey, TValue>(key, value));
        }
        else
        {
            throw new ArgumentException("The dictionary already contains the key");
        }
    }
    public bool ContainsKey(TKey key) => AsCollection().Any(x => Equals(key, x.Key));
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        var valuePair = GetKvpByTheKey(item.Key);
        return valuePair is not null && Equals(valuePair.Value, item.Value);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        var valuePair = GetKvpByTheKey(item.Key);
        if (valuePair is null)
        {
            return false;
        }
        return Equals(valuePair.Value, item.Value) && AsCollection().Remove(valuePair);
    }
    public bool Remove(TKey key)
    {
        var remove = AsCollection().Single(x => Equals(x.Key, key));
        return AsCollection().Remove(remove);
    }


    public bool TryGetValue(TKey key, out TValue value)
    {
        var retrieved = GetKvpByTheKey(key);
        if (retrieved is not null)
        {
            value = retrieved.Value;
            return true;
        }

        value = default;
        return false;
    }
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { }

    public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => AsCollection().Select(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value)).GetEnumerator();

    private ObservableCollection<ObservableKeyValuePair<TKey, TValue>> AsCollection() => this;
    private bool Equals(TKey a, TKey b) => EqualityComparer<TKey>.Default.Equals(a, b);
    private ObservableKeyValuePair<TKey, TValue>? GetKvpByTheKey(TKey key) => AsCollection().FirstOrDefault(i => Equals(i.Key, key));
}