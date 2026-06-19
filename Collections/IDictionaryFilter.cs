using System.Collections;

namespace PxtlCa.Collections;

/// <summary>
/// Similar to an <see cref="IDictionary{TKey, TValue}"/> interface, this interface describes
/// every operation for a dictionary, but implementors can also redirect to the
/// next Filter in the linked list.
/// </summary>
public interface IDictionaryFilter<TKey, TValue> {
    void Add(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, TValue value);
    void Clear(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter);
    bool Contains(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue> item);
    bool ContainsKey(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key);
    void CopyTo(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue>[] array, int arrayIndex);
    int GetCount(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter);
    IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter);
    ICollection<TKey> GetKeys(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter);
    IEnumerator GetObjectEnumerator(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter);
    TValue GetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> NextFilter, TKey key);
    ICollection<TValue> GetValues(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter);
    bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key);
    bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue> item);
    void SetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> NextFilter, TKey key, TValue value);
    bool TryGetValue(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, out TValue val);
}
