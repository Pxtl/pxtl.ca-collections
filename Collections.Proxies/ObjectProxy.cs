namespace PxtlCa.Collections.Proxies;

public abstract class ObjectProxy<T> : IObjectProxy<T> {
    abstract public T Value { get; set; }
    public override bool Equals(object obj) {
        return Equals(Value, obj);
    }
    public override int GetHashCode() {
        return Value?.GetHashCode() ?? -1;
    }
    public override string? ToString() {
        return Value?.ToString();
    }
}
