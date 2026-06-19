namespace PxtlCa.ObjectProxies;

public interface IKeyedProxyFactory<TDictionary, TKey, TValue> {
    IObjectProxy<TValue> Create(IObjectProxy<TDictionary> dict, TKey key);
}
