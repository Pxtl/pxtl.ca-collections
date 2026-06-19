namespace PxtlCa.ObjectProxies;

public static class DictionaryTraversalTool {
    public delegate void NodeSynchronizeHandler<TValue>(TValue source, IObjectProxy<TValue> target);

    public static void SynchronizeDictionaries<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source, IDictionary<TKey, TValue> target, NodeSynchronizeHandler<TValue> handler) {
        foreach (KeyValuePair<TKey, TValue> pair in source) {
            handler(pair.Value, new DictionaryProxy<TKey, TValue>(target, pair.Key));
        }
    }
}