namespace PxtlCa.Collections.Proxies;

public class DictionaryProxy<TKey, TValue> : ObjectMemberProxy<IDictionary<TKey, TValue>, TValue> {
    public DictionaryProxy(IDictionary<TKey, TValue> dict, TKey key)
        : base(new DictionaryMemberProxy<TKey, TValue>(key), new ValueProxy<IDictionary<TKey, TValue>>(dict)) { }
}
