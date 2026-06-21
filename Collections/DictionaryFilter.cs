using System.Collections;

namespace PxtlCa.Collections;

public class DictionaryFilter<TKey, TValue> : IDictionaryFilter<TKey, TValue> {
    #region Dictionary Methods
    public virtual bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key) {
        return nextFilter.Remove(key);
    }

    public virtual void Add(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, TValue value) {
        nextFilter.Add(key, value);
    }

    public virtual ICollection<TKey> GetKeys(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return nextFilter.Keys;
    }

    public virtual ICollection<TValue> GetValues(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return nextFilter.Values;
    }

    public virtual bool ContainsKey(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key) {
        return nextFilter.ContainsKey(key);
    }

    public virtual bool Contains(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue> item) {
        return nextFilter.Contains(item);
    }

    public virtual bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue> item) {
        return nextFilter.Remove(item);
    }

    public virtual void Clear(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        nextFilter.Clear();
    }

    public virtual int GetCount(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return nextFilter.Count;
    }

    public virtual void CopyTo(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        nextFilter.CopyTo(array, arrayIndex);
    }

    public virtual IEnumerator GetObjectEnumerator(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return nextFilter.GetEnumerator();
    }

    public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
        return nextFilter.GetEnumerator();
    }

    public virtual bool TryGetValue(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, out TValue val) {
        return nextFilter.TryGetValue(key, out val);
    }

    public virtual TValue GetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> NextFilter, TKey key) {
        return NextFilter[key];
    }

    public virtual void SetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> NextFilter, TKey key, TValue value) {
        NextFilter[key] = value;
    }
    #endregion
}
