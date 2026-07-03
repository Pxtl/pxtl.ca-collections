namespace PxtlCa.Collections.Proxies;

public class ValueProxy<T> : ObjectProxy<T> {
    protected T _val;
    public ValueProxy(T val) { _val = val; }
    public override T Value {
        get { return _val; }
        set { _val = value!; }
    }
}
