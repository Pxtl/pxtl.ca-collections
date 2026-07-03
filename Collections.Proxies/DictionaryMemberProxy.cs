namespace PxtlCa.Collections.Proxies;

public class DictionaryMemberProxy<TKey, TValue>
: MemberProxy<IDictionary<TKey, TValue>, TValue> {
    public readonly TKey Key;

    public DictionaryMemberProxy(TKey key) {
        //have to box it to get null-checks to work generically.
        Ensure.NotNull((object?)key, nameof(key));
        Key = key;
    }

    public override TValue Get(IDictionary<TKey, TValue> parent) {
        Ensure.NotNull(parent, nameof(parent));
        return parent[Key];
    }

    public override void Set(IObjectProxy<IDictionary<TKey, TValue>> parent, TValue memberValue) {
        Ensure.NotNull(parent, nameof(parent));
        parent.ThrowIfValNull();
        parent!.Value![Key] = memberValue;
    }

    public override ICollection<string> AsPathStrings {
        get {
            return [Key?.ToString() ?? ""];
        }
    }
}
