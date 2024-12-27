using System.Numerics;

namespace Utils;

public static class DictionaryExtensions
{
    public static void AddOrIncrement<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue val) where TValue : INumber<TValue>
    {
        dict[key] = dict.GetValueOrDefault(key, TValue.Zero) + val;
    }

    public static void AddOrAppend<TKey, TValue>(this Dictionary<TKey, List<TValue>> dict, TKey key, TValue value)
    {
        var cv = dict.GetValueOrDefault(key, []);
        cv.Add(value);

        dict[key] = cv;
    }
}
