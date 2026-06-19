using System.Collections;
using PxtlCa.Collections.MixinInternals;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary allowing mixin extensions for custom behavior.
/// </summary>
public class MixableDict<K, V> : VirtualDict<K, V> {
    #region Constructors
    public MixableDict() : base() {
        _mixinListHead = new DictMixinTerminalNode<K, V>(WrappedDict);
    }

    public MixableDict(IEqualityComparer<K> comparer)
        : base(comparer) {
        _mixinListHead = new DictMixinTerminalNode<K, V>(WrappedDict);
    }

    public MixableDict(int capacity)
        : base(capacity) {
        _mixinListHead = new DictMixinTerminalNode<K, V>(WrappedDict);
    }

    public MixableDict(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer) {
        _mixinListHead = new DictMixinTerminalNode<K, V>(WrappedDict);
    }

    public MixableDict(IDictionary<K, V> originalDictionary)
        : base(originalDictionary) {
        _mixinListHead = new DictMixinTerminalNode<K, V>(WrappedDict);
    }

    public MixableDict(IDictionary<K, V> originalDictionary, bool wrap)
        : base(originalDictionary, wrap) {
        _mixinListHead = new DictMixinTerminalNode<K, V>(WrappedDict);
    }

    public MixableDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        : base(originalDictionary, comparer) {
        _mixinListHead = new DictMixinTerminalNode<K, V>(WrappedDict);
    }
    #endregion

    #region Properties
    /// <summary>
    /// The head of the mixin linked-list for extending collection behavior.
    /// </summary>
    private IDictMixinNode<K, V> _mixinListHead;

    /// <summary>
    /// Gets/Sets all mixins applied to this dictionary for extension.
    /// </summary>
    public IEnumerable<IDictMixin<K, V>> Mixins {
        get {
            var mixinHolder = _mixinListHead;
            while (!(mixinHolder is DictMixinTerminalNode<K, V>)) {
                if (mixinHolder is DictMixinLinkNode<K, V> dictMixinLinkNode) {
                    mixinHolder = dictMixinLinkNode.NextMixin;
                    yield return dictMixinLinkNode.Mixin;
                } else {
                    break;
                }
            }
        }
        set {
            IDictMixinNode<K, V> headMixinHolder = new DictMixinTerminalNode<K, V>(WrappedDict);
            foreach (IDictMixin<K, V> mixin in value) {
                IDictMixinNode<K, V> oldHeadMixinHolder = headMixinHolder;
                headMixinHolder = new DictMixinLinkNode<K, V>(mixin, WrappedDict) {
                    NextMixin = oldHeadMixinHolder
                };
            }

            _mixinListHead = headMixinHolder;
        }
    }
    #endregion

    #region Dictionary Implementation
    public override V this[K key] {
        get {
            return _mixinListHead[key];
        }
        set {
            _mixinListHead[key] = value;
        }
    }

    public override bool Remove(K key) {
        return _mixinListHead.Remove(key);
    }

    public override void Add(K key, V value) {
        _mixinListHead.Add(key, value);
    }

    public override ICollection<K> Keys {
        get { return _mixinListHead.Keys; }
    }

    public override ICollection<V> Values {
        get { return _mixinListHead.Values; }
    }

    public override bool ContainsKey(K key) {
        return _mixinListHead.ContainsKey(key);
    }

    public override bool Contains(KeyValuePair<K, V> item) {
        return _mixinListHead.Contains(item);
    }
    public override bool Remove(KeyValuePair<K, V> item) {
        return _mixinListHead.Remove(item);
    }
    public override void Clear() {
        _mixinListHead.Clear();
    }
    public override int Count {
        get { return _mixinListHead.Count; }
    }

    public override void Add(KeyValuePair<K, V> item) {
        _mixinListHead.Add(item.Key, item.Value);
    }
    public override void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
        _mixinListHead.CopyTo(array, arrayIndex);
    }

    public override IEnumerator GetObjectEnumerator() {
        return _mixinListHead.GetEnumerator();
    }

    /// <summary>
    /// Returns an ordered KeyValuePair enumerator.
    /// </summary>
    public override IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
        return _mixinListHead.GetEnumerator();
    }

    public override bool TryGetValue(K key, out V val) {
        return _mixinListHead.TryGetValue(key, out val);
    }
    #endregion

}

public static class MixableDictFactory {
    public static MixableDict<K, V> Wrap<K, V>(IDictionary<K, V> dictionaryToWrap) => new MixableDict<K, V>(dictionaryToWrap, wrap: true);
    public static MixableDict<K, V> WrapInMixableDict<K, V>(this IDictionary<K, V> dictionaryToWrap) => Wrap(dictionaryToWrap);
}