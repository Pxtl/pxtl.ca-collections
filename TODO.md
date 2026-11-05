# TODO

- More documentation.
- VirtualHashSet, VirtualOrderedDictionary
- ILookup is immutable but can be modeled as a an OrderedDictionary of Lists. We should do that.
- are these things JSON serializable?
- Figure out what the story was with that commented-out code in TreePathProxy.cs

## ConcurrentHashSet<T> refactoring (deferred — not fixing now)

- `IsProperSubsetOf` / `IsProperSupersetOf` can be tested more efficiently using element `Count`s.
- Explicit interface implementations are overused.
- Some methods could leverage other methods — e.g. `IsProperSubsetOf` / `IsProperSupersetOf` could build on the basic `IsSubsetOf` / `IsSupersetOf` methods.
