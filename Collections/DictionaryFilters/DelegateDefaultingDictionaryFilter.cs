namespace PxtlCa.Collections.DictionaryFilters;

public class DelegateDefaultingDictionaryFilter<TKey, TValue> : DefaultingDictionaryFilter<TKey, TValue> {
    public DelegateDefaultingDictionaryFilter() : base() { }

    public DelegateDefaultingDictionaryFilter(ValueConstructor<TKey, TValue>? constructor)
    : this() {
        ValueConstructionHandler = constructor;
    }

    /// <exception cref="NullPropertyException">
    /// Throws if ValueConstructionHandler is not set.
    /// </exception>
    protected override bool TryGetDefaultValue(TKey key, out TValue val) {
        if (ValueConstructionHandler == null) {
            val = default!;
            return false;
        } else {
            val = ValueConstructionHandler(key);
            return true;
        }
    }

    #region ValueConstructor system to construct defaults.
    /// <summary>
    /// The default constructor to make dictionary objects if a value is not
    /// already present in the dictionary. 
    /// </summary>
    public ValueConstructor<TKey, TValue>? ValueConstructionHandler { get; set; }
    #endregion
}

public delegate TValue ValueConstructor<TKey, TValue>(TKey key);
