namespace PxtlCa.Collections;

/// <summary>
/// Mixins are managed by the <see cref="MixableDict{K, V}"/> as a linked list.
/// This interface represents a node in that list.
/// </summary>
public interface IDictMixinNode<K, V> : IDictionary<K, V> {
    internal void SetDict(MixableDict<K, V> thisDict);
}