namespace PxtlCa.Collections.Proxies;

public class ListProxy<TValue> : ObjectMemberProxy<IList<TValue>, TValue> {
    public ListProxy(IList<TValue> list, int index)
        : base(new ListItemProxy<TValue>(index), new ValueProxy<IList<TValue>>(list)) { }
}
