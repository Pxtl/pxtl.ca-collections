namespace PxtlCa.Collections.Proxies;

public class TreePathMemberProxy<TDictionary, TKey> : MemberProxy<TDictionary, TDictionary>
where TDictionary : IDictionary<TKey, TDictionary> {
    public readonly IReadOnlyList<TKey> KeyPath;

    public TreePathMemberProxy(IReadOnlyList<TKey> keyPath) {
        KeyPath = keyPath;
    }

    public TreePathMemberProxy(TreePathMemberProxy<TDictionary, TKey> toClone) {
        KeyPath = toClone.KeyPath.ToList()!;
    }

    public TreePathMemberProxy<TDictionary, TKey> Clone() {
        return new TreePathMemberProxy<TDictionary, TKey>(this);
    }

    public override TDictionary Get(TDictionary parent) {
        if (KeyPath == null) {
            return parent;
        }

        TDictionary curNode = parent;
        foreach (TKey name in KeyPath) {
            curNode = curNode[name];
        }
        return curNode;
    }

    public override void Set(IObjectProxy<TDictionary> parent, TDictionary memberValue) {
        if (KeyPath == null) {
            parent.Value = memberValue;
            return;
        }

        IObjectProxy<TDictionary> curNode = parent;
        foreach (TKey name in KeyPath) {
            curNode = new DictionaryProxy<TKey, TDictionary>(curNode.Value, name);
        }
        curNode.Value = memberValue;
    }

    public override ICollection<string> AsPathStrings {
        get {
            List<string> result = new List<string>(KeyPath.Count);
            foreach (TKey key in KeyPath) {
                result.Add(key?.ToString() ?? "");
            }
            return result;
        }
    }

    public static TreePathMemberProxy<TDictionary, TKey> operator +(
        TreePathMemberProxy<TDictionary, TKey> first,
        TreePathMemberProxy<TDictionary, TKey> second
    ) => new TreePathMemberProxy<TDictionary, TKey>(first.KeyPath.Concat(second.KeyPath).ToList());
}
