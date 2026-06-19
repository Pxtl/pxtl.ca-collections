namespace PxtlCa.Collections.DictionaryFilters;

public class DefaultValueTypeDefaultingDictionaryFilter<TKey, TValue> : DefaultingDictionaryFilter<TKey, TValue>
where TValue : struct {
    protected override bool TryGetDefaultValue(TKey key, out TValue val) {
        // can use "!" because we already declared that TValue is nullable in where.
        val = default!;
        return true;
    }
}
