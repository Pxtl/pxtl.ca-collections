using System.Collections;

namespace PxtlCa.Collections;

/// <summary>
/// Similar to an <see cref="IDictionary{K, V}"/> interface, this interface describes
/// every operation for a dictionary, but implementors can also redirect to the
/// next Mixin in the linked list.
/// </summary>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
public interface IDictMixin<K, V> {
    void Add(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key, V value);
    void Clear(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin);
    bool Contains(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, KeyValuePair<K, V> item);
    bool ContainsKey(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key);
    void CopyTo(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, KeyValuePair<K, V>[] array, int arrayIndex);
    int GetCount(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin);
    IEnumerator<KeyValuePair<K, V>> GetEnumerator(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin);
    ICollection<K> GetKeys(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin);
    IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin);
    V GetVal(IDictionary<K, V> thisDict, IDictMixinNode<K, V> NextMixin, K key);
    ICollection<V> GetValues(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin);
    bool Remove(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key);
    bool Remove(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, KeyValuePair<K, V> item);
    void SetVal(IDictionary<K, V> thisDict, IDictMixinNode<K, V> NextMixin, K key, V value);
    bool TryGetValue(IDictionary<K, V> thisDict, IDictMixinNode<K, V> nextMixin, K key, out V val);
}
