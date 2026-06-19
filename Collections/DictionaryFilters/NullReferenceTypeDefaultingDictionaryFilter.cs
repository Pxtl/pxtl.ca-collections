namespace PxtlCa.Collections.DictionaryFilters {
    public class NullReferenceTypeDefaultingDictionaryFilter<TKey, TValue> : DefaultingDictionaryFilter<TKey, TValue>
    where TValue : class? {
        protected override bool TryGetDefaultValue(TKey key, out TValue val) {
            // can use "!" because we already declared that TValue is nullable in where.
            val = null!;
            return true;
        }
    }
}
