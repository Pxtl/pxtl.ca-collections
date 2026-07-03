namespace PxtlCa.Collections.Proxies;

public class ArrayProxy<TValue> : ObjectMemberProxy<TValue[], TValue> {
    public ArrayProxy(TValue[] array, int index)
        : base(new ArrayItemProxy<TValue>(index), new ValueProxy<TValue[]>(array)) { }
}
