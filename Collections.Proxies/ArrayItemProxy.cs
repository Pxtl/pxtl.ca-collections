namespace PxtlCa.Collections.Proxies;

public class ArrayItemProxy<TValue> : MemberProxy<TValue[], TValue> {
    public int Index { get; }
    public ArrayItemProxy(int index)
        : base() { }

    public override ICollection<string> AsPathStrings => [Index.ToString()];

    public override TValue Get(TValue[] parent) => parent[Index];

    public override void Set(IObjectProxy<TValue[]> parent, TValue memberValue) {
        parent.Value[Index] = memberValue;
    }
}
