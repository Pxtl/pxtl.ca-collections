namespace PxtlCa.Collections.DictionaryFilters {
    public class ChangeEventDictionaryFilter<TKey, TValue> : DictionaryFilter<TKey, TValue> {
        public ChangeEventDictionaryFilter(ChangeHandler changed) {
            Changed = changed;
        }

        public delegate void ChangeHandler(TKey key, ChangeNote<TValue> change);
        public ChangeHandler Changed;

        protected void NoteChange(TKey key, ChangeNoteType changeType, TValue? value) {
            if (Changed != null) {
                Changed(key, new ChangeNote<TValue>(changeType, value));
            }
        }

        // This is the raison d'etre of this whole class - an indexer
        // which notes any changes
        public override void SetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, TValue value) {
            nextFilter[key] = value;
            NoteChange(key, ChangeNoteType.Set, value);
        }

        // Delegating implementations of all other methods.
        public override bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key) {
            NoteChange(key, ChangeNoteType.Remove, default);
            return nextFilter.Remove(key);
        }
        public override void Add(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key, TValue value) {
            nextFilter.Add(key, value);
            NoteChange(key, ChangeNoteType.Add, value);
        }
        public override bool Remove(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, KeyValuePair<TKey, TValue> item) {
            NoteChange(item.Key, ChangeNoteType.Remove, default(TValue));
            return nextFilter.Remove(item);
        }

        public override void Clear(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter) {
            foreach (TKey key in thisDictionary.Keys) {
                NoteChange(key, ChangeNoteType.Remove, default(TValue));
            }
            nextFilter.Clear();
        }
    }
}
