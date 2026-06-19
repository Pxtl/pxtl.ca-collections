using System;
using System.Collections.Generic;
using System.Text;

namespace PxtlCa.Collections;

public static class DictionaryExtensions {
    public static T Clone<T>(T obj) where T : ICloneable {
        return (T)obj;
    }

    public static void AddRange<TKey, TValue>(IDictionary<TKey, TValue> aDictionary, IEnumerable<KeyValuePair<TKey, TValue>> range) {
        foreach (KeyValuePair<TKey, TValue> pair in range) {
            aDictionary.Add(pair);
        }
    }

    public static void SetRange<TKey, TValue>(IDictionary<TKey, TValue> aDictionary, IEnumerable<KeyValuePair<TKey, TValue>> range) {
        foreach (KeyValuePair<TKey, TValue> pair in range) {
            aDictionary[pair.Key] = pair.Value;
        }
    }

    public static void AddRangeCloning<TKey, TValue>(IDictionary<TKey, TValue> aDictionary, IEnumerable<KeyValuePair<TKey, TValue>> range) where TValue : ICloneable {
        foreach (KeyValuePair<TKey, TValue> pair in range) {
            aDictionary.Add(pair.Key, Clone(pair.Value));
        }
    }

    public static void SetRangeCloning<TKey, TValue>(IDictionary<TKey, TValue> aDictionary, IEnumerable<KeyValuePair<TKey, TValue>> range) where TValue : ICloneable {
        foreach (KeyValuePair<TKey, TValue> pair in range) {
            aDictionary[pair.Key] = Clone(pair.Value);
        }
    }
}
