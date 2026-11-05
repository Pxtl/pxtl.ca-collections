using System.Collections;
using System.Collections.Concurrent;

namespace PxtlCa.Collections;

/// <summary>
/// A thread-safe, unordered collection of distinct elements.
/// </summary>
/// <remarks>
/// A thin wrapper over <see cref="ConcurrentDictionary{TKey, TValue}"/> providing
/// set semantics (uniqueness by element). The backing dictionary maps each element
/// to a placeholder value; only the keys (the elements themselves) are meaningful.
/// </remarks>
public class ConcurrentHashSet<T> : IEnumerable<T> {
    private readonly ConcurrentDictionary<T, byte> _dictionary;

    #region constructors
    public ConcurrentHashSet() : this(EqualityComparer<T>.Default) { }

    public ConcurrentHashSet(IEqualityComparer<T> comparer) {
        _dictionary = new ConcurrentDictionary<T, byte>(comparer);
    }
    #endregion

    /// <summary>
    /// Gets the number of distinct elements currently in the set.
    /// </summary>
    public int Count => _dictionary.Count;

    /// <summary>
    /// Adds an element to the set.
    /// </summary>
    /// <param name="item">The element to add.</param>
    /// <returns>
    /// true if the element was added (it was not already present); false if an
    /// equal element was already in the set.
    /// </returns>
    public bool Add(T item) => _dictionary.TryAdd(item, (byte)0);

    /// <summary>
    /// Removes an element from the set.
    /// </summary>
    /// <param name="item">The element to remove.</param>
    /// <returns>true if the element was found and removed; false otherwise.</returns>
    public bool Remove(T item) => _dictionary.TryRemove(item, out _);

    /// <summary>
    /// Determines whether the set contains the element.
    /// </summary>
    /// <param name="item">The element to locate.</param>
    /// <returns>true if the element is in the set; otherwise false.</returns>
    public bool Contains(T item) => _dictionary.ContainsKey(item);

    /// <summary>
    /// Removes all elements from the set.
    /// </summary>
    public void Clear() => _dictionary.Clear();

    #region IEnumerable<T>
    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => _dictionary.Keys.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion
}
