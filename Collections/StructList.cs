using System.Collections;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using PxtlCa.Collections.Extensions;

namespace PxtlCa.Collections;

/// <summary>
/// Simple <see cref="IReadOnlyList{T}"/> with <see cref="ValueType"/> semantics
/// such as member-based equality, copy-by-value semantics (which is trivial
/// since it's immutable), initial non-null value.  Named "StructList" because
/// "ValueList" might be more confusing.
/// </summary>
/// <typeparam name="T">The type of the items in the list.</typeparam>
public struct StructList<T> : IReadOnlyList<T> {
    #region Constructors
    public StructList(IEnumerable<T> values) {
        _array = values.ToImmutableArray();
    }

    public StructList(ImmutableArray<T> values) {
        _array = values;
    }

    public StructList(StructList<T> values) {
        _array = values._array;
    }

    #endregion
    private ImmutableArray<T> _array;
    public static StructList<T> Empty { get; }

    private ImmutableArray<T> Value {
        get {
            if (_array.IsDefault) {
                _array = ImmutableArray<T>.Empty;
            }
            return _array;
        }
    }

    /// <summary>Gets the number of items contained in this collection.</summary>
    public int Count => Value.Length;

    /// <summary>Gets the index of an item in the wrapped collection.</summary>
    public int IndexOf(T item) => Value.IndexOf(item);

    /// <summary>Determines whether the collection contains a specific item of type T.</summary>
    /// <param name="item">The item whose presence is checked.</param>
    public bool Contains(T item) => Value.Contains(item);

    /// <summary>Copies elements to an array starting at arrayIndex.</summary>
    public void CopyTo(T[] array, int arrayIndex) => Value.CopyTo(array, arrayIndex);

    /// <summary>Gets or sets a elemeant by index in the collection.</summary>
    public T this[int index] {
        get {
            return Value[index];
        }
    }
    #region IEnumerable<T> Members

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    => ((IEnumerable<T>)Value).GetEnumerator();

    #endregion IEnumerable<T> Members

    #region IEnumerable Members

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    => ((IEnumerable)Value).GetEnumerator();
    #endregion IEnumerable Members

    #region operators
    public override bool Equals(object obj) {
        if (obj is IEnumerable<T> enumerable) {
            return this.SequenceEqual(enumerable);
        } else {
            return base.Equals(obj);
        }
    }
    private int? _cachedHashCode = null;
    public override int GetHashCode() {
        if (!_cachedHashCode.HasValue) {
            HashCode hash = new HashCode();
            foreach (var item in Value) {
                hash.Add(item);
            }
            _cachedHashCode = hash.ToHashCode();
        }
        return _cachedHashCode.Value;
    }
    public override string ToString()
    => $"[{string.Join(", ", _array)}]";
    public static bool operator ==(StructList<T> a, IEnumerable<T> b)
    => a.Value.SequenceEqual(b);
    public static bool operator !=(StructList<T> a, IEnumerable<T> b)
    => !a.Value.SequenceEqual(b);
    public static StructList<T> operator +(StructList<T> a, IEnumerable<T> b)
    => new StructList<T>(a.Value.AddRange(b));
    public static StructList<T> operator +(StructList<T> a, StructList<T> b)
    => new StructList<T>(a.Value.AddRange(b.Value));
    #endregion
}

public static class StructList {
    public static StructList<T> Create<T>(IEnumerable<T> values)
    => new StructList<T>(values);

    public static StructList<T> Create<T>(ImmutableArray<T> values)
    => new StructList<T>(values);

    public static StructList<T> Create<T>(StructList<T> values)
    => new StructList<T>(values);
}