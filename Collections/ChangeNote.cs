namespace PxtlCa.Collections;

public struct ChangeNote<TValue> {
    public readonly ChangeNoteType Type;
    public readonly TValue? Val;

    public ChangeNote(ChangeNoteType type, TValue? val)
        : this(type) {
        Val = val;
    }

    public ChangeNote(ChangeNoteType type) {
        Type = type;
        Val = default(TValue);
    }
}
