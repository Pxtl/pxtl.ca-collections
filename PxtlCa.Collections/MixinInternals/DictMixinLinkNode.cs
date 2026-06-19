using System.Collections;
using PxtlCa.Collections.Polyfills;

namespace PxtlCa.Collections.MixinInternals;

/// <summary>
/// The body nodes of the linked-list of a <see cref="MixableDictionary{K,
/// V}"/>.  Each node contains a link to its successor.
/// </summary>
internal sealed class DictMixinLinkNode<K, V> : VirtualDict<K, V>, IDictMixinNode<K, V> {
    #region Constructors
    internal DictMixinLinkNode(IDictMixin<K, V> mixin, IDictionary<K, V> mixableDict) : base(mixableDict, wrap: true) {
        ArgumentGuard.ThrowIfNull(mixin, nameof(mixin));
        NextMixin = new DictMixinTerminalNode<K, V>(mixableDict);
        Mixin = mixin!;
    }

    internal DictMixinLinkNode(IDictMixin<K, V> mixin, IDictionary<K, V> mixableDict, IDictMixinNode<K, V> nextMixin) : this(mixin, mixableDict) {
        ArgumentGuard.ThrowIfNull(nextMixin, nameof(nextMixin));
        NextMixin = nextMixin;
    }
    #endregion

    #region Data Members
    public IDictMixin<K, V> Mixin { get; }
    internal IDictMixinNode<K, V> NextMixin { get; set; }
    #endregion

    private void ThrowIfWrappedDictNull() {
        if (WrappedDict == null) {
            throw new NullPropertyException($"Cannot complete operation. '{nameof(WrappedDict)}' is null.  Use '{nameof(SetDict)}' to provide a value.", nameof(WrappedDict));
        }
    }

    public void SetDict(MixableDict<K, V> wrappedDict) {
        ThrowIfWrappedDictNull();
        WrappedDict = wrappedDict;
        NextMixin.SetDict(wrappedDict);
    }

    #region Dictionary Methods

    public override V this[K key] {
        get {
            return Mixin.GetVal(WrappedDict, NextMixin, key);
        }
        set {
            Mixin.SetVal(WrappedDict, NextMixin, key, value);
        }
    }

    public override bool Remove(K key) {
        return Mixin.Remove(WrappedDict, NextMixin, key);
    }
    public override void Add(K key, V value) {
        Mixin.Add(WrappedDict, NextMixin, key, value);
    }

    public override void Add(KeyValuePair<K, V> pair) {
        Add(pair.Key, pair.Value);
    }

    public override ICollection<K> Keys {
        get {
            return Mixin.GetKeys(WrappedDict, NextMixin);
        }
    }
    public override ICollection<V> Values {
        get {
            return Mixin.GetValues(WrappedDict, NextMixin);
        }
    }

    public override bool ContainsKey(K key) {
        return Mixin.ContainsKey(WrappedDict, NextMixin, key);
    }

    public override bool Contains(KeyValuePair<K, V> item) {
        return Mixin.Contains(WrappedDict, NextMixin, item);
    }

    public override bool Remove(KeyValuePair<K, V> item) {
        return Mixin.Remove(WrappedDict!, NextMixin, item);
    }

    public override void Clear() {
        Mixin.Clear(WrappedDict!, NextMixin);
    }

    public override int Count {
        get {
            return Mixin.GetCount(WrappedDict!, NextMixin);
        }
    }

    public override void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
        Mixin.CopyTo(WrappedDict!, NextMixin, array, arrayIndex);
    }

    public override IEnumerator GetObjectEnumerator() {
        return Mixin.GetObjectEnumerator(WrappedDict!, NextMixin);
    }

    public override IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
        return Mixin.GetEnumerator(WrappedDict!, NextMixin);
    }

    public override bool TryGetValue(K key, out V val) {
        return Mixin.TryGetValue(WrappedDict!, NextMixin, key, out val);
    }

    public override bool IsReadOnly => false;
    #endregion
}