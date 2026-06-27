using System.Collections;
using PxtlCa.Collections.Polyfills;

namespace PxtlCa.Collections.FilterInternals;

/// <summary>
/// The body nodes of the linked-list of a <see cref="FilteredDictionary{TKey,
/// TValue}"/>.  Each node contains a link to its successor.
/// </summary>
internal sealed class DictionaryFilterLinkNode<TKey, TValue> : VirtualDictionary<TKey, TValue>, IDictionaryFilterNode<TKey, TValue> {
    #region Constructors
    internal DictionaryFilterLinkNode(IDictionaryFilter<TKey, TValue> filter, IDictionary<TKey, TValue> filteredDictionary) : base(filteredDictionary, wrap: true) {
        ArgumentGuard.ThrowIfNull(filter, nameof(filter));
        NextFilter = new DictionaryFilterTerminalNode<TKey, TValue>(filteredDictionary);
        Filter = filter!;
    }

    internal DictionaryFilterLinkNode(IDictionaryFilter<TKey, TValue> filter, IDictionary<TKey, TValue> filteredDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) : this(filter, filteredDictionary) {
        ArgumentGuard.ThrowIfNull(nextFilter, nameof(nextFilter));
        NextFilter = nextFilter;
    }
    #endregion

    #region Data Members
    public IDictionaryFilter<TKey, TValue> Filter { get; }
    internal IDictionaryFilterNode<TKey, TValue> NextFilter { get; set; }
    #endregion

    private void ThrowIfWrappedDictNull() {
        if (WrappedDictionary == null) {
            throw new NullPropertyException($"Cannot complete operation. '{nameof(WrappedDictionary)}' is null.  Use '{nameof(SetDictionary)}' to provide a value.", nameof(WrappedDictionary));
        }
    }

    public void SetDictionary(FilteredDictionary<TKey, TValue> wrappedDictionary) {
        ThrowIfWrappedDictNull();
        WrappedDictionary = wrappedDictionary;
        NextFilter.SetDictionary(wrappedDictionary);
    }

    #region Dictionary Methods

    public override TValue this[TKey key] {
        get {
            return Filter.GetVal(WrappedDictionary, NextFilter, key);
        }
        set {
            Filter.SetVal(WrappedDictionary, NextFilter, key, value);
        }
    }

    public override bool Remove(TKey key) {
        return Filter.Remove(WrappedDictionary, NextFilter, key);
    }
    public override void Add(TKey key, TValue value) {
        Filter.Add(WrappedDictionary, NextFilter, key, value);
    }

    public override void Add(KeyValuePair<TKey, TValue> pair) {
        Add(pair.Key, pair.Value);
    }

    public override ICollection<TKey> Keys {
        get {
            return Filter.GetKeys(WrappedDictionary, NextFilter);
        }
    }
    public override ICollection<TValue> Values {
        get {
            return Filter.GetValues(WrappedDictionary, NextFilter);
        }
    }

    public override bool ContainsKey(TKey key) {
        return Filter.ContainsKey(WrappedDictionary, NextFilter, key);
    }

    public override bool Contains(KeyValuePair<TKey, TValue> item) {
        return Filter.Contains(WrappedDictionary, NextFilter, item);
    }

    public override bool Remove(KeyValuePair<TKey, TValue> item) {
        return Filter.Remove(WrappedDictionary!, NextFilter, item);
    }

    public override void Clear() {
        Filter.Clear(WrappedDictionary!, NextFilter);
    }

    public override int Count {
        get {
            return Filter.GetCount(WrappedDictionary!, NextFilter);
        }
    }

    public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        Filter.CopyTo(WrappedDictionary!, NextFilter, array, arrayIndex);
    }

    public override IEnumerator GetObjectEnumerator() {
        return Filter.GetObjectEnumerator(WrappedDictionary!, NextFilter);
    }

    public override IEnumerator<KeyValuePair<TKey, TValue>> GetGenericEnumerator() {
        return Filter.GetEnumerator(WrappedDictionary!, NextFilter);
    }

    public override bool TryGetValue(TKey key, out TValue val) {
        return Filter.TryGetValue(WrappedDictionary!, NextFilter, key, out val);
    }

    public override bool IsReadOnly => false;
    #endregion
}
