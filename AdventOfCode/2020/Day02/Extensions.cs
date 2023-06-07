using System.Collections.Generic;

namespace Extensions
{
    public static class ListDeconstructionExtensions
    {
        public static string GetFirstHalf(this string[] array)
        {
            if (array == null || array.Length == 0)
                return string.Empty;

            int length = array[0].Length / 2;
            return array[0].Substring(0, length);
        }

        public static void Deconstruct<T>(this IList<T> list, out T first)
        {
            first = list.Count > 0 ? list[0] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
            third = list.Count > 2 ? list[2] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
            third = list.Count > 2 ? list[2] : default;
            fourth = list.Count > 3 ? list[3] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth, out T fifth)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
            third = list.Count > 2 ? list[2] : default;
            fourth = list.Count > 3 ? list[3] : default;
            fifth = list.Count > 4 ? list[4] : default;
        }
    }
}
