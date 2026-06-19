using System.Collections;

namespace PxtlCa.Collections.MixinInternals;

/// <summary>
/// The terminal node of the linked-list of a <see cref="MixableDictionary{K,
/// V}"/>.  This node implements its own mixin that directly goes straight to
/// the underlying wrapped dictionary.
/// </summary>
internal sealed class DictMixinTerminalNode<K, V>
: VirtualDict<K, V>, /*IDictMixin<K, V>,*/ IDictMixinNode<K, V> {
    #region IDictMixinHolder Members

    public void SetDict(MixableDict<K, V> baseDict) {
        WrappedDict = baseDict;
    }
    #endregion

    #region Constuctors
    internal DictMixinTerminalNode(IDictionary<K, V> baseDict) : base(baseDict, wrap: true) { }
    #endregion

    #region IDictMixin Members
    public bool Remove(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key) {
        return this.Remove(key);
    }
    public void Add(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key, V value) {
        this.Add(key, value);
    }
    public ICollection<K> GetKeys(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin) {
        return this.Keys;
    }
    public ICollection<V> GetValues(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin) {
        return this.Values;
    }

    public bool ContainsKey(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key) {
        return this.ContainsKey(key);
    }

    public bool Contains(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, KeyValuePair<K, V> item) {
        return this.Contains(item);
    }

    public bool Remove(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, KeyValuePair<K, V> item) {
        return this.Remove(item);
    }

    public void Clear(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin) {
        this.Clear();
    }

    public int GetCount(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin) {
        return this.Count;
    }

    public void CopyTo(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, KeyValuePair<K, V>[] array, int arrayIndex) {
        this.CopyTo(array, arrayIndex);
    }

    public IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin) {
        return (this as IEnumerable).GetEnumerator();
    }

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin) {
        return (this as IEnumerable<KeyValuePair<K, V>>).GetEnumerator();
    }

    public bool TryGetValue(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key, out V val) {
        return this.TryGetValue(key, out val);
    }

    public V GetVal(IDictionary<K, V> thisDict, IDictMixinNode<K, V> NextMixin, K key) {
        return this[key];
    }

    public void SetVal(IDictionary<K, V> thisDict, IDictMixinNode<K, V> NextMixin, K key, V value) {
        this[key] = value;
    }

    #endregion
}