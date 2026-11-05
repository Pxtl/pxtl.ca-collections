# PxtlCa.Collections

This is an old collections library made by Martin Zarate (AKA Pxtl) in 2008. It 
has since been modernised to run on .NET Standard 2.0 to maximise compatibility
with all versions of .NET (both modern Core and old Framework).

Provides customizable Dictionaries and Lists.

## VirtualList, VirtualDictionary

These are simple implementations of `IList<T>` and `IDictionary<TKey, TValue>`
that provide a wrapper around an existing IList or IDictionary (defaulting to
the `System.Collections.Generics` implementations) and implement every operation
with a `virtual` modifier so that they can be conveniently subclassed and
overridden.  This makes it convenient to implement custom collections.

## FilteredDictionary

FilteredDictionary is `IDictionary<TKey, TValue>` that allows one or more
"filters", or "mixins".  Each one changes the dictionary's functionality, for
example by firing events or by providing default value logic for the dictionary
so that it will never throw a `KeyNotFoundException`.  Available filters are
provided in `PxtlCa.Collections.DictionaryFilters`.  For convenience, pre-built
FilteredDictionaries exist in the form of `AutoConstructingDictionary`,
`DefaultingDictionary`, and `ChangeNotingDictionary`, which each of which are a
subclass of FilteredDictionary with a Filter baked-in.

## ConcurrentHashSet

The obvious thing - an implementation of `ISet<T>` using
ConcurrentDictionary.

## AI Disclosure

While this code is primarily hand-made, a Qwen 3.5 9B and 3.8 27B were used
to modernise the original 2008 codebase, add unit tests, and make any later
additions done afterwards (eg ConcurrentHashSet).

# PxtlCa.Collections.Proxies

A second project `PxtlCa.Collections.Proxies`.  It is a set of pseudo-pointer
generics in C#.  I don't really remember what I was going for with these.  This
may be mostly an artifact of .NET 2.0 since `Expression` objects have replaced
the need for that generally.

# Contributions

See [CONTRIB.md](CONTRIB.md).

# License

See [LICENSE](LICENSE).
