using System.Collections;

namespace PxtlCa.Collections.FilterInternals;

/// <summary>
/// The terminal node of the linked-list of a <see cref="FilteredDictionary{TKey,
/// TValue}"/>.  This node implements its own filter that directly goes straight to
/// the underlying wrapped dictionary.
/// </summary>
internal sealed class DictionaryFilterTerminalNode<TKey, TValue>
: VirtualDictionary<TKey, TValue>, IDictionaryFilterNode<TKey, TValue> {
    #region IDictionaryFilterHolder Members

    public void SetDictionary(FilteredDictionary<TKey, TValue> baseDictionary) {
        WrappedDictionary = baseDictionary;
    }
    #endregion

    #region Constuctors
    internal DictionaryFilterTerminalNode(IDictionary<TKey, TValue> baseDictionary) : base(baseDictionary, wrap: true) { }
    #endregion

    #region IDictionaryFilter Members
    public bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key) {
        return this.Remove(key);
    }
    public void Add(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, TValue value) {
        this.Add(key, value);
    }
    public ICollection<TKey> GetKeys(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return this.Keys;
    }
    public ICollection<TValue> GetValues(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return this.Values;
    }

    public bool ContainsKey(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key) {
        return this.ContainsKey(key);
    }

    public bool Contains(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue> item) {
        return this.Contains(item);
    }

    public bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue> item) {
        return this.Remove(item);
    }

    public void Clear(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        this.Clear();
    }

    public int GetCount(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return this.Count;
    }

    public void CopyTo(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        this.CopyTo(array, arrayIndex);
    }

    public IEnumerator GetObjectEnumerator(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return (this as IEnumerable).GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return (this as IEnumerable<KeyValuePair<TKey, TValue>>).GetEnumerator();
    }

    public bool TryGetValue(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, out TValue val) {
        return this.TryGetValue(key, out val);
    }

    public TValue GetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> NextFilter, TKey key) {
        return this[key];
    }

    public void SetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> NextFilter, TKey key, TValue value) {
        this[key] = value;
    }

    #endregion
}
