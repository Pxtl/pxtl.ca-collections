namespace PxtlCa.Collections;

/// <summary>
/// Filters are managed by the <see cref="FilteredDictionary{TKey, TValue}"/> as a linked list.
/// This interface represents a node in that list.
/// </summary>
public interface IDictionaryFilterNode<TKey, TValue> : IDictionary<TKey, TValue> {
    internal void SetDictionary(FilteredDictionary<TKey, TValue> thisDictionary);
}
