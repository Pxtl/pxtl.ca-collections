using System.Collections;
using PxtlCa.Collections.Polyfills;

namespace PxtlCa.Collections;

public class VirtualDict<K, V> : VirtualWrappedDict<IDictionary<K, V>, K, V> {
    #region Dictionary Constructors
    // We provide all the same constructors that the generic
    // Dictionary provides.

    public VirtualDict() : base(new Dictionary<K, V>()) { }

    public VirtualDict(IEqualityComparer<K> comparer) : base(new Dictionary<K, V>(comparer)) { }

    public VirtualDict(int capacity) : base(new Dictionary<K, V>(capacity)) { }

    public VirtualDict(int capacity, IEqualityComparer<K> comparer) : base(new Dictionary<K, V>(capacity, comparer)) { }

    public VirtualDict(IDictionary<K, V> originalDictionary) : this(originalDictionary, false) { }

    public VirtualDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer) : base(new Dictionary<K, V>(originalDictionary, comparer)) { }
    #endregion

    #region Wrapping Constructors
    public VirtualDict(IDictionary<K, V> originalDictionary, bool wrap)
    : base(wrap ? originalDictionary : new Dictionary<K, V>(originalDictionary)) { }
    #endregion
}

public class VirtualWrappedDict<D, K, V> : IDictionary<K, V>
where D : IDictionary<K, V> {
    /// <summary>
    /// Provides the underlying collection for this wrapper.
    /// </summary>
    // to this.

    protected D WrappedDict { get; set; }

    #region Wrapping Constructors
    public VirtualWrappedDict(D originalDictionary) {
        ArgumentGuard.ThrowIfNull(originalDictionary, nameof(originalDictionary));
        WrappedDict = originalDictionary;
    }
    #endregion

    public virtual V this[K key] {
        get {
            return WrappedDict[key];
        }
        set {
            WrappedDict[key] = value;
        }
    }

    // Delegating implementations of all other methods.

    public virtual bool Remove(K key) {
        return WrappedDict.Remove(key);
    }
    public virtual void Add(K key, V value) {
        WrappedDict.Add(key, value);
    }
    public virtual ICollection<K> Keys {
        get { return WrappedDict.Keys; }
    }

    public virtual ICollection<V> Values {
        get { return WrappedDict.Values; }
    }

    public virtual bool ContainsKey(K key) {
        return WrappedDict.ContainsKey(key);
    }

    public virtual bool IsReadOnly {
        get { return WrappedDict.IsReadOnly; }
    }

    public virtual bool Contains(KeyValuePair<K, V> item) {
        return WrappedDict.Contains(item);
    }
    public virtual bool Remove(KeyValuePair<K, V> item) {
        return WrappedDict.Remove(item);
    }
    public virtual void Clear() {
        WrappedDict.Clear();
    }
    public virtual int Count {
        get { return WrappedDict.Count; }
    }

    public virtual void Add(KeyValuePair<K, V> item) {
        WrappedDict.Add(item);
    }
    public virtual void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
        WrappedDict.CopyTo(array, arrayIndex);
    }

    public virtual IEnumerator GetObjectEnumerator() {
        return WrappedDict.GetEnumerator();
    }

    IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        return GetObjectEnumerator();
    }

    public virtual IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
        return WrappedDict.GetEnumerator();
    }

    IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator() {
        return GetGenericEnumerator();
    }

    public virtual bool TryGetValue(K key, out V val) {
        return WrappedDict.TryGetValue(key, out val);
    }
}

public static class VirtualDictFactory {
    public static VirtualDict<K, V> Wrap<K, V>(IDictionary<K, V> dictionaryToWrap) => new VirtualDict<K, V>(dictionaryToWrap, wrap: true);
    public static VirtualDict<K, V> WrapInVirtualDict<K, V>(this IDictionary<K, V> dictionaryToWrap) => Wrap(dictionaryToWrap);
}