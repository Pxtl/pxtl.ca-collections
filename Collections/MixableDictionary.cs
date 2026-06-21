using System.Collections;
using PxtlCa.Collections.FilterInternals;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary allowing filter extensions for custom behavior.
/// </summary>
public class MixableDictionary<TKey, TValue> : VirtualDictionary<TKey, TValue> {
    #region Constructors
    public MixableDictionary() : base() {
        _filterListHead = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
    }

    public MixableDictionary(IEqualityComparer<TKey> comparer)
        : base(comparer) {
        _filterListHead = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
    }

    public MixableDictionary(int capacity)
        : base(capacity) {
        _filterListHead = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
    }

    public MixableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        : base(capacity, comparer) {
        _filterListHead = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
    }

    public MixableDictionary(IDictionary<TKey, TValue> originalDictionary)
        : base(originalDictionary) {
        _filterListHead = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
    }

    public MixableDictionary(IDictionary<TKey, TValue> originalDictionary, bool wrap)
        : base(originalDictionary, wrap) {
        _filterListHead = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
    }

    public MixableDictionary(IDictionary<TKey, TValue> originalDictionary, IEqualityComparer<TKey> comparer)
        : base(originalDictionary, comparer) {
        _filterListHead = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
    }
    #endregion

    #region Properties
    /// <summary>
    /// The head of the filter linked-list for extending collection behavior.
    /// </summary>
    private IDictionaryFilterNode<TKey, TValue> _filterListHead;

    /// <summary>
    /// Gets/Sets all filters applied to this dictionary for extension.
    /// </summary>
    public IEnumerable<IDictionaryFilter<TKey, TValue>> Filters {
        get {
            var filterHolder = _filterListHead;
            while (!(filterHolder is DictionaryFilterTerminalNode<TKey, TValue>)) {
                if (filterHolder is DictionaryFilterLinkNode<TKey, TValue> dictionaryFilterLinkNode) {
                    filterHolder = dictionaryFilterLinkNode.NextFilter;
                    yield return dictionaryFilterLinkNode.Filter;
                } else {
                    break;
                }
            }
        }
        set {
            IDictionaryFilterNode<TKey, TValue> headFilterHolder = new DictionaryFilterTerminalNode<TKey, TValue>(WrappedDictionary);
            foreach (IDictionaryFilter<TKey, TValue> filter in value) {
                IDictionaryFilterNode<TKey, TValue> oldHeadFilterHolder = headFilterHolder;
                headFilterHolder = new DictionaryFilterLinkNode<TKey, TValue>(filter, WrappedDictionary) {
                    NextFilter = oldHeadFilterHolder
                };
            }

            _filterListHead = headFilterHolder;
        }
    }
    #endregion

    #region Dictionary Implementation
    public override TValue this[TKey key] {
        get {
            return _filterListHead[key];
        }
        set {
            _filterListHead[key] = value;
        }
    }

    public override bool Remove(TKey key) {
        return _filterListHead.Remove(key);
    }

    public override void Add(TKey key, TValue value) {
        _filterListHead.Add(key, value);
    }

    public override ICollection<TKey> Keys {
        get { return _filterListHead.Keys; }
    }

    public override ICollection<TValue> Values {
        get { return _filterListHead.Values; }
    }

    public override bool ContainsKey(TKey key) {
        return _filterListHead.ContainsKey(key);
    }

    public override bool Contains(KeyValuePair<TKey, TValue> item) {
        return _filterListHead.Contains(item);
    }
    public override bool Remove(KeyValuePair<TKey, TValue> item) {
        return _filterListHead.Remove(item);
    }
    public override void Clear() {
        _filterListHead.Clear();
    }
    public override int Count {
        get { return _filterListHead.Count; }
    }

    public override void Add(KeyValuePair<TKey, TValue> item) {
        _filterListHead.Add(item.Key, item.Value);
    }
    public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        _filterListHead.CopyTo(array, arrayIndex);
    }

    public override IEnumerator GetObjectEnumerator() {
        return _filterListHead.GetEnumerator();
    }

    /// <summary>
    /// Returns an ordered KeyValuePair enumerator.
    /// </summary>
    public override IEnumerator<KeyValuePair<TKey, TValue>> GetGenericEnumerator() {
        return _filterListHead.GetEnumerator();
    }

    public override bool TryGetValue(TKey key, out TValue val) {
        return _filterListHead.TryGetValue(key, out val);
    }
    #endregion

}

public static class MixableDictFactory {
    public static MixableDictionary<TKey, TValue> Wrap<TKey, TValue>(IDictionary<TKey, TValue> dictionaryToWrap) => new MixableDictionary<TKey, TValue>(dictionaryToWrap, wrap: true);
    public static MixableDictionary<TKey, TValue> WrapInMixableDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionaryToWrap) => Wrap(dictionaryToWrap);
}
