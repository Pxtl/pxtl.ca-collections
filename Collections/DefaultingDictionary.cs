using PxtlCa.Collections.DictionaryFilters;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary that provides default values for missing keys.
/// </summary>
public class DefaultingDictionary<TKey, TValue> : MixableDictionary<TKey, TValue> {
    public DefaultingDictionary() : base() {
        Filters = [_delegateDefaultingDictionaryFilter];
    }

    public DefaultingDictionary(IEqualityComparer<TKey> comparer)
        : base(comparer) { }

    public DefaultingDictionary(int capacity)
        : base(capacity) { }

    public DefaultingDictionary(int capacity, IEqualityComparer<TKey> comparer)
        : base(capacity, comparer) { }

    public DefaultingDictionary(IDictionary<TKey, TValue> originalDictionary)
        : base(originalDictionary) { }

    protected DefaultingDictionary(IDictionary<TKey, TValue> originalDictionary, bool wrap)
        : base(originalDictionary, wrap) { }

    public DefaultingDictionary(IDictionary<TKey, TValue> originalDictionary, IEqualityComparer<TKey> comparer)
        : base(originalDictionary, comparer) { }

    private DelegateDefaultingDictionaryFilter<TKey, TValue> _delegateDefaultingDictionaryFilter = new();

    public ValueConstructor<TKey, TValue>? ValueConstructionHandler {
        get => _delegateDefaultingDictionaryFilter.ValueConstructionHandler;
        set { _delegateDefaultingDictionaryFilter.ValueConstructionHandler = value; }
    }
}