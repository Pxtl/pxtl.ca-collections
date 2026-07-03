namespace PxtlCa.Collections.Proxies;

public class ListItemProxy<TValue> : MemberProxy<IList<TValue>, TValue> {
    public int Index { get; }
    public ListItemProxy(int index)
        : base() { }

    public override ICollection<string> AsPathStrings => [Index.ToString()];

    public override TValue Get(IList<TValue> parent) => parent[Index];

    public override void Set(IObjectProxy<IList<TValue>> parent, TValue memberValue) {
        parent.Value[Index] = memberValue;
    }
}
