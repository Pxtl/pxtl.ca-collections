# PxtlCa.Collections

This is an old collections library made by Martin Zarate (AKA Pxtl) in 2008.

Provides customizable Dictionaries and Lists.

# VirtualList, VirtualDictionary

These are simple implementations of `IList<T>` and `IDictionary<TKey, TValue>`
that provide a wrapper around an existing IList or IDictionary (defaulting to
the `System.Collections.Generics` implementations) and implement every operation
with a `virtual` modifier so that they can be conveniently subclassed and
overridden.  This makes it convenient to implement custom collections.

# FilteredDictionary

FilteredDictionary is `IDictionary<TKey, TValue>` that allows one or more
"filters", or "mixins".  Each one changes the dictionary's functionality, for
example by firing events or by providing default value logic for the dictionary
so that it will never throw a `KeyNotFoundException`.  Available filters are
provided in `PxtlCa.Collections.DictionaryFilters`.  For convenience, pre-built
FilteredDictionaries exist in the form of `AutoConstructingDictionary`,
`DefaultingDictionary`, and `ChangeNotingDictionary`, which each of which are a
subclass of FilteredDictionary with a Filter baked-in.

# AI Disclosure

While this code is primarily hand-made, a Qwen 3.5 AI was used to modernize the
codebase a bit and add the test classes.

# Contributions

See [CONTRIB.md](CONTRIB.md).

# License

See [LICENSE](LICENSE).