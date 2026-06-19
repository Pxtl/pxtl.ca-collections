namespace PxtlCa.ObjectProxies;

public class DictionaryProxyFactory<TDictionary, TKey, TValue> : IKeyedProxyFactory<TDictionary, TKey, TValue> where TDictionary : IDictionary<TKey, TValue> {
    private DictionaryProxyFactory() { }
    public static readonly DictionaryProxyFactory<TDictionary, TKey, TValue> Instance = new DictionaryProxyFactory<TDictionary, TKey, TValue>();
    public IObjectProxy<TValue> Create(IObjectProxy<TDictionary> dict, TKey key) {
        return Create(dict.Val, key);
    }

    public IObjectProxy<TValue> Create(TDictionary dict, TKey key) {
        return new DictionaryProxy<TKey, TValue>(dict, key);
    }
}
