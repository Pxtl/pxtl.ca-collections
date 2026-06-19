namespace PxtlCa.Collections;

using PxtlCa.Collections.DictionaryFilters;

public class AutoConstructingDictionary<TKey, TValue> : MixableDictionary<TKey, TValue>
where TValue : new() {
    public AutoConstructingDictionary() : base() {
        Initialize();
    }

    public AutoConstructingDictionary(IEqualityComparer<TKey> comparer)
    : base(comparer) {
        Initialize();
    }

    public AutoConstructingDictionary(int capacity)
    : base(capacity) {
        Initialize();
    }

    public AutoConstructingDictionary(int capacity, IEqualityComparer<TKey> comparer)
    : base(capacity, comparer) {
        Initialize();
    }

    public AutoConstructingDictionary(IDictionary<TKey, TValue> originalDictionary)
    : base(originalDictionary) {
        Initialize();
    }

    protected AutoConstructingDictionary(IDictionary<TKey, TValue> originalDictionary, bool wrap)
    : base(originalDictionary, wrap) {
        Initialize();
    }

    public AutoConstructingDictionary(IDictionary<TKey, TValue> originalDictionary, IEqualityComparer<TKey> comparer)
    : base(originalDictionary, comparer) {
        Initialize();
    }

    private void Initialize() {
        Filters = new[] { new AutoConstructingDictionaryFilter<TKey, TValue>() };
    }
}
