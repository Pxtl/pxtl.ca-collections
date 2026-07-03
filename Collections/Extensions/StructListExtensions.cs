using System.Collections.Immutable;

namespace PxtlCa.Collections.Extensions;

public static class StructListExtensions {
    public static StructList<T> ToStructList<T>(this ImmutableArray<T> values)
    => new StructList<T>(values);
    public static StructList<T> ToStructList<T>(this IEnumerable<T> values)
    => new StructList<T>(values);
    public static StructList<T> ToStructList<T>(this StructList<T> values)
    => values;
}