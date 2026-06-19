namespace PxtlCa.Collections;

public class ChangeNotingDictionary<TKey, TValue> : VirtualDictionary<TKey, TValue>
where TValue : ICloneable {
    /// <summary>
    /// Underlying dictionary - everything delegates to this.
    /// </summary>
    private IDictionary<TKey, ChangeNote<TValue>> changes;
    public IDictionary<TKey, ChangeNote<TValue>> Changes {
        get {
            return changes;
        }
    }

    public delegate void ChangeHandler(TKey key, ChangeNote<TValue> change);
    public ChangeHandler? Changed;

    #region New Methods and Members

    public bool NoteChanges = false;

    protected void NoteChange(TKey key, ChangeNoteType type, TValue? val) {
        if (NoteChanges) {
            ChangeNote<TValue> change = new ChangeNote<TValue>(type, val);
            changes[key] = change;
            if (Changed != null) {
                Changed(key, change);
            }
        }
    }

    #endregion

    #region Constructors
    // We provide all the same constructors that the generic
    // Dictionary provides.

    public ChangeNotingDictionary() : base() {
        changes = new Dictionary<TKey, ChangeNote<TValue>>();
    }

    public ChangeNotingDictionary(ChangeHandler onChanged)
        : this() {
        Changed = onChanged;
    }

    public ChangeNotingDictionary(ChangeHandler onChanged, IEqualityComparer<TKey> comparer)
        : this(comparer) {
        Changed = onChanged;
    }

    public ChangeNotingDictionary(IEqualityComparer<TKey> comparer)
        : base(comparer) {
        changes = new Dictionary<TKey, ChangeNote<TValue>>(comparer);
    }

    public ChangeNotingDictionary(int capacity)
        : base(capacity) {
        changes = new Dictionary<TKey, ChangeNote<TValue>>();
    }

    public ChangeNotingDictionary(int capacity, IEqualityComparer<TKey> comparer)
        : base(capacity, comparer) {
        changes = new Dictionary<TKey, ChangeNote<TValue>>(comparer);
    }

    public ChangeNotingDictionary(IDictionary<TKey, TValue> originalDictionary)
        : base(originalDictionary) {
        changes = new Dictionary<TKey, ChangeNote<TValue>>();
    }

    public ChangeNotingDictionary(IDictionary<TKey, TValue> originalDictionary, IEqualityComparer<TKey> comparer)
        : base(originalDictionary, comparer) {
        changes = new Dictionary<TKey, ChangeNote<TValue>>(comparer);
    }
    #endregion

    // This methods allow an existing dictionary to be wrapped.
    // (The public constructors that take an IDictionary all
    // copy the data.)
    public static ChangeNotingDictionary<TKey, TValue> Wrap(IDictionary<TKey, TValue> originalDictionary, IEqualityComparer<TKey> comparer) {
        return new ChangeNotingDictionary<TKey, TValue>(originalDictionary, comparer, true);
    }

    // Private constructor used to enable a dictionary either to be
    // copied, or used as the underying storage.
    protected ChangeNotingDictionary(IDictionary<TKey, TValue> originalDictionary, IEqualityComparer<TKey> comparer, bool wrap)
        : base(originalDictionary, wrap) {
        changes = new Dictionary<TKey, ChangeNote<TValue>>(comparer);
    }

    // This is the raison d'etre of this whole class - an indexer
    // which notes any changes
    public override TValue this[TKey key] {
        get {
            return base[key];
        }
        set {
            base[key] = value;
            NoteChange(key, ChangeNoteType.Set, value);
        }
    }

    // Delegating implementations of all other methods.
    public override bool Remove(TKey key) {
        NoteChange(key, ChangeNoteType.Remove, default(TValue));
        return base.Remove(key);
    }
    public override void Add(TKey key, TValue value) {
        base.Add(key, value);
        NoteChange(key, ChangeNoteType.Add, value);
    }
    public override bool Remove(KeyValuePair<TKey, TValue> item) {
        NoteChange(item.Key, ChangeNoteType.Remove, default(TValue));
        return base.Remove(item);
    }

    public override void Clear() {
        foreach (TKey key in Keys) {
            NoteChange(key, ChangeNoteType.Remove, default(TValue));
        }
        base.Clear();
    }

    public override void Add(KeyValuePair<TKey, TValue> item) {
        Changes[item.Key] = new ChangeNote<TValue>(ChangeNoteType.Add, item.Value);
        base.Add(item);
    }
}
