namespace Aoc.Utils;

public static class DictionaryExtensions
{
    public static void AddOrUpdate<T>(this Dictionary<T, int> dict, T key, int val)
    {
        dict[key] = dict.GetValueOrDefault(key, 0) + val;
    }
}