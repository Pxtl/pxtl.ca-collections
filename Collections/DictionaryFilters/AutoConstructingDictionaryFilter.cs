namespace PxtlCa.Collections.DictionaryFilters {
    public class AutoConstructingDictionaryFilter<TKey, TValue> : DefaultingDictionaryFilter<TKey, TValue>
    where TValue : new() {
        protected override bool TryGetDefaultValue(TKey key, out TValue val) {
            // can use "!" because we already declared that TValue is nullable in where.
            val = new TValue();
            return true;
        }
    }
}
