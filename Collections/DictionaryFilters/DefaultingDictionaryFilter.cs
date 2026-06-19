namespace PxtlCa.Collections.DictionaryFilters {
    public abstract class DefaultingDictionaryFilter<TKey, TValue> : DictionaryFilter<TKey, TValue> {
        #region Constructors

        public DefaultingDictionaryFilter() : base() {

        }
        #endregion

        protected abstract bool TryGetDefaultValue(TKey key, out TValue val);

        public override TValue GetVal(IDictionary<TKey, TValue> thisDictionary, IDictionaryFilterNode<TKey, TValue> nextFilter, TKey key) {
            if (thisDictionary.TryGetValue(key, out TValue val)) {
                return nextFilter[key];
            } else if (TryGetDefaultValue(key, out TValue constructedVal)) {
                thisDictionary[key] = constructedVal;
                return constructedVal;
            } else {
                return nextFilter[key];
            }
        }
    }
}
