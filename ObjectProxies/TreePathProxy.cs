namespace PxtlCa.ObjectProxies;

public class TreePathProxy<TDictionary, TKey> : ObjectMemberProxy<TDictionary, TDictionary> where TDictionary : IDictionary<TKey, TDictionary> {
    public TreePathProxy(IObjectProxy<TDictionary> nodeProxy, IList<TKey> keyPath) : base(new TreePathMemberProxy<TDictionary, TKey>(keyPath), nodeProxy) { }
}

//public class TreePathProxy<TDictionary, TKey> : ObjectProxy<TDictionary> where TDictionary : IDictionary<TKey, TDictionary>
//{
//    public IObjectProxy<TDictionary> dict;
//    public TreePathProxy(IObjectProxy<TDictionary> dict, IList<TKey> keyPath)
//    {
//        this.dict = dict;
//        this.keyPath = keyPath;
//    }
//    public override TDictionary Val
//    {
//        get
//        {
//            if (keyPath == null)
//                return dict.Val;

//            TDictionary curNode = dict.Val;
//            foreach (TKey name in keyPath)
//            {
//                curNode = curNode[name];
//            }
//            return curNode;
//        }
//        set
//        {
//            if (keyPath == null)
//            {
//                dict.Val = value;
//                return;
//            }

//            IObjectProxy<TDictionary> curNode = dict;
//            foreach (TKey name in keyPath)
//            {
//                curNode = new IndexerProxy<TKey, TDictionary>(curNode.Val, name);
//            }
//            curNode.Val = value; 
//        }
//    }
//}