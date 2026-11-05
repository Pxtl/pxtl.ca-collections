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
/// Standard set operations are available via <see cref="ISet{T}"/>; those batch
/// operations are not atomic (each element is applied independently).
/// </remarks>
public class ConcurrentHashSet<T> : IEnumerable<T>, ISet<T> {
    private readonly ConcurrentDictionary<T, byte> _dictionary;
    private readonly IEqualityComparer<T> _comparer;

    #region constructors
    public ConcurrentHashSet() : this(EqualityComparer<T>.Default) { }

    public ConcurrentHashSet(IEqualityComparer<T> comparer) {
        _comparer = comparer;
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

    #region ICollection<T>
    /// <summary>
    /// Adds an element to the set (via <see cref="ICollection{T}"/>; returns nothing).
    /// </summary>
    /// <param name="item">The element to add.</param>
    void ICollection<T>.Add(T item) {
        _dictionary.TryAdd(item, (byte)0);
    }

    /// <summary>
    /// Copies the elements of the set to the specified array (via <see cref="ICollection{T}"/>).
    /// </summary>
    /// <param name="array">The array to copy the elements into.</param>
    /// <param name="arrayIndex">The zero-based index at which the copy begins.</param>
    void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
        if (array == null) { throw new ArgumentNullException(nameof(array)); }
        var index = arrayIndex;
        foreach (T element in _dictionary.Keys) {
            array[index++] = element;
        }
    }

    /// <summary>
    /// Gets whether the set is read-only (it is not).
    /// </summary>
    bool ICollection<T>.IsReadOnly => false;
    #endregion

    #region ISet<T>
    /// <summary>
    /// Modifies the set to contain the union of its current elements and the specified collection.
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    void ISet<T>.UnionWith(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        foreach (T element in other) {
            _dictionary.TryAdd(element, (byte)0);
        }
    }

    /// <summary>
    /// Modifies the set to remove any element that also appears in the specified collection.
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    void ISet<T>.ExceptWith(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        foreach (T element in other) {
            _dictionary.TryRemove(element, out _);
        }
    }

    /// <summary>
    /// Modifies the set to contain only the elements that also appear in the specified collection.
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    void ISet<T>.IntersectWith(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        var otherSet = new HashSet<T>(other, _comparer);
        foreach (T element in _dictionary.Keys) {
            if (!otherSet.Contains(element)) {
                _dictionary.TryRemove(element, out _);
            }
        }
    }

    /// <summary>
    /// Modifies the set to contain only the elements that are in this set or the specified
    /// collection, but not both (the symmetric difference).
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    void ISet<T>.SymmetricExceptWith(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        foreach (T element in other) {
            if (!_dictionary.TryAdd(element, (byte)0)) {
                _dictionary.TryRemove(element, out _);
            }
        }
    }

    /// <summary>
    /// Determines whether the set and the specified collection share at least one element.
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    bool ISet<T>.Overlaps(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        foreach (T element in other) {
            if (_dictionary.ContainsKey(element)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Determines whether the set contains all the elements of the specified collection.
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    bool ISet<T>.IsSupersetOf(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        foreach (T element in other) {
            if (!_dictionary.ContainsKey(element)) {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Determines whether the set is a proper superset of the specified collection
    /// (it contains all of its elements and at least one more).
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    bool ISet<T>.IsProperSupersetOf(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        var otherSet = new HashSet<T>(other, _comparer);
        var hasExtraElement = false;
        foreach (T element in _dictionary.Keys) {
            if (!otherSet.Contains(element)) {
                hasExtraElement = true;
                break;
            }
        }
        if (!hasExtraElement) {
            return false;
        }
        foreach (T element in other) {
            if (!_dictionary.ContainsKey(element)) {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Determines whether the set contains no element that is not in the specified collection.
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    bool ISet<T>.IsSubsetOf(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        var otherSet = new HashSet<T>(other, _comparer);
        foreach (T element in _dictionary.Keys) {
            if (!otherSet.Contains(element)) {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Determines whether the set is a proper subset of the specified collection
    /// (all of its elements are in the collection, and the collection has at least one more).
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    bool ISet<T>.IsProperSubsetOf(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        var otherSet = new HashSet<T>(other, _comparer);
        foreach (T element in _dictionary.Keys) {
            if (!otherSet.Contains(element)) {
                return false;
            }
        }
        foreach (T element in other) {
            if (!_dictionary.ContainsKey(element)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Determines whether the set and the specified collection contain the same elements.
    /// </summary>
    /// <param name="other">The collection of elements.</param>
    bool ISet<T>.SetEquals(IEnumerable<T> other) {
        if (other == null) { throw new ArgumentNullException(nameof(other)); }
        var otherSet = new HashSet<T>(other, _comparer);
        if (otherSet.Count != _dictionary.Count) {
            return false;
        }
        foreach (T element in _dictionary.Keys) {
            if (!otherSet.Contains(element)) {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region IEnumerable<T>
    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => _dictionary.Keys.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion
}
