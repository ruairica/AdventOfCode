using System.Numerics;

namespace Utils;

public static class DictionaryExtensions
{
    public static void AddOrIncrement<T>(this Dictionary<T, int> dict, T key, int val)
    {
        dict[key] = dict.GetValueOrDefault(key, 0) + val;
    }

    public static void AddOrIncrement<T>(this Dictionary<T, long> dict, T key, long val)
    {
        dict[key] = dict.GetValueOrDefault(key, 0) + val;
    }
}
