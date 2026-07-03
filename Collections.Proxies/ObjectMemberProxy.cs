namespace PxtlCa.Collections.Proxies;

public class ObjectMemberProxy<TParent, TMember> : IObjectMemberProxy<TParent, TMember> {
    public IMemberProxy<TParent, TMember> MemberProxy { get; }
    public IObjectProxy<TParent> Parent { get; }

    public ObjectMemberProxy(IMemberProxy<TParent, TMember> memberProxy, IObjectProxy<TParent> parent) {
        Ensure.NotNull(memberProxy, nameof(memberProxy));
        Ensure.NotNull(parent, nameof(parent));

        MemberProxy = memberProxy;
        Parent = parent;
    }

    public TMember Value {
        get {
            return MemberProxy.Get(Parent!.Value!);
        }
        set {
            MemberProxy.Set(Parent, value);
        }
    }
}
