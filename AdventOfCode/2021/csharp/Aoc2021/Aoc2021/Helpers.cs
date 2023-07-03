using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2021
{
    public static class Helpers
    {
        public static int ConvertToDecimal(List<int> binary)
        {
            var result = 0;
            for (int m = 0; m < binary.Count; m++)
            {
                result += binary[m] * (int)Math.Pow(2, binary.Count - 1 - m);
            }
            return result;
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
