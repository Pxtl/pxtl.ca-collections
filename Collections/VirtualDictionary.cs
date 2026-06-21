using System.Collections;
using PxtlCa.Collections.Polyfills;

namespace PxtlCa.Collections;

public class VirtualDictionary<TKey, TValue> : VirtualWrappedDictionary<IDictionary<TKey, TValue>, TKey, TValue> {
    #region Dictionary Constructors
    // We provide all the same constructors that the generic
    // Dictionary provides.

    public VirtualDictionary() : base(new Dictionary<TKey, TValue>()) { }

    public VirtualDictionary(IEqualityComparer<TKey> comparer) : base(new Dictionary<TKey, TValue>(comparer)) { }

    public VirtualDictionary(int capacity) : base(new Dictionary<TKey, TValue>(capacity)) { }

    public VirtualDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(new Dictionary<TKey, TValue>(capacity, comparer)) { }

    public VirtualDictionary(IDictionary<TKey, TValue> originalDictionary) : this(originalDictionary, false) { }

    public VirtualDictionary(IDictionary<TKey, TValue> originalDictionary, IEqualityComparer<TKey> comparer) : base(new Dictionary<TKey, TValue>(originalDictionary, comparer)) { }
    #endregion

    #region Wrapping Constructors
    public VirtualDictionary(IDictionary<TKey, TValue> originalDictionary, bool wrap)
    : base(wrap ? originalDictionary : new Dictionary<TKey, TValue>(originalDictionary)) { }
    #endregion
}

public class VirtualWrappedDictionary<TDictionary, TKey, TValue> : IDictionary<TKey, TValue>
where TDictionary : IDictionary<TKey, TValue> {
    /// <summary>
    /// Provides the underlying collection for this wrapper.
    /// </summary>
    // to this.

    protected TDictionary WrappedDictionary { get; set; }

    #region Wrapping Constructors
    public VirtualWrappedDictionary(TDictionary originalDictionary) {
        ArgumentGuard.ThrowIfNull(originalDictionary, nameof(originalDictionary));
        WrappedDictionary = originalDictionary;
    }
    #endregion

    public virtual TValue this[TKey key] {
        get {
            return WrappedDictionary[key];
        }
        set {
            WrappedDictionary[key] = value;
        }
    }

    // Delegating implementations of all other methods.

    public virtual bool Remove(TKey key) {
        return WrappedDictionary.Remove(key);
    }
    public virtual void Add(TKey key, TValue value) {
        WrappedDictionary.Add(key, value);
    }
    public virtual ICollection<TKey> Keys {
        get { return WrappedDictionary.Keys; }
    }

    public virtual ICollection<TValue> Values {
        get { return WrappedDictionary.Values; }
    }

    public virtual bool ContainsKey(TKey key) {
        return WrappedDictionary.ContainsKey(key);
    }

    public virtual bool IsReadOnly {
        get { return WrappedDictionary.IsReadOnly; }
    }

    public virtual bool Contains(KeyValuePair<TKey, TValue> item) {
        return WrappedDictionary.Contains(item);
    }
    public virtual bool Remove(KeyValuePair<TKey, TValue> item) {
        return WrappedDictionary.Remove(item);
    }
    public virtual void Clear() {
        WrappedDictionary.Clear();
    }
    public virtual int Count {
        get { return WrappedDictionary.Count; }
    }

    public virtual void Add(KeyValuePair<TKey, TValue> item) {
        WrappedDictionary.Add(item);
    }
    public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        WrappedDictionary.CopyTo(array, arrayIndex);
    }

    public virtual IEnumerator GetObjectEnumerator() {
        return WrappedDictionary.GetEnumerator();
    }

    IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        return GetObjectEnumerator();
    }

    public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetGenericEnumerator() {
        return WrappedDictionary.GetEnumerator();
    }

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
        return GetGenericEnumerator();
    }

    public virtual bool TryGetValue(TKey key, out TValue val) {
        return WrappedDictionary.TryGetValue(key, out val);
    }
}

public static class VirtualDictFactory {
    public static VirtualDictionary<TKey, TValue> Wrap<TKey, TValue>(IDictionary<TKey, TValue> dictionaryToWrap) => new VirtualDictionary<TKey, TValue>(dictionaryToWrap, wrap: true);
    public static VirtualDictionary<TKey, TValue> WrapInVirtualDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionaryToWrap) => Wrap(dictionaryToWrap);
}
