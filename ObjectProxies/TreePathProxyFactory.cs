namespace PxtlCa.ObjectProxies;

public class TreePathProxyFactory<TDictionary, TKey> : IKeyedProxyFactory<TDictionary, IList<TKey>, TDictionary> where TDictionary : IDictionary<TKey, TDictionary> {
    private TreePathProxyFactory() { }
    public static readonly TreePathProxyFactory<TDictionary, TKey> Instance = new TreePathProxyFactory<TDictionary, TKey>();
    public IObjectProxy<TDictionary> Create(IObjectProxy<TDictionary> dict, IList<TKey> keyList) {
        return new TreePathProxy<TDictionary, TKey>(dict, keyList);
    }
}
